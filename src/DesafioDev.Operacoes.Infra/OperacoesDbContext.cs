using DesafioDev.Core.Data;
using DesafioDev.Operacoes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DesafioDev.Operacoes.Infra
{
    public class OperacoesDbContext : DbContext, IUnitOfWork
    {
        private IDbContextTransaction _transaction;

        public OperacoesDbContext(DbContextOptions<OperacoesDbContext> options) : base(options) { }

        public virtual DbSet<Loja> Lojas { get; set; }
        public virtual DbSet<Operacao> Operacoes { get; set; }
        public virtual DbSet<TipoTransacao> TiposTransacoes { get; set; }

        public async Task BeginTransactionAsync()
        {
            _transaction = await Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await _transaction.CommitAsync();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public async Task<int> SaveChanges()
        {
            return await SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OperacoesDbContext).Assembly);

            #region TipoTransacao[IsUnique]
            modelBuilder.Entity<TipoTransacao>().HasIndex(c => c.Tipo).IsUnique();
            #endregion

            #region Loja[IsUnique]
            modelBuilder.Entity<Loja>().HasIndex(c => c.NomeLoja).IsUnique();
            #endregion

            #region TipoTransacao[Insert]
            modelBuilder.Entity<TipoTransacao>().HasData(
                new TipoTransacao(1, "Débito", "Entrada", "+"),
                new TipoTransacao(2, "Boleto", "Saída", "-"),
                new TipoTransacao(3, "Financiamento", "Saída", "-"),
                new TipoTransacao(4, "Crédito", "Entrada", "+"),
                new TipoTransacao(5, "Recebimento Empréstimo", "Entrada", "+"),
                new TipoTransacao(6, "Vendas", "Entrada", "+"),
                new TipoTransacao(7, "Recebimento TED", "Entrada", "+"),
                new TipoTransacao(8, "Recebimento DOC", "Entrada", "+"),
                new TipoTransacao(9, "Aluguel", "Saída", "-")
            );
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
