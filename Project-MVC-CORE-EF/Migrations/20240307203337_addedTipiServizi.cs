using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_MVC_CORE_EF.Migrations
{
    /// <inheritdoc />
    public partial class addedTipiServizi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoServizio",
                table: "Servizi");

            migrationBuilder.AddColumn<int>(
                name: "IdTipoServizio",
                table: "Servizi",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TipiServizi",
                columns: table => new
                {
                    IdTipoServizio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeTipoServizio = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipiServizi", x => x.IdTipoServizio);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Servizi_IdTipoServizio",
                table: "Servizi",
                column: "IdTipoServizio");

            migrationBuilder.AddForeignKey(
                name: "FK_Servizi_TipiServizi_IdTipoServizio",
                table: "Servizi",
                column: "IdTipoServizio",
                principalTable: "TipiServizi",
                principalColumn: "IdTipoServizio",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servizi_TipiServizi_IdTipoServizio",
                table: "Servizi");

            migrationBuilder.DropTable(
                name: "TipiServizi");

            migrationBuilder.DropIndex(
                name: "IX_Servizi_IdTipoServizio",
                table: "Servizi");

            migrationBuilder.DropColumn(
                name: "IdTipoServizio",
                table: "Servizi");

            migrationBuilder.AddColumn<string>(
                name: "TipoServizio",
                table: "Servizi",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
