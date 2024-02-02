using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSP.Auth.API.Migrations
{
    /// <inheritdoc />
    public partial class EntityBaseUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "AuthUsers",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 1, 22, 3, 52, 187, DateTimeKind.Local).AddTicks(3066));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AuthUsers",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 1, 22, 3, 52, 187, DateTimeKind.Local).AddTicks(2808));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "AuthUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 1, 22, 3, 52, 187, DateTimeKind.Local).AddTicks(3066),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AuthUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 1, 22, 3, 52, 187, DateTimeKind.Local).AddTicks(2808),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");
        }
    }
}
