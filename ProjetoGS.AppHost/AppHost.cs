var builder = DistributedApplication.CreateBuilder(args);

// Add MySQL container database
var mysql = builder.AddMySql("mysql");
var db = mysql.AddDatabase("gsdb");

var apiService = builder.AddProject<Projects.ProjetoGS_ApiService>("apiservice")
    .WithReference(db)
    .WithHttpHealthCheck("/health");

builder.AddProject<Projects.ProjetoGS_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
