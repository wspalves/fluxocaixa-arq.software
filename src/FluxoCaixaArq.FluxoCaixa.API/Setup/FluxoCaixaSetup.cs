using FluxoCaixaArq.FluxoCaixa.Data;
using FluxoCaixaArq.FluxoCaixa.Data.Repository;
using FluxoCaixaArq.FluxoCaixa.Domain.Interfaces;
using MediatR;

namespace FluxoCaixaArq.FluxoCaixa.API.Setup;

public static class FluxoCaixaSetup
{
    public static WebApplicationBuilder AddDBContext(this WebApplicationBuilder builder,
        IConfiguration configuration)
    {
        builder.AddSqlServerDbContext<FluxoCaixaContext>("FluxoCaixaDB");

        //builder.Services.AddDbContext<FluxoCaixaContext>(options =>
        //    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        return builder;
    }

    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        // Mediator
        builder.Services.AddScoped<IMediatorHandler, MediatorHandler>();

        // FluxoCaixa
        builder.Services.AddScoped<ILancamentoRepository, LancamentoRepository>();
        builder.Services.AddScoped<IRequestHandler<CreditarLancamentoCommand, bool>, LancamentoCommandHandler>();
        builder.Services.AddScoped<IRequestHandler<DebitarLancamentoCommand, bool>, LancamentoCommandHandler>();
        return builder;
    }
}