using FluentValidation.Results;
using MediatR;

namespace FluxoCaixaArq.Core.Messages;

public abstract class Command : Message, IRequest<bool>
{
    protected Command()
    {
        Timespan = DateTime.Now;
    }

    public DateTime Timespan { get; set; }

    public ValidationResult ValidationResult { get; set; }

    public virtual bool EhValido()
    {
        throw new NotImplementedException();
    }
}