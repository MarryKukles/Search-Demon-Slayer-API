using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SearchDemonSlayerAPI;
using SearchDemonSlayerAPI.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://www.demonslayer-api.com/")
});

builder.Services.AddScoped<CharacterService>();

await builder.Build().RunAsync();
