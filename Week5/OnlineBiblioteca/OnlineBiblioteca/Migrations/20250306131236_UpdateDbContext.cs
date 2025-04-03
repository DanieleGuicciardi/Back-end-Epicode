using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Biblioteca.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Libri",
                keyColumn: "Id",
                keyValue: new Guid("880dce35-0f87-418d-ac89-5ce9ba864d59"));

            migrationBuilder.DeleteData(
                table: "Libri",
                keyColumn: "Id",
                keyValue: new Guid("bcc8de3e-5b5c-468d-b5fd-6edff8e506c3"));

            migrationBuilder.DeleteData(
                table: "Libri",
                keyColumn: "Id",
                keyValue: new Guid("ca5500c7-7098-4d63-bb91-245b494b8210"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Libri",
                columns: new[] { "Id", "Autore", "Disponibile", "Genere", "PercorsoImmagineCopertina", "Titolo" },
                values: new object[,]
                {
                    { new Guid("880dce35-0f87-418d-ac89-5ce9ba864d59"), "George Orwell", true, "Distopico", null, "1984" },
                    { new Guid("bcc8de3e-5b5c-468d-b5fd-6edff8e506c3"), "Antoine de Saint-Exupéry", true, "Favola", null, "Il Piccolo Principe" },
                    { new Guid("ca5500c7-7098-4d63-bb91-245b494b8210"), "Umberto Eco", true, "Storico", null, "Il Nome della Rosa" }
                });
        }
    }
}
