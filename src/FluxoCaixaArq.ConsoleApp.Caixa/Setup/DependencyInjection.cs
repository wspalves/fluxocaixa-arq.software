using FluxoCaixaArq.ConsoleApp.Caixa.AppServices;
using FluxoCaixaArq.ConsolidadoCaixa.Application.Queries;
using FluxoCaixaArq.Core.Communication.Mediator;
using FluxoCaixaArq.FluxoCaixa.Application.Commands;
using FluxoCaixaArq.FluxoCaixa.Data;
using FluxoCaixaArq.FluxoCaixa.Data.Repository;
using FluxoCaixaArq.FluxoCaixa.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FluxoCaixaArq.ConsoleApp.Caixa.Setup;

public static class DependencyInjection
{
    public static void AddDependencyInjection(this IServiceCollection services)
    {
        // Mediator
        services.AddScoped<IMediatorHandler, MediatorHandler>();

        // FluxoCaixa
        services.AddScoped<FluxoCaixaContext>();
        services.AddScoped<ILancamentoRepository, LancamentoRepository>();
        services.AddScoped<IRequestHandler<CreditarLancamentoCommand, bool>, LancamentoCommandHandler>();
        services.AddScoped<IRequestHandler<DebitarLancamentoCommand, bool>, LancamentoCommandHandler>();

        // Caixa
        services.AddSingleton<CaixaService>();
        services.AddSingleton<Caixa>();

        // Consolidado
        services.AddScoped<ILancamentoQueries, LancamentoQueries>();
    }
}