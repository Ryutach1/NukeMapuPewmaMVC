using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoNukeMapuPewmaMVC.Migrations
{
    /// <inheritdoc />
    public partial class MigracionImagenes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Ropa",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagenRuta",
                table: "Ropa",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Otros",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagenRuta",
                table: "Otros",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagenRuta",
                table: "Libro",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagenRuta",
                table: "Artesania",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Ropa");

            migrationBuilder.DropColumn(
                name: "ImagenRuta",
                table: "Ropa");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Otros");

            migrationBuilder.DropColumn(
                name: "ImagenRuta",
                table: "Otros");

            migrationBuilder.DropColumn(
                name: "ImagenRuta",
                table: "Libro");

            migrationBuilder.DropColumn(
                name: "ImagenRuta",
                table: "Artesania");
        }
    }
}
