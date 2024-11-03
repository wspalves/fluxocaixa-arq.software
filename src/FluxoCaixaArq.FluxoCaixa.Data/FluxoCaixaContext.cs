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