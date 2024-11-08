using FluxoCaixaArq.ConsolidadoCaixa.Application.ViewModel;

namespace FluxoCaixaArq.ConsolidadoCaixa.Application.Interfaces;

public interface IConsolidadoRepository
{
    Task<ConsolidadoViewModel> ObterConsolidadoPorDataAsync(DateTime data);
}