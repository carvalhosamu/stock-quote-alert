using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace stock_quote_alert.Migrations
{
    public partial class TesteMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Consultas",
                columns: table => new
                {
                    IdConsulta = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomeAcao = table.Column<string>(type: "TEXT", nullable: true),
                    DtaExecucao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ValorApurado = table.Column<double>(type: "REAL", nullable: false),
                    MercadoAberto = table.Column<bool>(type: "INTEGER", nullable: true),
                    RetornouResultados = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultas", x => x.IdConsulta);
                });

            migrationBuilder.CreateTable(
                name: "EnvioEmail",
                columns: table => new
                {
                    IdEmail = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdConsulta = table.Column<long>(type: "INTEGER", nullable: false),
                    EmailEnviado = table.Column<string>(type: "TEXT", nullable: true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    TipoEnvio = table.Column<string>(type: "TEXT", maxLength: 1, nullable: true),
                    DtaEnvio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    StatusEnviado = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnvioEmail", x => x.IdEmail);
                    table.ForeignKey(
                        name: "FK_EnvioEmail_Consultas_IdConsulta",
                        column: x => x.IdConsulta,
                        principalTable: "Consultas",
                        principalColumn: "IdConsulta",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnvioEmail_IdConsulta",
                table: "EnvioEmail",
                column: "IdConsulta",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnvioEmail");

            migrationBuilder.DropTable(
                name: "Consultas");
        }
    }
}
