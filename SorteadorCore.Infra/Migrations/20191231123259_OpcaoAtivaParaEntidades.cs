using Microsoft.EntityFrameworkCore.Migrations;

namespace SorteadorCore.Infra.Migrations
{
    public partial class OpcaoAtivaParaEntidades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ParticipacaoValida",
                table: "SorteioDetalhe",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Sala",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParticipacaoValida",
                table: "SorteioDetalhe");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Sala");
        }
    }
}
