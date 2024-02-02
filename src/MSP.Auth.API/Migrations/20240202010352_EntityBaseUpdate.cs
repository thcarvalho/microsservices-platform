using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSP.Auth.API.Migrations
{
    /// <inheritdoc />
    public partial class EntityBaseUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "AuthUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 1, 22, 3, 52, 187, DateTimeKind.Local).AddTicks(3066),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AuthUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 1, 22, 3, 52, 187, DateTimeKind.Local).AddTicks(2808),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                table: "AuthUsers",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "AuthUsers",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 1, 22, 3, 52, 187, DateTimeKind.Local).AddTicks(3066));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AuthUsers",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 1, 22, 3, 52, 187, DateTimeKind.Local).AddTicks(2808));

            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                table: "AuthUsers",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);
        }
    }
}
