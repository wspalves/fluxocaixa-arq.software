var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("dbserver")
                .AddDatabase("FluxoCaixaDB");

var apiFluxoCaixa = builder
    .AddProject<Projects.FluxoCaixaArq_FluxoCaixa_API>("FluxoCaixaAPI")
    .WithReference(sql);

var apiConsolidadoCaixa = builder
    .AddProject<Projects.FluxoCaixaArq_ConsolidadoCaixa_API>("ConsolidadoCaixaAPI")
    .WithReference(sql);

builder.AddProject<Projects.FluxoCaixaArq_Aspire_Migration>("migrations")
    .WithReference(sql);

builder.Build().Run();
