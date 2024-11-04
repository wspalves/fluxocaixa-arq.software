using FluxoCaixaArq.Core.Data;
using FluxoCaixaArq.FluxoCaixa.Domain.Entities;
using FluxoCaixaArq.FluxoCaixa.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FluxoCaixaArq.FluxoCaixa.Data.Repository;

public class LancamentoRepository : ILancamentoRepository
{
    private readonly FluxoCaixaContext _context;

    public LancamentoRepository(FluxoCaixaContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task AdicionarAsync(Lancamento lancamento)
    {
        await _context.Lancamentos.AddAsync(lancamento);
    }

    public async Task<IEnumerable<Lancamento>> ObterTodosOntem()
    {
        return await _context.Lancamentos.Where(x => x.DataCadastro.Date == DateTime.Today.AddDays(-1)).ToListAsync();
    }

    public async Task<IEnumerable<Lancamento>> ObterTodosPorData(DateTime data)
    {
        return await _context.Lancamentos.Where(x => x.DataCadastro.Date == data.Date).ToListAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}