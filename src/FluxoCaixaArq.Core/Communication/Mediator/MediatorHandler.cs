using FluxoCaixaArq.Core.Messages;
using FluxoCaixaArq.Core.Messages.CommonMessages.Notifications;
using MediatR;

namespace FluxoCaixaArq.Core.Communication.Mediator;

public class MediatorHandler : IMediatorHandler
{
    private readonly IMediator _mediator;

    public MediatorHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task PublicarEventoAsync<T>(T evento) where T : Event => await _mediator.Publish(evento);

    public async Task<bool> EnviarComando<T>(T? command) where T : Command => await _mediator.Send(command);

    public async Task PublicarNotificacoes<T>(T notification) where T : DomainNotification =>
        await _mediator.Publish(notification);
}