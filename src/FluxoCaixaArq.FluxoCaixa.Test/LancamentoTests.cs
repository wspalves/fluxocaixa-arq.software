using FluxoCaixaArq.Core.DomainObjects;
using FluxoCaixaArq.FluxoCaixa.Domain.Entities;

namespace FluxoCaixaArq.FluxoCaixa.Test;

public class LancamentoTests
{
    [Fact]
    public void Credito_Validar_ValidacoesDevemRetornarException()
    {
        var exec = Assert.Throws<DomainException>(() => Credito.LancamentoFactory.NovoCredito("", 10));
        Assert.Equal("Para lançamentos, a descrição deve ser preenchida.", exec.Message);
        
        exec = Assert.Throws<DomainException>(() => Credito.LancamentoFactory.NovoCredito("Crédito", 0));
        Assert.Equal("Para lançamentos de crédito, o valor deve ser maior que zero.", exec.Message);        
    }
}