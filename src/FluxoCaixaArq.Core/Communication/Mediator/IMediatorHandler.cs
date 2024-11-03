using FluxoCaixaArq.Core.Messages;
using FluxoCaixaArq.Core.Messages.CommonMessages.Notifications;

namespace FluxoCaixaArq.Core.Communication.Mediator;

public interface IMediatorHandler
{
    Task PublicarEventoAsync<T>(T evento) where T : Event;
    Task<bool> EnviarComando<T>(T? command) where T : Command;
    Task PublicarNotificacoes<T>(T notification) where T : DomainNotification;
}