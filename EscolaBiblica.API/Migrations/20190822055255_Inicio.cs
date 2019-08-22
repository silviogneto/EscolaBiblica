using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaBiblica.API.Migrations
{
    public partial class Inicio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Setor",
                columns: table => new
                {
                    Numero = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setor", x => x.Numero);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    Login = table.Column<string>(nullable: false),
                    Senha = table.Column<string>(nullable: false),
                    Perfil = table.Column<string>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Congregacao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false),
                    SetorNumero = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Congregacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Congregacao_Setor_SetorNumero",
                        column: x => x.SetorNumero,
                        principalTable: "Setor",
                        principalColumn: "Numero",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Aluno",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "date", nullable: false),
                    Telefone = table.Column<string>(nullable: true),
                    CongregacaoId = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    UsuarioId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aluno", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aluno_Congregacao_CongregacaoId",
                        column: x => x.CongregacaoId,
                        principalTable: "Congregacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Aluno_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Classe",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    CongregacaoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classe_Congregacao_CongregacaoId",
                        column: x => x.CongregacaoId,
                        principalTable: "Congregacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CEP = table.Column<string>(nullable: false),
                    DescEndereco = table.Column<string>(nullable: false),
                    Numero = table.Column<int>(nullable: false),
                    Complemento = table.Column<string>(nullable: true),
                    Bairro = table.Column<string>(nullable: false),
                    Cidade = table.Column<string>(nullable: false),
                    Estado = table.Column<string>(nullable: false),
                    TipoEndereco = table.Column<int>(nullable: false),
                    AlunoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Endereco_Aluno_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Aluno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Matricula",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataMatricula = table.Column<DateTime>(nullable: false),
                    DataTerminoMatricula = table.Column<DateTime>(nullable: true),
                    ClasseId = table.Column<int>(nullable: false),
                    AlunoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matricula", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matricula_Aluno_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Aluno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matricula_Classe_ClasseId",
                        column: x => x.ClasseId,
                        principalTable: "Classe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Setor",
                columns: new[] { "Numero", "Nome" },
                values: new object[,]
                {
                    { 1, "Sede" },
                    { 25, "Jordão" },
                    { 24, "Ristow" },
                    { 23, "Pérola do Vale" },
                    { 22, "Cidade Jardim" },
                    { 21, "Rua das Missões" },
                    { 20, "América do Sol" },
                    { 19, "Água Verde" },
                    { 18, "Moriá" },
                    { 17, "Vila Itoupava" },
                    { 16, "Morell" },
                    { 15, "Itoupava Norte" },
                    { 26, "Pedro Krauss" },
                    { 14, "Araranguá" },
                    { 12, "Progresso" },
                    { 11, "Velha Grande" },
                    { 10, "Betesda" },
                    { 9, "Nova Jerusalém" },
                    { 8, "Betânia" },
                    { 7, "Itoupava Central" },
                    { 6, "Velha Central" },
                    { 5, "Escola Agrícola" },
                    { 4, "Fortaleza" },
                    { 3, "Badenfurt" },
                    { 2, "Garcia" },
                    { 13, "Jerusalém" }
                });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "Ativo", "Login", "Nome", "Perfil", "Senha" },
                values: new object[] { 1, true, "silviogneto", "Silvio Neto", "ADMIN", "gQKeZEq2lKP+47IczeNqpjD3LJBCrA==" });

            migrationBuilder.InsertData(
                table: "Congregacao",
                columns: new[] { "Id", "Nome", "SetorNumero" },
                values: new object[,]
                {
                    { 1, "Garcia", 2 },
                    { 2, "Engenheiro Odebrecht", 2 },
                    { 3, "Glória", 2 },
                    { 4, "Hermann Huscher", 2 },
                    { 5, "Itapuí", 2 },
                    { 6, "Zendron", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_CongregacaoId",
                table: "Aluno",
                column: "CongregacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_UsuarioId",
                table: "Aluno",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Classe_CongregacaoId",
                table: "Classe",
                column: "CongregacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Congregacao_SetorNumero",
                table: "Congregacao",
                column: "SetorNumero");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_AlunoId",
                table: "Endereco",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Matricula_AlunoId",
                table: "Matricula",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Matricula_ClasseId",
                table: "Matricula",
                column: "ClasseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Endereco");

            migrationBuilder.DropTable(
                name: "Matricula");

            migrationBuilder.DropTable(
                name: "Aluno");

            migrationBuilder.DropTable(
                name: "Classe");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Congregacao");

            migrationBuilder.DropTable(
                name: "Setor");
        }
    }
}
