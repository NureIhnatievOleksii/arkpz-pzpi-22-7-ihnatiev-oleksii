using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirSense.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Added_Can_Be_NULL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AirQualityHistorys_AirQualitys_AirQualityId",
                table: "AirQualityHistorys");

            migrationBuilder.DropForeignKey(
                name: "FK_AirQualitys_Locations_LocationId",
                table: "AirQualitys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AirQualitys",
                table: "AirQualitys");

            migrationBuilder.RenameTable(
                name: "AirQualitys",
                newName: "AirQualities");

            migrationBuilder.RenameIndex(
                name: "IX_AirQualitys_LocationId",
                table: "AirQualities",
                newName: "IX_AirQualities_LocationId");

            migrationBuilder.AlterColumn<Guid>(
                name: "LocationId",
                table: "AirQualities",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<bool>(
                name: "IsSynced",
                table: "AirQualities",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "DeviceId",
                table: "AirQualities",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AirQualities",
                table: "AirQualities",
                column: "AirQualityId");

            migrationBuilder.AddForeignKey(
                name: "FK_AirQualities_Locations_LocationId",
                table: "AirQualities",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AirQualityHistorys_AirQualities_AirQualityId",
                table: "AirQualityHistorys",
                column: "AirQualityId",
                principalTable: "AirQualities",
                principalColumn: "AirQualityId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AirQualities_Locations_LocationId",
                table: "AirQualities");

            migrationBuilder.DropForeignKey(
                name: "FK_AirQualityHistorys_AirQualities_AirQualityId",
                table: "AirQualityHistorys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AirQualities",
                table: "AirQualities");

            migrationBuilder.RenameTable(
                name: "AirQualities",
                newName: "AirQualitys");

            migrationBuilder.RenameIndex(
                name: "IX_AirQualities_LocationId",
                table: "AirQualitys",
                newName: "IX_AirQualitys_LocationId");

            migrationBuilder.AlterColumn<Guid>(
                name: "LocationId",
                table: "AirQualitys",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsSynced",
                table: "AirQualitys",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeviceId",
                table: "AirQualitys",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AirQualitys",
                table: "AirQualitys",
                column: "AirQualityId");

            migrationBuilder.AddForeignKey(
                name: "FK_AirQualityHistorys_AirQualitys_AirQualityId",
                table: "AirQualityHistorys",
                column: "AirQualityId",
                principalTable: "AirQualitys",
                principalColumn: "AirQualityId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AirQualitys_Locations_LocationId",
                table: "AirQualitys",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
