using FluxoCaixaArq.Core.Data;
using FluxoCaixaArq.FluxoCaixa.Domain.Entities;

namespace FluxoCaixaArq.FluxoCaixa.Domain.Interfaces;

public interface ILancamentoRepository : IRepository<Lancamento>
{
    Task AdicionarAsync(Lancamento lancamento);
    Task<IEnumerable<Lancamento>> ObterTodosOntem();
    Task<IEnumerable<Lancamento>> ObterTodosPorData(DateTime data);
}