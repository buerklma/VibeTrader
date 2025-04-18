using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using VibeTrader.Client;
using Fluxor;
using VibeTrader.Client.Services;
using System.Reflection;
using VibeTrader.Client.State.AlertState;
using Fluxor.Blazor.Web.ReduxDevTools;
using Microsoft.Extensions.Configuration;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Load configuration from appsettings.json
var apiSettings = builder.Configuration.GetSection("ApiSettings");
var apiBaseUrl = apiSettings.GetValue<string>("BaseUrl") ?? builder.HostEnvironment.BaseAddress;

// Register HttpClient with configured API base address
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBaseUrl) });

// Register services
builder.Services.AddScoped<IAlertService, AlertService>();

// Register Fluxor
builder.Services.AddFluxor(options => options
    .ScanAssemblies(Assembly.GetExecutingAssembly())
    .UseReduxDevTools());

await builder.Build().RunAsync();
