using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Teme.Shared.Data.Migrations
{
    public partial class Payment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Scopes",
                table: "AuthUsers");

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationAreaKz = table.Column<string>(nullable: true),
                    ApplicationAreaRu = table.Column<string>(nullable: true),
                    AppointmentKz = table.Column<string>(nullable: true),
                    AppointmentRu = table.Column<string>(nullable: true),
                    CardNumber = table.Column<string>(nullable: true),
                    ChangesMade = table.Column<string>(nullable: true),
                    ClassRisk = table.Column<string>(nullable: true),
                    ContractId = table.Column<int>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false),
                    IsBlank = table.Column<bool>(nullable: false),
                    IsClosedSystem = table.Column<bool>(nullable: false),
                    IsDiagnostics = table.Column<bool>(nullable: false),
                    IsMeasures = table.Column<bool>(nullable: false),
                    IsPresenceMedicinalProduct = table.Column<bool>(nullable: false),
                    IsStyryl = table.Column<bool>(nullable: false),
                    IsTypeImnMt = table.Column<bool>(nullable: false),
                    NameKz = table.Column<string>(nullable: true),
                    NameRu = table.Column<string>(nullable: true),
                    NumberModificationImn = table.Column<string>(nullable: true),
                    RationaleManufacturer = table.Column<string>(nullable: true),
                    RevisionBeforeChanges = table.Column<string>(nullable: true),
                    TradeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ref_EquipmentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    NameKz = table.Column<string>(nullable: false),
                    NameRu = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ref_EquipmentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ref_Measures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    NameKz = table.Column<string>(nullable: false),
                    NameRu = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ref_Measures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ref_PackagingTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    NameKz = table.Column<string>(nullable: false),
                    NameRu = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ref_PackagingTypes", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "PaymentEquipments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    CountryId = table.Column<int>(nullable: true),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false),
                    EquipmentTypeId = table.Column<int>(nullable: false),
                    Manufacturer = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PaymentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentEquipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentEquipments_Ref_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Ref_Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentEquipments_Ref_EquipmentTypes_EquipmentTypeId",
                        column: x => x.EquipmentTypeId,
                        principalTable: "Ref_EquipmentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentEquipments_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentPackagings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NumberUnitsInBox = table.Column<string>(nullable: true),
                    PackagingTypeId = table.Column<int>(nullable: true),
                    PackagingtTypeId = table.Column<int>(nullable: false),
                    PaymentId = table.Column<int>(nullable: false),
                    ShortDescription = table.Column<string>(nullable: true),
                    SizeHeight = table.Column<string>(nullable: true),
                    SizeLength = table.Column<string>(nullable: true),
                    SizeMeasureId = table.Column<int>(nullable: true),
                    SizeWidth = table.Column<string>(nullable: true),
                    VolumeMeasureId = table.Column<int>(nullable: true),
                    VolumeValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentPackagings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentPackagings_Ref_PackagingTypes_PackagingTypeId",
                        column: x => x.PackagingTypeId,
                        principalTable: "Ref_PackagingTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentPackagings_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentPackagings_Ref_Measures_SizeMeasureId",
                        column: x => x.SizeMeasureId,
                        principalTable: "Ref_Measures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentPackagings_Ref_Measures_VolumeMeasureId",
                        column: x => x.VolumeMeasureId,
                        principalTable: "Ref_Measures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentEquipments_CountryId",
                table: "PaymentEquipments",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentEquipments_EquipmentTypeId",
                table: "PaymentEquipments",
                column: "EquipmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentEquipments_PaymentId",
                table: "PaymentEquipments",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPackagings_PackagingTypeId",
                table: "PaymentPackagings",
                column: "PackagingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPackagings_PaymentId",
                table: "PaymentPackagings",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPackagings_SizeMeasureId",
                table: "PaymentPackagings",
                column: "SizeMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPackagings_VolumeMeasureId",
                table: "PaymentPackagings",
                column: "VolumeMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPlatforms_CountryId",
                table: "PaymentPlatforms",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPlatforms_PaymentId",
                table: "PaymentPlatforms",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ContractId",
                table: "Payments",
                column: "ContractId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentEquipments");

            migrationBuilder.DropTable(
                name: "PaymentPackagings");

            migrationBuilder.DropTable(
                name: "PaymentPlatforms");

            migrationBuilder.DropTable(
                name: "Ref_EquipmentTypes");

            migrationBuilder.DropTable(
                name: "Ref_PackagingTypes");

            migrationBuilder.DropTable(
                name: "Ref_Measures");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.AddColumn<string>(
                name: "Scopes",
                table: "AuthUsers",
                nullable: false,
                defaultValue: "");
        }
    }
}
