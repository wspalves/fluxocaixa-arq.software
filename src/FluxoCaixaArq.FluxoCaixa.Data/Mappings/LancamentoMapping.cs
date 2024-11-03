using FluxoCaixaArq.FluxoCaixa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluxoCaixaArq.FluxoCaixa.Data.Mappings;

public class LancamentoMapping : IEntityTypeConfiguration<Lancamento>
{
    public void Configure(EntityTypeBuilder<Lancamento> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Descricao)
            .IsRequired()
            .HasColumnType("varchar(200)");

        builder.ToTable("Lancamentos");
    }
}