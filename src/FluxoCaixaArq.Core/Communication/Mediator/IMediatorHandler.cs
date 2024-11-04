using FluxoCaixaArq.Core.Messages;

namespace FluxoCaixaArq.Core.Communication.Mediator;

public interface IMediatorHandler
{
    Task PublicarEventoAsync<T>(T evento) where T : Event;
    Task<bool> EnviarComando<T>(T? command) where T : Command;
}