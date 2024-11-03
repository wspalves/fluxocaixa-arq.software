namespace FluxoCaixaArq.Core.Messages;

public class Message
{
    public Message()
    {
        MessageType = GetType().Name;
    }

    public string MessageType { get; private set; }
    public Guid AggregateId { get; protected set; }
}