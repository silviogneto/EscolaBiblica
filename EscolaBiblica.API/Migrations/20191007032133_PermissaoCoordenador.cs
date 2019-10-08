using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaBiblica.API.Migrations
{
    public partial class PermissaoCoordenador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoordenadorCongregacao",
                columns: table => new
                {
                    CoordenadorId = table.Column<int>(nullable: false),
                    CongregacaoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoordenadorCongregacao", x => new { x.CoordenadorId, x.CongregacaoId });
                    table.ForeignKey(
                        name: "FK_CoordenadorCongregacao_Congregacao_CongregacaoId",
                        column: x => x.CongregacaoId,
                        principalTable: "Congregacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoordenadorCongregacao_Aluno_CoordenadorId",
                        column: x => x.CoordenadorId,
                        principalTable: "Aluno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoordenadorCongregacao_CongregacaoId",
                table: "CoordenadorCongregacao",
                column: "CongregacaoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoordenadorCongregacao");
        }
    }
}
