using DesafioDev.Operacoes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioDev.Operacoes.Infra.Mappings
{
    public class TipoTransacaoMap : IEntityTypeConfiguration<TipoTransacao>
    {
        public void Configure(EntityTypeBuilder<TipoTransacao> builder)
        {

            builder.ToTable("TipoTransacoes");

            builder.HasKey(c => c.Tipo);

            builder.Property(c => c.Descricao)
               .HasColumnType("varchar(20)")
               .HasMaxLength(20)
               .IsRequired();

            builder.Property(c => c.Natureza)
               .HasColumnType("varchar(20)")
               .HasMaxLength(20)
               .IsRequired();

            builder.Property(c => c.Sinal)
              .HasColumnType("char(1)")
              .HasMaxLength(1)
              .IsRequired();

            // 1 : N => TipoTransacao : Operacao
            builder.HasMany(c => c.Operacao)
                .WithOne(p => p.TipoTransacao)
                .HasForeignKey(p => p.TipoTransacaoId);
        }
    }
}
