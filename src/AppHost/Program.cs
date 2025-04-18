using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

// Add the API project as a service
var api = builder.AddProject<Projects.VibeTrader_API>("api");

// Add SQL Server as a container resource
var sqlServer = builder.AddSqlServer("sql")
    .AddDatabase("VibeTraderDb");

// Add reference to the SQL database to the API project
api.WithReference(sqlServer);

// Add the Web project as a service
var web = builder.AddProject<Projects.VibeTrader_Web>("web")
    .WithReference(api);

builder.Build().Run();
