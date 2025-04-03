using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Biblioteca.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Libri",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titolo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Autore = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Genere = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Disponibile = table.Column<bool>(type: "bit", nullable: false),
                    PercorsoImmagineCopertina = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Libri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prestiti",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeUtente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailUtente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataPrestito = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataRestituzione = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataRestituzioneEffettiva = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LibroId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prestiti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prestiti_Libri_LibroId",
                        column: x => x.LibroId,
                        principalTable: "Libri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Libri",
                columns: new[] { "Id", "Autore", "Disponibile", "Genere", "PercorsoImmagineCopertina", "Titolo" },
                values: new object[,]
                {
                    { new Guid("880dce35-0f87-418d-ac89-5ce9ba864d59"), "George Orwell", true, "Distopico", null, "1984" },
                    { new Guid("bcc8de3e-5b5c-468d-b5fd-6edff8e506c3"), "Antoine de Saint-Exupéry", true, "Favola", null, "Il Piccolo Principe" },
                    { new Guid("ca5500c7-7098-4d63-bb91-245b494b8210"), "Umberto Eco", true, "Storico", null, "Il Nome della Rosa" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prestiti_LibroId",
                table: "Prestiti",
                column: "LibroId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prestiti");

            migrationBuilder.DropTable(
                name: "Libri");
        }
    }
}
