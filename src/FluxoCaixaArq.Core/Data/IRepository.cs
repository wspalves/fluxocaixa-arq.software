using FluxoCaixaArq.Core.DomainObjects;

namespace FluxoCaixaArq.Core.Data;

public interface IRepository<T> : IDisposable where T : IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }
}   