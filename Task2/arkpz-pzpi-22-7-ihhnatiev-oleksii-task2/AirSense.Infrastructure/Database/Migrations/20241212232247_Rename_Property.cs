using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirSense.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Rename_Property : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AirQualityHistorys_AirQualitys_QualityId",
                table: "AirQualityHistorys");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Locations",
                newName: "LocationId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AirQualitys",
                newName: "AirQualityId");

            migrationBuilder.RenameColumn(
                name: "QualityId",
                table: "AirQualityHistorys",
                newName: "AirQualityId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AirQualityHistorys",
                newName: "AirQualityHistoryId");

            migrationBuilder.RenameIndex(
                name: "IX_AirQualityHistorys_QualityId",
                table: "AirQualityHistorys",
                newName: "IX_AirQualityHistorys_AirQualityId");

            migrationBuilder.AddForeignKey(
                name: "FK_AirQualityHistorys_AirQualitys_AirQualityId",
                table: "AirQualityHistorys",
                column: "AirQualityId",
                principalTable: "AirQualitys",
                principalColumn: "AirQualityId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AirQualityHistorys_AirQualitys_AirQualityId",
                table: "AirQualityHistorys");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "Locations",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AirQualityId",
                table: "AirQualitys",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AirQualityId",
                table: "AirQualityHistorys",
                newName: "QualityId");

            migrationBuilder.RenameColumn(
                name: "AirQualityHistoryId",
                table: "AirQualityHistorys",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_AirQualityHistorys_AirQualityId",
                table: "AirQualityHistorys",
                newName: "IX_AirQualityHistorys_QualityId");

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacebookUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstagramUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkedInUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TwitterUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebsiteUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AirQualityHistorys_AirQualitys_QualityId",
                table: "AirQualityHistorys",
                column: "QualityId",
                principalTable: "AirQualitys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
