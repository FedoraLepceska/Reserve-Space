using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Reserve_Space.Data.Migrations
{
    public partial class maps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateFrom",
                table: "SpacesInOrders",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTo",
                table: "SpacesInOrders",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<float>(
                name: "latitude",
                table: "Spaces",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "longitude",
                table: "Spaces",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateFrom",
                table: "SpacesInOrders");

            migrationBuilder.DropColumn(
                name: "DateTo",
                table: "SpacesInOrders");

            migrationBuilder.DropColumn(
                name: "latitude",
                table: "Spaces");

            migrationBuilder.DropColumn(
                name: "longitude",
                table: "Spaces");
        }
    }
}
