using FluxoCaixaArq.ConsolidadoCaixa.Application.Interfaces;
using FluxoCaixaArq.ConsolidadoCaixa.Application.ViewModel;

namespace FluxoCaixaArq.ConsolidadoCaixa.Application.Queries;

public class ConsolidadoQueries : IConsolidadoQueries
{
    private readonly IConsolidadoRepository _consolidadoRepository;

    public ConsolidadoQueries(IConsolidadoRepository consolidadoRepository)
    {
        _consolidadoRepository = consolidadoRepository;
    }

    public async Task<ConsolidadoViewModel> ObterConsolidadoOntemAsync()
    {
        var consolidado = await _consolidadoRepository.ObterConsolidadoOntemAsync();
        if (consolidado == null)
            return new ConsolidadoViewModel
                { Valor = 0, DataCadastro = DateTime.Now.Date.AddDays(-1), QuantidadeLancamentos = 0 };

        return consolidado;
    }

    public async Task<ConsolidadoViewModel> ObterConsolidadoPorDataAsync(DateTime data)
    {
        var consolidado = await _consolidadoRepository.ObterConsolidadoPorDataAsync(data);
        if (consolidado == null)
            return new ConsolidadoViewModel
                { Valor = 0, DataCadastro = DateTime.Now.Date.AddDays(-1), QuantidadeLancamentos = 0 };

        return consolidado;
    }
}