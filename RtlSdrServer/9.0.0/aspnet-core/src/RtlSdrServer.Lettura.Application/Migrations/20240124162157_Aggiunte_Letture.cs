using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RtlSdrServer.Lettura.Application.Migrations
{
    /// <inheritdoc />
    public partial class Aggiunte_Letture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Letture",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FrequenzaIniziale = table.Column<decimal>(type: "decimal(20,10)", precision: 20, scale: 10, nullable: false),
                    FrequenzaFinale = table.Column<decimal>(type: "decimal(20,10)", precision: 20, scale: 10, nullable: false),
                    PosizioneLatitudine = table.Column<decimal>(type: "decimal(20,10)", precision: 20, scale: 10, nullable: false),
                    PosizioneLongitudine = table.Column<decimal>(type: "decimal(20,10)", precision: 20, scale: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Letture", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PotenzaSegnali",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LetturaId = table.Column<long>(type: "bigint", nullable: false),
                    Frequenza = table.Column<decimal>(type: "decimal(20,10)", precision: 20, scale: 10, nullable: false),
                    Valore = table.Column<decimal>(type: "decimal(20,10)", precision: 20, scale: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PotenzaSegnali", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PotenzaSegnali_Letture_LetturaId",
                        column: x => x.LetturaId,
                        principalTable: "Letture",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PotenzaSegnali_LetturaId",
                table: "PotenzaSegnali",
                column: "LetturaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PotenzaSegnali");

            migrationBuilder.DropTable(
                name: "Letture");
        }
    }
}
