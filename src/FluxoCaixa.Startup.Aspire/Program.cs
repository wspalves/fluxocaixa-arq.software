var builder = DistributedApplication.CreateBuilder(args);

var apiFluxoCaixa = builder.AddProject<Projects.FluxoCaixaArq_FluxoCaixa_API>("FluxoCaixaAPI");
var apiConsolidadoCaixa = builder.AddProject<Projects.FluxoCaixaArq_ConsolidadoCaixa_API>("ConsolidadoCaixaAPI");

builder.Build().Run();