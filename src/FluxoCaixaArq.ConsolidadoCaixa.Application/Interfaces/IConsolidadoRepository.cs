using FluxoCaixaArq.ConsolidadoCaixa.Application.ViewModel;

namespace FluxoCaixaArq.ConsolidadoCaixa.Application.Interfaces;

public interface IConsolidadoRepository
{
    Task<ConsolidadoViewModel> ObterConsolidadoOntemAsync();
    Task<ConsolidadoViewModel> ObterConsolidadoPorDataAsync(DateTime data);
}