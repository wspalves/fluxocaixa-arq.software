using FluxoCaixaArq.ConsolidadoCaixa.Application.Interfaces;
using FluxoCaixaArq.ConsolidadoCaixa.Application.Queries;
using FluxoCaixaArq.ConsolidadoCaixa.Data.Repository;

namespace FluxoCaixaArq.ConsolidadoCaixa.API.Setup;

public static class ConsolidadoCaixaSetup
{
    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        //Queries
        builder.Services.AddScoped<IConsolidadoQueries, ConsolidadoQueries>();
        builder.Services.AddScoped<IConsolidadoRepository>(x =>
            new ConsolidadoRepository(builder.Configuration.GetConnectionString("FluxoCaixaDB") ??
                                      throw new InvalidOperationException("Null Connection String!")));

        return builder;
    }
}