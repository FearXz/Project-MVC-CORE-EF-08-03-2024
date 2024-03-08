using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_MVC_CORE_EF.Migrations
{
    /// <inheritdoc />
    public partial class newStart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    IdAdmin = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.IdAdmin);
                });

            migrationBuilder.CreateTable(
                name: "Clienti",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCliente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CognomeCliente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodFiscale = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Citta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cellulare = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clienti", x => x.IdCliente);
                });

            migrationBuilder.CreateTable(
                name: "Pensioni",
                columns: table => new
                {
                    IdPensione = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoPensione = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CostoPensione = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pensioni", x => x.IdPensione);
                });

            migrationBuilder.CreateTable(
                name: "TipiCamere",
                columns: table => new
                {
                    IdTipoCamera = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeTipoCamera = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipiCamere", x => x.IdTipoCamera);
                });

            migrationBuilder.CreateTable(
                name: "Camere",
                columns: table => new
                {
                    IdCamera = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTipoCamera = table.Column<int>(type: "int", nullable: false),
                    NumeroCamera = table.Column<int>(type: "int", nullable: false),
                    CostoCamera = table.Column<double>(type: "float", nullable: false),
                    CameraDisponibile = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Camere", x => x.IdCamera);
                    table.ForeignKey(
                        name: "FK_Camere_TipiCamere_IdTipoCamera",
                        column: x => x.IdTipoCamera,
                        principalTable: "TipiCamere",
                        principalColumn: "IdTipoCamera",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prenotazioni",
                columns: table => new
                {
                    IdPrenotazione = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    IdCamera = table.Column<int>(type: "int", nullable: false),
                    IdPensione = table.Column<int>(type: "int", nullable: false),
                    DataInizioPrenotazione = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFinePrenotazione = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccontoPrenotazione = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prenotazioni", x => x.IdPrenotazione);
                    table.ForeignKey(
                        name: "FK_Prenotazioni_Camere_IdCamera",
                        column: x => x.IdCamera,
                        principalTable: "Camere",
                        principalColumn: "IdCamera",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prenotazioni_Clienti_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clienti",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prenotazioni_Pensioni_IdPensione",
                        column: x => x.IdPensione,
                        principalTable: "Pensioni",
                        principalColumn: "IdPensione",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Servizi",
                columns: table => new
                {
                    IdServizio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPrenotazione = table.Column<int>(type: "int", nullable: false),
                    TipoServizio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CostoServizio = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servizi", x => x.IdServizio);
                    table.ForeignKey(
                        name: "FK_Servizi_Prenotazioni_IdPrenotazione",
                        column: x => x.IdPrenotazione,
                        principalTable: "Prenotazioni",
                        principalColumn: "IdPrenotazione",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Camere_IdTipoCamera",
                table: "Camere",
                column: "IdTipoCamera");

            migrationBuilder.CreateIndex(
                name: "IX_Prenotazioni_IdCamera",
                table: "Prenotazioni",
                column: "IdCamera");

            migrationBuilder.CreateIndex(
                name: "IX_Prenotazioni_IdCliente",
                table: "Prenotazioni",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Prenotazioni_IdPensione",
                table: "Prenotazioni",
                column: "IdPensione");

            migrationBuilder.CreateIndex(
                name: "IX_Servizi_IdPrenotazione",
                table: "Servizi",
                column: "IdPrenotazione");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Servizi");

            migrationBuilder.DropTable(
                name: "Prenotazioni");

            migrationBuilder.DropTable(
                name: "Camere");

            migrationBuilder.DropTable(
                name: "Clienti");

            migrationBuilder.DropTable(
                name: "Pensioni");

            migrationBuilder.DropTable(
                name: "TipiCamere");
        }
    }
}
