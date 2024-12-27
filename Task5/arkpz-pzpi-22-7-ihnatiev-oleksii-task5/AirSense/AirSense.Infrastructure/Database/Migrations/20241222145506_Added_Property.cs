using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirSense.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Added_Property : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "AirQualitys",
                newName: "MeasuredAt");

            migrationBuilder.RenameColumn(
                name: "Components",
                table: "AirQualitys",
                newName: "DeviceId");

            migrationBuilder.RenameColumn(
                name: "Aqi",
                table: "AirQualitys",
                newName: "So2");

            migrationBuilder.AddColumn<float>(
                name: "Co",
                table: "AirQualitys",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<bool>(
                name: "IsSynced",
                table: "AirQualitys",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<float>(
                name: "Nh3",
                table: "AirQualitys",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "No",
                table: "AirQualitys",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "No2",
                table: "AirQualitys",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "O3",
                table: "AirQualitys",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Pm10",
                table: "AirQualitys",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Pm2_5",
                table: "AirQualitys",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Co",
                table: "AirQualitys");

            migrationBuilder.DropColumn(
                name: "IsSynced",
                table: "AirQualitys");

            migrationBuilder.DropColumn(
                name: "Nh3",
                table: "AirQualitys");

            migrationBuilder.DropColumn(
                name: "No",
                table: "AirQualitys");

            migrationBuilder.DropColumn(
                name: "No2",
                table: "AirQualitys");

            migrationBuilder.DropColumn(
                name: "O3",
                table: "AirQualitys");

            migrationBuilder.DropColumn(
                name: "Pm10",
                table: "AirQualitys");

            migrationBuilder.DropColumn(
                name: "Pm2_5",
                table: "AirQualitys");

            migrationBuilder.RenameColumn(
                name: "So2",
                table: "AirQualitys",
                newName: "Aqi");

            migrationBuilder.RenameColumn(
                name: "MeasuredAt",
                table: "AirQualitys",
                newName: "Timestamp");

            migrationBuilder.RenameColumn(
                name: "DeviceId",
                table: "AirQualitys",
                newName: "Components");
        }
    }
}
