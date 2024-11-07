using FluxoCaixaArq.ConsolidadoCaixa.Application.Queries;
using FluxoCaixaArq.ConsolidadoCaixa.Application.ViewModel;

var builder = WebApplication.CreateBuilder(args);

builder
    .AddAPI()
    .AddServices()
    .AddLog();

var app = builder.Build();

app.UseAPI();

#region Get Consolidado Ontem

app.MapGet("v1/consolidado",
        async (IConsolidadoQueries _consolidado) => { return await _consolidado.ObterConsolidadoOntemAsync(); })
    .WithName("GetConsolidadoOntem")
    .WithOpenApi()
    .Produces<ConsolidadoViewModel>(200);

#endregion

#region GET Consolidado por Data

app.MapGet("v1/{data}/consolidado/",
        async (IConsolidadoQueries _consolidado, DateTime data) =>
        {
            return await _consolidado.ObterConsolidadoPorDataAsync(data);
        })
    .WithName("GetConsolidadoPorData")
    .WithOpenApi()
    .Produces<ConsolidadoViewModel>(200);

#endregion

app.Run();
Log.CloseAndFlush();