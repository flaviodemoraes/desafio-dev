using DesafioDev.Operacoes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioDev.Operacoes.Infra.Mappings
{
    public class LojaMap : IEntityTypeConfiguration<Loja>
    {
        public void Configure(EntityTypeBuilder<Loja> builder)
        {
            builder.ToTable("Lojas");

            builder.HasKey(c => c.LojaId);

            builder.Property(c => c.NomeLoja)
               .HasColumnType("varchar(19)")
               .HasMaxLength(19)
               .IsRequired();

            builder.Property(c => c.NomeProprietario)
               .HasColumnType("varchar(14)")
               .HasMaxLength(14)
               .IsRequired();

            // 1 : N => Loja : Operacao
            builder.HasMany(c => c.Operacao)
                .WithOne(p => p.Loja)
                .HasForeignKey(p => p.LojaId);
        }
    }
}
