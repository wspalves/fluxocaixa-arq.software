using FluxoCaixaArq.ConsolidadoCaixa.Application.ViewModel;

namespace FluxoCaixaArq.ConsolidadoCaixa.Application.Queries;

public interface IConsolidadoQueries
{   
    Task<ConsolidadoViewModel> ObterConsolidadoPorDataAsync(DateTime data);
}