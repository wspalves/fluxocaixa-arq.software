using FluxoCaixaArq.Core.DomainObjects;

namespace FluxoCaixaArq.FluxoCaixa.Domain.Entities;

public class Debito : Lancamento
{
    protected Debito(string descricao, decimal valor) : base(descricao, valor)
    {
    }

    public override void Validar()
    {
        Validacoes.ValidarSeMaiorIgualZero(Valor,
            "Para lançamentos de débito, o valor deve ser menor que zero.");

        Validacoes.ValidarSeStringVazia(Descricao,
            "Para lançamentos, a descrição deve ser preenchida.");
    }


    public static class LancamentoFactory
    {
        public static Lancamento NovoDebito(string descricao, decimal valor)
        {
            var debito = new Debito(descricao, (valor * -1));
            debito.Validar();

            return debito;
        }
    }
}