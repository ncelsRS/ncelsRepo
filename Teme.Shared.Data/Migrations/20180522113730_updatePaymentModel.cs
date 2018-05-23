using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Teme.Shared.Data.Migrations
{
    public partial class updatePaymentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MedicalDeviceDataId",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PayerDetailId",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PayerId",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NdNumber",
                table: "MedicalDeviceDatas",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MedicalDeviceDataId",
                table: "Declaration",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MedicalDeviceManufacturerId",
                table: "Declaration",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ManufactureType",
                table: "Declarants",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_MedicalDeviceDataId",
                table: "Payments",
                column: "MedicalDeviceDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PayerDetailId",
                table: "Payments",
                column: "PayerDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PayerId",
                table: "Payments",
                column: "PayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Declaration_MedicalDeviceDataId",
                table: "Declaration",
                column: "MedicalDeviceDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Declaration_MedicalDeviceManufacturerId",
                table: "Declaration",
                column: "MedicalDeviceManufacturerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Declaration_MedicalDeviceDatas_MedicalDeviceDataId",
                table: "Declaration",
                column: "MedicalDeviceDataId",
                principalTable: "MedicalDeviceDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Declaration_MedicalDeviceManufacturers_MedicalDeviceManufacturerId",
                table: "Declaration",
                column: "MedicalDeviceManufacturerId",
                principalTable: "MedicalDeviceManufacturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_MedicalDeviceDatas_MedicalDeviceDataId",
                table: "Payments",
                column: "MedicalDeviceDataId",
                principalTable: "MedicalDeviceDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_DeclarantDetails_PayerDetailId",
                table: "Payments",
                column: "PayerDetailId",
                principalTable: "DeclarantDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Declarants_PayerId",
                table: "Payments",
                column: "PayerId",
                principalTable: "Declarants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Declaration_MedicalDeviceDatas_MedicalDeviceDataId",
                table: "Declaration");

            migrationBuilder.DropForeignKey(
                name: "FK_Declaration_MedicalDeviceManufacturers_MedicalDeviceManufacturerId",
                table: "Declaration");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_MedicalDeviceDatas_MedicalDeviceDataId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_DeclarantDetails_PayerDetailId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Declarants_PayerId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_MedicalDeviceDataId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_PayerDetailId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_PayerId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Declaration_MedicalDeviceDataId",
                table: "Declaration");

            migrationBuilder.DropIndex(
                name: "IX_Declaration_MedicalDeviceManufacturerId",
                table: "Declaration");

            migrationBuilder.DropColumn(
                name: "MedicalDeviceDataId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PayerDetailId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PayerId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "NdNumber",
                table: "MedicalDeviceDatas");

            migrationBuilder.DropColumn(
                name: "MedicalDeviceDataId",
                table: "Declaration");

            migrationBuilder.DropColumn(
                name: "MedicalDeviceManufacturerId",
                table: "Declaration");

            migrationBuilder.DropColumn(
                name: "ManufactureType",
                table: "Declarants");
        }
    }
}
