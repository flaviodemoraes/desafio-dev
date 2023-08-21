using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DesafioDev.Operacoes.Infra.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Lojas",
                columns: table => new
                {
                    LojaId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    NomeLoja = table.Column<string>(type: "varchar(19)", maxLength: 19, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NomeProprietario = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lojas", x => x.LojaId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TipoTransacoes",
                columns: table => new
                {
                    Tipo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Natureza = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Sinal = table.Column<string>(type: "char(1)", maxLength: 1, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoTransacoes", x => x.Tipo);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Operacoes",
                columns: table => new
                {
                    OperacaoId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TipoTransacaoId = table.Column<int>(type: "int", nullable: false),
                    LojaId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DataOcorrencia = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Cpf = table.Column<string>(type: "char(11)", maxLength: 11, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CartaoTransacao = table.Column<string>(type: "char(12)", maxLength: 12, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HoraOcorrencia = table.Column<string>(type: "char(8)", maxLength: 8, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operacoes", x => x.OperacaoId);
                    table.ForeignKey(
                        name: "FK_Operacoes_Lojas_LojaId",
                        column: x => x.LojaId,
                        principalTable: "Lojas",
                        principalColumn: "LojaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Operacoes_TipoTransacoes_TipoTransacaoId",
                        column: x => x.TipoTransacaoId,
                        principalTable: "TipoTransacoes",
                        principalColumn: "Tipo",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "TipoTransacoes",
                columns: new[] { "Tipo", "Descricao", "Natureza", "Sinal" },
                values: new object[,]
                {
                    { 1, "Débito", "Entrada", "+" },
                    { 2, "Boleto", "Saída", "-" },
                    { 3, "Financiamento", "Saída", "-" },
                    { 4, "Crédito", "Entrada", "+" },
                    { 5, "Recebimento Empréstimo", "Entrada", "+" },
                    { 6, "Vendas", "Entrada", "+" },
                    { 7, "Recebimento TED", "Entrada", "+" },
                    { 8, "Recebimento DOC", "Entrada", "+" },
                    { 9, "Aluguel", "Saída", "-" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lojas_NomeLoja",
                table: "Lojas",
                column: "NomeLoja",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Operacoes_LojaId",
                table: "Operacoes",
                column: "LojaId");

            migrationBuilder.CreateIndex(
                name: "IX_Operacoes_TipoTransacaoId",
                table: "Operacoes",
                column: "TipoTransacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoTransacoes_Tipo",
                table: "TipoTransacoes",
                column: "Tipo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Operacoes");

            migrationBuilder.DropTable(
                name: "Lojas");

            migrationBuilder.DropTable(
                name: "TipoTransacoes");
        }
    }
}
