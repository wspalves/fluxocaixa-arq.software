using FluxoCaixaArq.Core.DomainObjects;

namespace FluxoCaixaArq.FluxoCaixa.Domain.Entities;

public class Credito : Lancamento
{
    protected Credito(string descricao, decimal valor) : base(descricao, valor)
    {
        Validar();
    }

    public override void Validar()
    {
        Validacoes.ValidarSeMenorIgualZero(Valor,
            "Para lançamentos de crédito, o valor deve ser maior que zero.");

        Validacoes.ValidarSeStringVazia(Descricao,
            "Para lançamentos, a descrição deve ser preenchida.");
    }


    public static class LancamentoFactory
    {
        public static Lancamento NovoCredito(string descricao, decimal valor)
        {
            var credito = new Credito(descricao, valor);
            credito.Validar();

            return credito;
        }
    }
}