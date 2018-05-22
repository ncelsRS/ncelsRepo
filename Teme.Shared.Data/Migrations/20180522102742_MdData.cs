using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Teme.Shared.Data.Migrations
{
    public partial class MdData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentEquipments_Payments_PaymentId",
                table: "PaymentEquipments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Ref_DegreeRiskClasses_DegreeRiskClassId",
                table: "Payments");

            migrationBuilder.DropTable(
                name: "PaymentPlatforms");

            migrationBuilder.DropIndex(
                name: "IX_Payments_DegreeRiskClassId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ApplicationAreaKz",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ApplicationAreaRu",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "AppointmentKz",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "AppointmentRu",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "CardBeginDate",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "CardEndDate",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "CardNumber",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ContractForm",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "DegreeRiskClassId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "IsBlank",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "IsClosedSystem",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "IsDiagnostics",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "IsMeasures",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "IsPresenceMedicinalProduct",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "IsStyryl",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "RationaleManufacturer",
                table: "Payments");

            migrationBuilder.AddColumn<int>(
                name: "PaymentForm",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeclarationId",
                table: "PaymentPackagings",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PaymentId",
                table: "PaymentEquipments",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "DeclarationId",
                table: "PaymentEquipments",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Declaration",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContractId = table.Column<int>(nullable: true),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false),
                    DeclarationForm = table.Column<int>(nullable: false),
                    LetterDate = table.Column<DateTime>(nullable: true),
                    LetterNumber = table.Column<string>(nullable: true),
                    NomenclatureId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Declaration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Declaration_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Declaration_Ref_NomenclatureCodeMedProducts_NomenclatureId",
                        column: x => x.NomenclatureId,
                        principalTable: "Ref_NomenclatureCodeMedProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MedicalDeviceDatas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationAreaKz = table.Column<string>(nullable: true),
                    ApplicationAreaRu = table.Column<string>(nullable: true),
                    AppointmentKz = table.Column<string>(nullable: true),
                    AppointmentRu = table.Column<string>(nullable: true),
                    CardBeginDate = table.Column<DateTime>(nullable: true),
                    CardEndDate = table.Column<DateTime>(nullable: true),
                    CardNumber = table.Column<string>(nullable: true),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false),
                    DegreeRiskClassId = table.Column<int>(nullable: true),
                    IsBlank = table.Column<bool>(nullable: true),
                    IsClosedSystem = table.Column<bool>(nullable: true),
                    IsDiagnostics = table.Column<bool>(nullable: true),
                    IsMeasures = table.Column<bool>(nullable: true),
                    IsPresenceMedicinalProduct = table.Column<bool>(nullable: true),
                    IsStyryl = table.Column<bool>(nullable: true),
                    RationaleManufacturer = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalDeviceDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalDeviceDatas_Ref_DegreeRiskClasses_DegreeRiskClassId",
                        column: x => x.DegreeRiskClassId,
                        principalTable: "Ref_DegreeRiskClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MedicalDeviceManufacturers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false),
                    ManufactureDetailId = table.Column<int>(nullable: true),
                    ManufactureId = table.Column<int>(nullable: true),
                    PaymentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalDeviceManufacturers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalDeviceManufacturers_DeclarantDetails_ManufactureDetailId",
                        column: x => x.ManufactureDetailId,
                        principalTable: "DeclarantDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalDeviceManufacturers_Declarants_ManufactureId",
                        column: x => x.ManufactureId,
                        principalTable: "Declarants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalDeviceManufacturers_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPackagings_DeclarationId",
                table: "PaymentPackagings",
                column: "DeclarationId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentEquipments_DeclarationId",
                table: "PaymentEquipments",
                column: "DeclarationId");

            migrationBuilder.CreateIndex(
                name: "IX_Declaration_ContractId",
                table: "Declaration",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Declaration_NomenclatureId",
                table: "Declaration",
                column: "NomenclatureId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalDeviceDatas_DegreeRiskClassId",
                table: "MedicalDeviceDatas",
                column: "DegreeRiskClassId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalDeviceManufacturers_ManufactureDetailId",
                table: "MedicalDeviceManufacturers",
                column: "ManufactureDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalDeviceManufacturers_ManufactureId",
                table: "MedicalDeviceManufacturers",
                column: "ManufactureId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalDeviceManufacturers_PaymentId",
                table: "MedicalDeviceManufacturers",
                column: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentEquipments_Declaration_DeclarationId",
                table: "PaymentEquipments",
                column: "DeclarationId",
                principalTable: "Declaration",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentEquipments_Payments_PaymentId",
                table: "PaymentEquipments",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentPackagings_Declaration_DeclarationId",
                table: "PaymentPackagings",
                column: "DeclarationId",
                principalTable: "Declaration",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentEquipments_Declaration_DeclarationId",
                table: "PaymentEquipments");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentEquipments_Payments_PaymentId",
                table: "PaymentEquipments");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentPackagings_Declaration_DeclarationId",
                table: "PaymentPackagings");

            migrationBuilder.DropTable(
                name: "Declaration");

            migrationBuilder.DropTable(
                name: "MedicalDeviceDatas");

            migrationBuilder.DropTable(
                name: "MedicalDeviceManufacturers");

            migrationBuilder.DropIndex(
                name: "IX_PaymentPackagings_DeclarationId",
                table: "PaymentPackagings");

            migrationBuilder.DropIndex(
                name: "IX_PaymentEquipments_DeclarationId",
                table: "PaymentEquipments");

            migrationBuilder.DropColumn(
                name: "PaymentForm",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "DeclarationId",
                table: "PaymentPackagings");

            migrationBuilder.DropColumn(
                name: "DeclarationId",
                table: "PaymentEquipments");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationAreaKz",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationAreaRu",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppointmentKz",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppointmentRu",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CardBeginDate",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CardEndDate",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CardNumber",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ContractForm",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DegreeRiskClassId",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsBlank",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsClosedSystem",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDiagnostics",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsMeasures",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPresenceMedicinalProduct",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsStyryl",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RationaleManufacturer",
                table: "Payments",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PaymentId",
                table: "PaymentEquipments",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "PaymentPlatforms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CountryId = table.Column<int>(nullable: true),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false),
                    FactAddress = table.Column<string>(maxLength: 255, nullable: true),
                    LegalAddress = table.Column<string>(maxLength: 255, nullable: true),
                    NameEn = table.Column<string>(nullable: true),
                    NameKz = table.Column<string>(nullable: true),
                    NameRu = table.Column<string>(nullable: true),
                    PaymentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentPlatforms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentPlatforms_Ref_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Ref_Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentPlatforms_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_DegreeRiskClassId",
                table: "Payments",
                column: "DegreeRiskClassId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPlatforms_CountryId",
                table: "PaymentPlatforms",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPlatforms_PaymentId",
                table: "PaymentPlatforms",
                column: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentEquipments_Payments_PaymentId",
                table: "PaymentEquipments",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Ref_DegreeRiskClasses_DegreeRiskClassId",
                table: "Payments",
                column: "DegreeRiskClassId",
                principalTable: "Ref_DegreeRiskClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
