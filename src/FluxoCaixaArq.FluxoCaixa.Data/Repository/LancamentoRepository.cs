using FluxoCaixaArq.Core.Data;
using FluxoCaixaArq.FluxoCaixa.Domain.Entities;
using FluxoCaixaArq.FluxoCaixa.Domain.Interfaces;

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

    public void Dispose()
    {
        _context.Dispose();
    }
}