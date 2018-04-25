using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Teme.Shared.Data.Migrations
{
    public partial class contract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CostWorks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Count = table.Column<int>(nullable: true),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false),
                    IsImport = table.Column<bool>(nullable: false),
                    PriceListId = table.Column<int>(nullable: true),
                    PriceWithValueAddedTax = table.Column<decimal>(nullable: true),
                    TotalPriceWithValueAddedTax = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostWorks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CostWorks_Ref_PriceLists_PriceListId",
                        column: x => x.PriceListId,
                        principalTable: "Ref_PriceLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeclarantDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BankBin = table.Column<string>(nullable: true),
                    BankIik = table.Column<string>(maxLength: 255, nullable: true),
                    BankName = table.Column<string>(maxLength: 255, nullable: true),
                    BankSwift = table.Column<string>(maxLength: 255, nullable: true),
                    BossFirstName = table.Column<string>(maxLength: 255, nullable: true),
                    BossLastName = table.Column<string>(maxLength: 255, nullable: true),
                    BossMiddleName = table.Column<string>(maxLength: 255, nullable: true),
                    BossPositionKz = table.Column<string>(maxLength: 255, nullable: true),
                    BossPositionRu = table.Column<string>(maxLength: 255, nullable: true),
                    CurrencyId = table.Column<int>(nullable: true),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false),
                    DeclarantDocEndDate = table.Column<DateTime>(nullable: true),
                    DeclarantDocNumber = table.Column<string>(maxLength: 255, nullable: true),
                    DeclarantDocStartDate = table.Column<DateTime>(nullable: true),
                    DeclarantDocType = table.Column<int>(nullable: false),
                    DeclarantDocWithoutNumber = table.Column<bool>(nullable: false),
                    DeclarantPerpetualDoc = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 255, nullable: true),
                    FactAddress = table.Column<string>(maxLength: 255, nullable: true),
                    LegalAddress = table.Column<string>(maxLength: 255, nullable: true),
                    Phone = table.Column<string>(maxLength: 20, nullable: true),
                    Phone2 = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeclarantDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeclarantDetails_Ref_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Ref_Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Declarants",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CountryId = table.Column<int>(nullable: true),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false),
                    IdNumber = table.Column<string>(maxLength: 12, nullable: true),
                    IsConfirmed = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsResident = table.Column<bool>(nullable: false),
                    NameEn = table.Column<string>(nullable: true),
                    NameKz = table.Column<string>(nullable: true),
                    NameRu = table.Column<string>(nullable: true),
                    OrganizationFormId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Declarants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Declarants_Ref_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Ref_Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Declarants_Ref_OrganizationForms_OrganizationFormId",
                        column: x => x.OrganizationFormId,
                        principalTable: "Ref_OrganizationForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false),
                    RoleName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatePolicies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContractId = table.Column<int>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false),
                    PermissionScopeId = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatePolicies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserForActions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserForActions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContractForm = table.Column<int>(nullable: false),
                    ContractType = table.Column<int>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false),
                    DeclarantDetailId = table.Column<int>(nullable: true),
                    DeclarantId = table.Column<int>(nullable: true),
                    DeclarantIsManufacture = table.Column<bool>(nullable: false),
                    HolderType = table.Column<int>(nullable: false),
                    ManufacturDetailId = table.Column<int>(nullable: true),
                    ManufacturId = table.Column<int>(nullable: true),
                    MedicalDeviceNameKz = table.Column<string>(maxLength: 255, nullable: true),
                    MedicalDeviceNameRu = table.Column<string>(maxLength: 255, nullable: true),
                    Number = table.Column<string>(maxLength: 255, nullable: true),
                    PayerDetailId = table.Column<int>(nullable: true),
                    PayerId = table.Column<int>(nullable: true),
                    WorkflowId = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_DeclarantDetails_DeclarantDetailId",
                        column: x => x.DeclarantDetailId,
                        principalTable: "DeclarantDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contracts_Declarants_DeclarantId",
                        column: x => x.DeclarantId,
                        principalTable: "Declarants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contracts_DeclarantDetails_ManufacturDetailId",
                        column: x => x.ManufacturDetailId,
                        principalTable: "DeclarantDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contracts_Declarants_ManufacturId",
                        column: x => x.ManufacturId,
                        principalTable: "Declarants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contracts_DeclarantDetails_PayerDetailId",
                        column: x => x.PayerDetailId,
                        principalTable: "DeclarantDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contracts_Declarants_PayerId",
                        column: x => x.PayerId,
                        principalTable: "Declarants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_DeclarantDetailId",
                table: "Contracts",
                column: "DeclarantDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_DeclarantId",
                table: "Contracts",
                column: "DeclarantId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ManufacturDetailId",
                table: "Contracts",
                column: "ManufacturDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ManufacturId",
                table: "Contracts",
                column: "ManufacturId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_PayerDetailId",
                table: "Contracts",
                column: "PayerDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_PayerId",
                table: "Contracts",
                column: "PayerId");

            migrationBuilder.CreateIndex(
                name: "IX_CostWorks_PriceListId",
                table: "CostWorks",
                column: "PriceListId");

            migrationBuilder.CreateIndex(
                name: "IX_DeclarantDetails_CurrencyId",
                table: "DeclarantDetails",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Declarants_CountryId",
                table: "Declarants",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Declarants_OrganizationFormId",
                table: "Declarants",
                column: "OrganizationFormId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "CostWorks");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "StatePolicies");

            migrationBuilder.DropTable(
                name: "UserForActions");

            migrationBuilder.DropTable(
                name: "DeclarantDetails");

            migrationBuilder.DropTable(
                name: "Declarants");
        }
    }
}
