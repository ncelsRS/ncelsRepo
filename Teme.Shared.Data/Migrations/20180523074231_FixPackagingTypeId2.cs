using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Teme.Shared.Data.Migrations
{
    public partial class FixPackagingTypeId2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentPackagings_Ref_PackagingTypes_PackagingTypeId",
                table: "PaymentPackagings");

            migrationBuilder.AlterColumn<int>(
                name: "PackagingTypeId",
                table: "PaymentPackagings",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentPackagings_Ref_PackagingTypes_PackagingTypeId",
                table: "PaymentPackagings",
                column: "PackagingTypeId",
                principalTable: "Ref_PackagingTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentPackagings_Ref_PackagingTypes_PackagingTypeId",
                table: "PaymentPackagings");

            migrationBuilder.AlterColumn<int>(
                name: "PackagingTypeId",
                table: "PaymentPackagings",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentPackagings_Ref_PackagingTypes_PackagingTypeId",
                table: "PaymentPackagings",
                column: "PackagingTypeId",
                principalTable: "Ref_PackagingTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
