namespace FluxoCaixaArq.Core.Data;

public interface IUnitOfWork
{
    Task<bool> Commit();
}