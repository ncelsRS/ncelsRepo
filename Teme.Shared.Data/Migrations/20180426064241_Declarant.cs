using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Teme.Shared.Data.Migrations
{
    public partial class Declarant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeclarantId",
                table: "DeclarantDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DeclarantDetails_DeclarantId",
                table: "DeclarantDetails",
                column: "DeclarantId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeclarantDetails_Declarants_DeclarantId",
                table: "DeclarantDetails",
                column: "DeclarantId",
                principalTable: "Declarants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeclarantDetails_Declarants_DeclarantId",
                table: "DeclarantDetails");

            migrationBuilder.DropIndex(
                name: "IX_DeclarantDetails_DeclarantId",
                table: "DeclarantDetails");

            migrationBuilder.DropColumn(
                name: "DeclarantId",
                table: "DeclarantDetails");
        }
    }
}
