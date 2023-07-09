using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonitoramentoSaudeAPI.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    Cpf = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HistoricoMedico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicamentosEmUso = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Alergias = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Observacoes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.Cpf);
                });

            migrationBuilder.CreateTable(
                name: "LeiturasMonitoramento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PressaoArterial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BatimentosCardiacos = table.Column<int>(type: "int", nullable: false),
                    FrequenciaRespiratoria = table.Column<int>(type: "int", nullable: false),
                    SaturacaoOxigenio = table.Column<int>(type: "int", nullable: false),
                    NivelCO2 = table.Column<int>(type: "int", nullable: false),
                    Temperatura = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PacienteCpf = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeiturasMonitoramento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeiturasMonitoramento_Pacientes_PacienteCpf",
                        column: x => x.PacienteCpf,
                        principalTable: "Pacientes",
                        principalColumn: "Cpf",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeiturasMonitoramento_PacienteCpf",
                table: "LeiturasMonitoramento",
                column: "PacienteCpf");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeiturasMonitoramento");

            migrationBuilder.DropTable(
                name: "Pacientes");
        }
    }
}
