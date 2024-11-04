using System.Reflection;
using FluxoCaixaArq.Core.Data;
using FluxoCaixaArq.Core.Messages;
using FluxoCaixaArq.FluxoCaixa.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FluxoCaixaArq.FluxoCaixa.Data;

public class FluxoCaixaContext(DbContextOptions<FluxoCaixaContext> options)
    : DbContext(options), IUnitOfWork
{
    public DbSet<Lancamento> Lancamentos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes()
                     .SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        modelBuilder.Ignore<Event>();

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    public static void Seed(FluxoCaixaContext? context)
    {
        context.Lancamentos.AddRange(
            Lancamento.LancamentoFactory.NovoLancamento("Crédito seed", 35.99m, DateTime.Today.AddDays(-1)),
            Lancamento.LancamentoFactory.NovoLancamento("Crédito seed", 159.90m, DateTime.Today.AddDays(-1)),
            Lancamento.LancamentoFactory.NovoLancamento("Crédito seed", 450, DateTime.Today.AddDays(-1)),
            Lancamento.LancamentoFactory.NovoLancamento("Crédito seed", 329.50m, DateTime.Today.AddDays(-1)),
            Lancamento.LancamentoFactory.NovoLancamento("Crédito seed", 159.99m, DateTime.Today.AddDays(-1)),
            Lancamento.LancamentoFactory.NovoLancamento("Crédito seed", 259.80m, DateTime.Today.AddDays(-1)),
            Lancamento.LancamentoFactory.NovoLancamento("Crédito seed", 129.90m, DateTime.Today.AddDays(-1)),
            Lancamento.LancamentoFactory.NovoLancamento("Crédito seed", 59.99m, DateTime.Today.AddDays(-1)),
            Lancamento.LancamentoFactory.NovoLancamento("Crédito seed", 10, DateTime.Today.AddDays(-1)),
            Lancamento.LancamentoFactory.NovoLancamento("Crédito seed", 85.50m, DateTime.Today.AddDays(-1)),
            Lancamento.LancamentoFactory.NovoLancamento("Débito seed", -10, DateTime.Today.AddDays(-1)),
            Lancamento.LancamentoFactory.NovoLancamento("Débito seed", -15, DateTime.Today.AddDays(-1)),
            Lancamento.LancamentoFactory.NovoLancamento("Débito seed", -15, DateTime.Today.AddDays(-1)),
            Lancamento.LancamentoFactory.NovoLancamento("Débito seed", -10, DateTime.Today.AddDays(-1))
        );

        context.SaveChanges();
    }

    public async Task<bool> Commit()
    {
        {
            foreach (var entry in ChangeTracker.Entries()
                         .Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }

            return await SaveChangesAsync() > 0;
        }
    }
}