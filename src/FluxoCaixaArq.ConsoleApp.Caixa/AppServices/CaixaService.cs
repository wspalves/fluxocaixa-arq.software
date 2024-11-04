using FluxoCaixaArq.Core.Communication.Mediator;
using FluxoCaixaArq.Core.DomainObjects;
using FluxoCaixaArq.FluxoCaixa.Application.Commands;
using Serilog;

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

        try
        {
            var result = await _mediatorHandler.EnviarComando(command);

            if (result)
                Log.Information("Crédito no valor de {valor} realizado com sucesso", valor);

            return result;
        }
        catch (DomainException ex)
        {
            Log.Warning("Crédito no valor de {valor} não realizado. Erro: {erro}", valor, ex.Message);
            return false;
        }
    }

    public async Task<bool> Debitar(decimal valor)
    {
        try
        {
            var command = new DebitarLancamentoCommand("Débito de valor", valor);
            var result = await _mediatorHandler.EnviarComando(command);

            if (result)
                Log.Information("Débito no valor de {valor} realizado com sucesso", valor);

            return result;
        }
        catch (DomainException ex)
        {
            Log.Warning("Débito no valor de {valor} não realizado. Erro: {erro}", valor, ex.Message);
            return false;
        }
    }
}