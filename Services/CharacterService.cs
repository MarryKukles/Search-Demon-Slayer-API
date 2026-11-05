
using System.Net.Http.Json;
using System.Text.Json;
using SearchDemonSlayerAPI.Models;

namespace SearchDemonSlayerAPI.Services;

public class CharacterService(HttpClient http)
{
    public async Task<CharacterResponse> GetCharacters(int page)
    {
        return await http.GetFromJsonAsync<CharacterResponse>(
            $"api/v1/characters?page={page}") ?? new CharacterResponse();
    }

    public async Task<List<Character>> SearchCharacters(string name)
    {
        var url = $"api/v1/characters?name={Uri.EscapeDataString(name)}";
        var json = await http.GetStringAsync(url);

        using var document = JsonDocument.Parse(json);
        var root = document.RootElement;

        if (root.ValueKind == JsonValueKind.Array)
            return root.EnumerateArray().Select(ParseCharacter).ToList();

        foreach (var property in new[] { "content", "characters", "character", "data" })
        {
            if (root.TryGetProperty(property, out var result))
            {
                if (result.ValueKind == JsonValueKind.Array)
                    return result.EnumerateArray().Select(ParseCharacter).ToList();

                if (result.ValueKind == JsonValueKind.Object)
                    return [ParseCharacter(result)];
            }
        }

        if (root.ValueKind == JsonValueKind.Object)
            return [ParseCharacter(root)];

        return [];
    }

    private static Character ParseCharacter(JsonElement json)
    {
        var character = new Character();

        if (json.TryGetProperty("id", out var id))
            character.Id = id.GetInt32();

        if (json.TryGetProperty("name", out var name))
            character.Name = name.ToString();

        if (json.TryGetProperty("age", out var age))
        {
            if (age.ValueKind == JsonValueKind.Number)
                character.Age = age.GetInt32();
            else if (int.TryParse(age.ToString(), out var parsedAge))
                character.Age = parsedAge;
        }

        if (json.TryGetProperty("race", out var race))
            character.Race = race.ToString();

        if (json.TryGetProperty("quote", out var quote))
            character.Quote = quote.ToString();

        if (json.TryGetProperty("img", out var img))
            character.Img = img.ToString();

        return character;
    }
}
