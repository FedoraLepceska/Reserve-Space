using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Reserve_Space.Data.Migrations
{
    public partial class thirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "address",
                table: "Spaces",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateFrom",
                table: "Spaces",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTo",
                table: "Spaces",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "image",
                table: "Spaces",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateFrom",
                table: "Spaces");

            migrationBuilder.DropColumn(
                name: "DateTo",
                table: "Spaces");

            migrationBuilder.DropColumn(
                name: "image",
                table: "Spaces");

            migrationBuilder.AlterColumn<string>(
                name: "address",
                table: "Spaces",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
