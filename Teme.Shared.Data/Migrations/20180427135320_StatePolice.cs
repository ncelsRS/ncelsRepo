using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Teme.Shared.Data.Migrations
{
    public partial class StatePolice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PermissionScopeId",
                table: "StatePolicies");

            migrationBuilder.AddColumn<string>(
                name: "Permission",
                table: "StatePolicies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Scope",
                table: "StatePolicies",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ContractId",
                table: "UserForActions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExecutorId",
                table: "UserForActions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StatePolicies_ContractId",
                table: "StatePolicies",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_UserForActions_ContractId",
                table: "UserForActions",
                column: "ContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_StatePolicies_Contracts_ContractId",
                table: "StatePolicies",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserForActions_Contracts_ContractId",
                table: "UserForActions",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StatePolicies_Contracts_ContractId",
                table: "StatePolicies");

            migrationBuilder.DropForeignKey(
                name: "FK_UserForActions_Contracts_ContractId",
                table: "UserForActions");

            migrationBuilder.DropIndex(
                name: "IX_StatePolicies_ContractId",
                table: "StatePolicies");

            migrationBuilder.DropIndex(
                name: "IX_UserForActions_ContractId",
                table: "UserForActions");

            migrationBuilder.DropColumn(
                name: "Permission",
                table: "StatePolicies");

            migrationBuilder.DropColumn(
                name: "Scope",
                table: "StatePolicies");

            migrationBuilder.DropColumn(
                name: "ContractId",
                table: "UserForActions");

            migrationBuilder.DropColumn(
                name: "ExecutorId",
                table: "UserForActions");

            migrationBuilder.AddColumn<int>(
                name: "PermissionScopeId",
                table: "StatePolicies",
                nullable: false,
                defaultValue: 0);
        }
    }
}
