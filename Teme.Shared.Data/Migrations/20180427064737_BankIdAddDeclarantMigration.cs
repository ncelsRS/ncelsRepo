using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Teme.Shared.Data.Migrations
{
    public partial class BankIdAddDeclarantMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsJuridical",
                table: "Declarants",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "BankId",
                table: "DeclarantDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "AuthUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_DeclarantDetails_BankId",
                table: "DeclarantDetails",
                column: "BankId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeclarantDetails_Ref_Banks_BankId",
                table: "DeclarantDetails",
                column: "BankId",
                principalTable: "Ref_Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeclarantDetails_Ref_Banks_BankId",
                table: "DeclarantDetails");

            migrationBuilder.DropIndex(
                name: "IX_DeclarantDetails_BankId",
                table: "DeclarantDetails");

            migrationBuilder.DropColumn(
                name: "IsJuridical",
                table: "Declarants");

            migrationBuilder.DropColumn(
                name: "BankId",
                table: "DeclarantDetails");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "AuthUsers");
        }
    }
}
