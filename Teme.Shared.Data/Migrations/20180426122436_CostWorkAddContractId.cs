using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Teme.Shared.Data.Migrations
{
    public partial class CostWorkAddContractId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContractId",
                table: "CostWorks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CostWorks_ContractId",
                table: "CostWorks",
                column: "ContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_CostWorks_Contracts_ContractId",
                table: "CostWorks",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CostWorks_Contracts_ContractId",
                table: "CostWorks");

            migrationBuilder.DropIndex(
                name: "IX_CostWorks_ContractId",
                table: "CostWorks");

            migrationBuilder.DropColumn(
                name: "ContractId",
                table: "CostWorks");
        }
    }
}
