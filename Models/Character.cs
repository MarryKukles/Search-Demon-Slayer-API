
using System.Text.Json.Serialization;

namespace SearchDemonSlayerAPI.Models;

public class Character
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("age")]
    public int? Age { get; set; }

    [JsonPropertyName("race")]
    public string Race { get; set; } = "";

    [JsonPropertyName("quote")]
    public string Quote { get; set; } = "";

    [JsonPropertyName("img")]
    public string Img { get; set; } = "";
}

public class CharacterResponse
{
    [JsonPropertyName("content")]
    public List<Character> Content { get; set; } = [];

    [JsonPropertyName("pagination")]
    public Pagination Pagination { get; set; } = new();
}

public class Pagination
{
    [JsonPropertyName("currentPage")]
    public int CurrentPage { get; set; }

    [JsonPropertyName("totalPages")]
    public int TotalPages { get; set; }
}
