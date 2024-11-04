using FluxoCaixaArq.ConsolidadoCaixa.Application.Queries.ViewModel;

namespace FluxoCaixaArq.ConsolidadoCaixa.Application.Queries;

public interface ILancamentoQueries
{
    Task<LancamentoViewModel> ObterConsolidadoOntemAsync();
    Task<LancamentoViewModel> ObterConsolidadoPorDataAsync(DateTime data);
}