using FluxoCaixaArq.Core.Communication.Mediator;
using FluxoCaixaArq.FluxoCaixa.Application.Commands;

namespace FluxoCaixaArq.ConsoleApp.Caixa.AppServices;

public class CaixaService
{
    private readonly IMediatorHandler _mediatorHandler;

    public CaixaService(IMediatorHandler mediatorHandler)
    {
        _mediatorHandler = mediatorHandler;
    }

    public async Task<bool> Creditar(decimal valor)
    {
        var command = new CreditarLancamentoCommand("Crédito de valor", valor);
        return await _mediatorHandler.EnviarComando(command);
    }

    public async Task<bool> Debitar(decimal valor)
    {
        var command = new DebitarLancamentoCommand("Débito de valor", valor);
        return await _mediatorHandler.EnviarComando(command);
    }
}