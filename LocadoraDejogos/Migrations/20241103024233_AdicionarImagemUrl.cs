using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocadoraDejogos.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarImagemUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CPF = table.Column<string>(type: "TEXT", nullable: true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    DataNascimento = table.Column<string>(type: "TEXT", nullable: true),
                    Telefone = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Consoles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Fabricante = table.Column<string>(type: "TEXT", nullable: true),
                    Geracao = table.Column<int>(type: "INTEGER", nullable: true),
                    Ano = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consoles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CPF = table.Column<string>(type: "TEXT", nullable: true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    DataNascimento = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Jogos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Desenvolvedor = table.Column<string>(type: "TEXT", nullable: true),
                    Distribuidora = table.Column<string>(type: "TEXT", nullable: true),
                    Genero = table.Column<string>(type: "TEXT", nullable: true),
                    Ano = table.Column<int>(type: "INTEGER", nullable: true),
                    Unidade = table.Column<int>(type: "INTEGER", nullable: true),
                    ConsoleID = table.Column<int>(type: "INTEGER", nullable: true),
                    ImagemURL = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jogos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Jogos_Consoles_ConsoleID",
                        column: x => x.ConsoleID,
                        principalTable: "Consoles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alugueis",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClienteID = table.Column<int>(type: "INTEGER", nullable: true),
                    JogoID = table.Column<int>(type: "INTEGER", nullable: true),
                    FuncionarioID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alugueis", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Alugueis_Clientes_ClienteID",
                        column: x => x.ClienteID,
                        principalTable: "Clientes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Alugueis_Funcionarios_FuncionarioID",
                        column: x => x.FuncionarioID,
                        principalTable: "Funcionarios",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Alugueis_Jogos_JogoID",
                        column: x => x.JogoID,
                        principalTable: "Jogos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JogosConsoles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    JogoID = table.Column<int>(type: "INTEGER", nullable: true),
                    ConsoleID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JogosConsoles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_JogosConsoles_Consoles_ConsoleID",
                        column: x => x.ConsoleID,
                        principalTable: "Consoles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JogosConsoles_Jogos_JogoID",
                        column: x => x.JogoID,
                        principalTable: "Jogos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alugueis_ClienteID",
                table: "Alugueis",
                column: "ClienteID");

            migrationBuilder.CreateIndex(
                name: "IX_Alugueis_FuncionarioID",
                table: "Alugueis",
                column: "FuncionarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Alugueis_JogoID",
                table: "Alugueis",
                column: "JogoID");

            migrationBuilder.CreateIndex(
                name: "IX_Jogos_ConsoleID",
                table: "Jogos",
                column: "ConsoleID");

            migrationBuilder.CreateIndex(
                name: "IX_JogosConsoles_ConsoleID",
                table: "JogosConsoles",
                column: "ConsoleID");

            migrationBuilder.CreateIndex(
                name: "IX_JogosConsoles_JogoID",
                table: "JogosConsoles",
                column: "JogoID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alugueis");

            migrationBuilder.DropTable(
                name: "JogosConsoles");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Funcionarios");

            migrationBuilder.DropTable(
                name: "Jogos");

            migrationBuilder.DropTable(
                name: "Consoles");
        }
    }
}
