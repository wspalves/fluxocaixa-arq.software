using FluxoCaixaArq.ConsolidadoCaixa.Application.Queries.ViewModel;
using FluxoCaixaArq.Core.DomainObjects;
using FluxoCaixaArq.FluxoCaixa.Domain.Interfaces;

namespace FluxoCaixaArq.ConsolidadoCaixa.Application.Queries;

public class LancamentoQueries : ILancamentoQueries
{
    private readonly ILancamentoRepository _lancamentoRepository;

    public LancamentoQueries(ILancamentoRepository lancamentoRepository)
    {
        _lancamentoRepository = lancamentoRepository;
    }

    public async Task<LancamentoViewModel> ObterConsolidadoOntemAsync()
    {
        var lancamentos = await _lancamentoRepository.ObterTodosOntem();
        if (!lancamentos.Any())
            return new LancamentoViewModel
                { Valor = 0, DataCadastro = DateTime.Now.Date.AddDays(-1), QuantidadeLancamentos = 0 };

        return new LancamentoViewModel
        {
            Valor = lancamentos.Sum((x => x.Valor)),
            DataCadastro = lancamentos.FirstOrDefault().DataCadastro,
            QuantidadeLancamentos = lancamentos.Count(),
        };
    }

    public async Task<LancamentoViewModel> ObterConsolidadoPorDataAsync(DateTime data)
    {
        Validacoes.ValidarSeDataMaiorQueHoje(data, "A data informada deve ser menor ou igual ao dia de hoje!");

        var lancamentos = await _lancamentoRepository.ObterTodosPorData(data);
        if (!lancamentos.Any())
            return new LancamentoViewModel
                { Valor = 0, DataCadastro = data.Date, QuantidadeLancamentos = 0 };

        return new LancamentoViewModel
        {
            Valor = lancamentos.Sum((x => x.Valor)),
            DataCadastro = lancamentos.FirstOrDefault().DataCadastro,
            QuantidadeLancamentos = lancamentos.Count(),
        };
    }
}