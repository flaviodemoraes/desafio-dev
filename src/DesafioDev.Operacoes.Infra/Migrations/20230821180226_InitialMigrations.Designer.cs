﻿// <auto-generated />
using System;
using DesafioDev.Operacoes.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DesafioDev.Operacoes.Infra.Migrations
{
    [DbContext(typeof(OperacoesDbContext))]
    [Migration("20230821180226_InitialMigrations")]
    partial class InitialMigrations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DesafioDev.Operacoes.Domain.Entities.Loja", b =>
                {
                    b.Property<Guid>("LojaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("NomeLoja")
                        .IsRequired()
                        .HasMaxLength(19)
                        .HasColumnType("varchar(19)");

                    b.Property<string>("NomeProprietario")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("varchar(14)");

                    b.HasKey("LojaId");

                    b.HasIndex("NomeLoja")
                        .IsUnique();

                    b.ToTable("Lojas", (string)null);
                });

            modelBuilder.Entity("DesafioDev.Operacoes.Domain.Entities.Operacao", b =>
                {
                    b.Property<Guid>("OperacaoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CartaoTransacao")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("char(12)");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(11)
                        .IsUnicode(true)
                        .HasColumnType("char(11)");

                    b.Property<DateTime>("DataOcorrencia")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("HoraOcorrencia")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("char(8)");

                    b.Property<Guid>("LojaId")
                        .HasColumnType("char(36)");

                    b.Property<int>("TipoTransacaoId")
                        .HasColumnType("int");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("OperacaoId");

                    b.HasIndex("LojaId");

                    b.HasIndex("TipoTransacaoId");

                    b.ToTable("Operacoes", (string)null);
                });

            modelBuilder.Entity("DesafioDev.Operacoes.Domain.Entities.TipoTransacao", b =>
                {
                    b.Property<int>("Tipo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Natureza")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Sinal")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("char(1)");

                    b.HasKey("Tipo");

                    b.HasIndex("Tipo")
                        .IsUnique();

                    b.ToTable("TipoTransacoes", (string)null);

                    b.HasData(
                        new
                        {
                            Tipo = 1,
                            Descricao = "Débito",
                            Natureza = "Entrada",
                            Sinal = "+"
                        },
                        new
                        {
                            Tipo = 2,
                            Descricao = "Boleto",
                            Natureza = "Saída",
                            Sinal = "-"
                        },
                        new
                        {
                            Tipo = 3,
                            Descricao = "Financiamento",
                            Natureza = "Saída",
                            Sinal = "-"
                        },
                        new
                        {
                            Tipo = 4,
                            Descricao = "Crédito",
                            Natureza = "Entrada",
                            Sinal = "+"
                        },
                        new
                        {
                            Tipo = 5,
                            Descricao = "Recebimento Empréstimo",
                            Natureza = "Entrada",
                            Sinal = "+"
                        },
                        new
                        {
                            Tipo = 6,
                            Descricao = "Vendas",
                            Natureza = "Entrada",
                            Sinal = "+"
                        },
                        new
                        {
                            Tipo = 7,
                            Descricao = "Recebimento TED",
                            Natureza = "Entrada",
                            Sinal = "+"
                        },
                        new
                        {
                            Tipo = 8,
                            Descricao = "Recebimento DOC",
                            Natureza = "Entrada",
                            Sinal = "+"
                        },
                        new
                        {
                            Tipo = 9,
                            Descricao = "Aluguel",
                            Natureza = "Saída",
                            Sinal = "-"
                        });
                });

            modelBuilder.Entity("DesafioDev.Operacoes.Domain.Entities.Operacao", b =>
                {
                    b.HasOne("DesafioDev.Operacoes.Domain.Entities.Loja", "Loja")
                        .WithMany("Operacao")
                        .HasForeignKey("LojaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DesafioDev.Operacoes.Domain.Entities.TipoTransacao", "TipoTransacao")
                        .WithMany("Operacao")
                        .HasForeignKey("TipoTransacaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Loja");

                    b.Navigation("TipoTransacao");
                });

            modelBuilder.Entity("DesafioDev.Operacoes.Domain.Entities.Loja", b =>
                {
                    b.Navigation("Operacao");
                });

            modelBuilder.Entity("DesafioDev.Operacoes.Domain.Entities.TipoTransacao", b =>
                {
                    b.Navigation("Operacao");
                });
#pragma warning restore 612, 618
        }
    }
}
