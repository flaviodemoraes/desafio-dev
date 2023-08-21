using DesafioDev.Operacoes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioDev.Operacoes.Infra.Mappings
{
    public class OperacaoMap : IEntityTypeConfiguration<Operacao>
    {

        public void Configure(EntityTypeBuilder<Operacao> builder)
        {
            builder.ToTable("Operacoes");

            builder.HasKey(c => c.OperacaoId);

            builder.Property(c => c.DataOcorrencia)
                .IsRequired();

            builder.Property(c => c.Valor)
                .IsRequired();

            builder.Property(c => c.Cpf)
                .HasColumnType("char(11)")
                .HasMaxLength(11)
                .IsUnicode()
                .IsRequired();

            builder.Property(c => c.CartaoTransacao)
                .HasColumnType("char(12)")
                .HasMaxLength(12)
                .IsRequired();

            builder.Property(c => c.HoraOcorrencia)
                .HasColumnType("char(8)")
                .HasMaxLength(8)
                .IsRequired();
        }
    }
}
