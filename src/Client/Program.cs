using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using VibeTrader.Client;
using Fluxor;
using VibeTrader.Client.Services;
using VibeTrader.Client.Services.Interfaces;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Set the static IsDevelopment flag
Program.IsDevelopment = builder.HostEnvironment.IsDevelopment();

// Register HttpClient factory
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

// Register client services
builder.Services.AddScoped<IAlertService, AlertService>();
builder.Services.AddScoped<IAlertApiService, AlertApiService>();

// Add Fluxor state management
builder.Services.AddFluxor(options => 
{
    options.ScanAssemblies(typeof(Program).Assembly);
    
    // Only add Redux DevTools in development
    if (builder.HostEnvironment.IsDevelopment())
    {
        try 
        {
            // Wrap this in a try-catch to prevent runtime errors if Redux DevTools extension is not available
            options.UseReduxDevTools();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to initialize Redux DevTools: {ex.Message}");
            Console.WriteLine("The application will continue without Redux DevTools support.");
        }
    }
});

await builder.Build().RunAsync();

/// <summary>
/// Program class with static properties
/// </summary>
public partial class Program
{
    /// <summary>
    /// Indicates whether the application is running in development mode
    /// </summary>
    public static bool IsDevelopment { get; set; }
}
