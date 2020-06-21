using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SorteadorCore.Infra.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Participante",
                columns: table => new
                {
                    ParticipanteId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nome = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participante", x => x.ParticipanteId);
                });

            migrationBuilder.CreateTable(
                name: "Sala",
                columns: table => new
                {
                    SalaId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nome = table.Column<string>(type: "VARCHAR", maxLength: 150, nullable: false),
                    QuantidadeVencedoresMaioresPontos = table.Column<int>(nullable: false),
                    QuantidadeVencedoresMenoresPontos = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sala", x => x.SalaId);
                });

            migrationBuilder.CreateTable(
                name: "Sorteio",
                columns: table => new
                {
                    SorteioId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    SalaId = table.Column<int>(nullable: false),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    DataEncerramento = table.Column<DateTime>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sorteio", x => x.SorteioId);
                    table.ForeignKey(
                        name: "FK_Sorteio_Sala_SalaId",
                        column: x => x.SalaId,
                        principalTable: "Sala",
                        principalColumn: "SalaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SorteioDetalhe",
                columns: table => new
                {
                    SorteioDetalheId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    SorteioId = table.Column<int>(nullable: false),
                    ParticipanteId = table.Column<int>(nullable: false),
                    EnderecoIP = table.Column<string>(type: "VARCHAR", maxLength: 150, nullable: false),
                    Pontos = table.Column<int>(nullable: false),
                    DataParticipacao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SorteioDetalhe", x => x.SorteioDetalheId);
                    table.ForeignKey(
                        name: "FK_SorteioDetalhe_Participante_ParticipanteId",
                        column: x => x.ParticipanteId,
                        principalTable: "Participante",
                        principalColumn: "ParticipanteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SorteioDetalhe_Sorteio_SorteioId",
                        column: x => x.SorteioId,
                        principalTable: "Sorteio",
                        principalColumn: "SorteioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sorteio_SalaId",
                table: "Sorteio",
                column: "SalaId");

            migrationBuilder.CreateIndex(
                name: "IX_SorteioDetalhe_ParticipanteId",
                table: "SorteioDetalhe",
                column: "ParticipanteId");

            migrationBuilder.CreateIndex(
                name: "IX_SorteioDetalhe_SorteioId",
                table: "SorteioDetalhe",
                column: "SorteioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SorteioDetalhe");

            migrationBuilder.DropTable(
                name: "Participante");

            migrationBuilder.DropTable(
                name: "Sorteio");

            migrationBuilder.DropTable(
                name: "Sala");
        }
    }
}
