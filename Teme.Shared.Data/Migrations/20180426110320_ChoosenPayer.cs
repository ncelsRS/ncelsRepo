using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Teme.Shared.Data.Migrations
{
    public partial class ChoosenPayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "DeclarantPerpetualDoc",
                table: "DeclarantDetails",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChoosePayer",
                table: "Contracts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChoosePayer",
                table: "Contracts");

            migrationBuilder.AlterColumn<string>(
                name: "DeclarantPerpetualDoc",
                table: "DeclarantDetails",
                nullable: true,
                oldClrType: typeof(bool));
        }
    }
}
