using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add Aspire service defaults
builder.AddServiceDefaults();

// Add Blazor WebAssembly server services
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Add services for hosting Blazor WebAssembly
builder.Services.AddHttpClient();

// Add HttpClient factory for API communication
builder.Services.AddHttpClient("ApiClient", client =>
{
    // Configure the HttpClient to use the API service through Aspire service discovery
    var apiServiceUrl = builder.Configuration["Services:ApiService:Url"] ?? "http://api";
    client.BaseAddress = new Uri(apiServiceUrl);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");
app.MapDefaultEndpoints();

app.Run();
