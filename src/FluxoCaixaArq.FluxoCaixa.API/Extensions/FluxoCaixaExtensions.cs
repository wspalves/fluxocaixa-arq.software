using FluxoCaixaArq.Core.Communication.Mediator;
using FluxoCaixaArq.FluxoCaixa.API.AppServices;
using FluxoCaixaArq.FluxoCaixa.Application.Commands;
using FluxoCaixaArq.FluxoCaixa.Data;
using FluxoCaixaArq.FluxoCaixa.Data.Repository;
using FluxoCaixaArq.FluxoCaixa.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FluxoCaixaArq.FluxoCaixa.API.Extensions;

public static class FluxoCaixaExtensions
{
    public static WebApplicationBuilder AddDBContext(this WebApplicationBuilder builder,
        IConfiguration configuration)
    {
        builder.Services.AddDbContext<FluxoCaixaContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

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