namespace FluxoCaixaArq.Core.DomainObjects;

public static class Validacoes
{
    public static void ValidarSeMaiorIgualZero(decimal valor, string mensagem)
    {
        if (valor >= 0)
            throw new DomainException(mensagem);
    }

    public static void ValidarSeMenorIgualZero(decimal valor, string mensagem)
    {
        if (valor <= 0)
            throw new DomainException(mensagem);
    }

    public static void ValidarSeStringVazia(string valor, string mensagem)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new DomainException(mensagem);
    }

    public static void ValidarSeDataMaiorQueHoje(DateTime data, string mensagem)
    {
        if (data.Date > DateTime.Today)
            throw new DomainException(mensagem);
    }
}