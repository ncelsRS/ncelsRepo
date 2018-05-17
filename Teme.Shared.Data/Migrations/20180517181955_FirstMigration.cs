using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.IO;

namespace Teme.Shared.Data.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Icons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false),
                    FieldName = table.Column<string>(maxLength: 500, nullable: true),
                    ModuleType = table.Column<int>(nullable: false),
                    ObjectId = table.Column<int>(nullable: false),
                    isDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Icons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ref_ApplicationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    ContractForm = table.Column<string>(maxLength: 20, nullable: true),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    NameKz = table.Column<string>(nullable: false),
                    NameRu = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ref_ApplicationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ref_Banks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false),
                    IsConfirmed = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    NameKz = table.Column<string>(nullable: false),
                    NameRu = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ref_Banks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ref_ClassifierMedicalAreas",
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
                    table.PrimaryKey("PK_Ref_ClassifierMedicalAreas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ref_Countries",
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
                    table.PrimaryKey("PK_Ref_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ref_Currencies",
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
                    table.PrimaryKey("PK_Ref_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ref_DegreeRiskClasses",
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
                    table.PrimaryKey("PK_Ref_DegreeRiskClasses", x => x.Id);
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
                name: "Ref_NomenclatureCodeMedProducts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false),
                    DefenitionNameKz = table.Column<string>(nullable: true),
                    DefenitionNameRu = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    NameKz = table.Column<string>(nullable: false),
                    NameRu = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ref_NomenclatureCodeMedProducts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ref_OrganizationForms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false),
                    IsConfirmed = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    NameKz = table.Column<string>(nullable: false),
                    NameRu = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ref_OrganizationForms", x => x.Id);
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
                name: "Ref_PriceTypes",
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
                    table.PrimaryKey("PK_Ref_PriceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ref_StorageConditions",
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
                    table.PrimaryKey("PK_Ref_StorageConditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ref_ValueAddedTaxes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Value = table.Column<decimal>(nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ref_ValueAddedTaxes", x => x.Id);
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
                name: "Ref_ServiceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationTypeId = table.Column<int>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    NameKz = table.Column<string>(nullable: false),
                    NameRu = table.Column<string>(nullable: false),
                    ParentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ref_ServiceTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ref_ServiceTypes_Ref_ApplicationTypes_ApplicationTypeId",
                        column: x => x.ApplicationTypeId,
                        principalTable: "Ref_ApplicationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ref_ServiceTypes_Ref_ServiceTypes_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Ref_ServiceTypes",
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
                    IsJuridical = table.Column<bool>(nullable: false),
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
                name: "Ref_PriceLists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsImport = table.Column<bool>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    PriceTypeId = table.Column<int>(nullable: false),
                    ServiceTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ref_PriceLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ref_PriceLists_Ref_PriceTypes_PriceTypeId",
                        column: x => x.PriceTypeId,
                        principalTable: "Ref_PriceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ref_PriceLists_Ref_ServiceTypes_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "Ref_ServiceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeclarantDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BankBin = table.Column<string>(nullable: true),
                    BankId = table.Column<int>(nullable: true),
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
                    DeclarantId = table.Column<int>(nullable: false),
                    DeclarantPerpetualDoc = table.Column<bool>(nullable: false),
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
                        name: "FK_DeclarantDetails_Ref_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Ref_Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeclarantDetails_Ref_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Ref_Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeclarantDetails_Declarants_DeclarantId",
                        column: x => x.DeclarantId,
                        principalTable: "Declarants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ChoosePayer = table.Column<int>(nullable: false),
                    ContractForm = table.Column<int>(nullable: false),
                    ContractScope = table.Column<string>(nullable: true),
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
                    WorkflowId = table.Column<string>(maxLength: 50, nullable: true),
                    isDeleted = table.Column<bool>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "CostWorks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContractId = table.Column<int>(nullable: false),
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
                        name: "FK_CostWorks_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CostWorks_Ref_PriceLists_PriceListId",
                        column: x => x.PriceListId,
                        principalTable: "Ref_PriceLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                    ContractForm = table.Column<int>(nullable: true),
                    ContractId = table.Column<int>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false),
                    DegreeRiskClassId = table.Column<int>(nullable: true),
                    IsBlank = table.Column<bool>(nullable: true),
                    IsClosedSystem = table.Column<bool>(nullable: true),
                    IsDiagnostics = table.Column<bool>(nullable: true),
                    IsMeasures = table.Column<bool>(nullable: true),
                    IsPresenceMedicinalProduct = table.Column<bool>(nullable: true),
                    IsStyryl = table.Column<bool>(nullable: true),
                    IsTypeImnMt = table.Column<bool>(nullable: true),
                    NameKz = table.Column<string>(nullable: true),
                    NameRu = table.Column<string>(nullable: true),
                    NumberModificationImn = table.Column<string>(nullable: true),
                    RationaleManufacturer = table.Column<string>(nullable: true),
                    RevisionBeforeChanges = table.Column<string>(nullable: true),
                    TradeName = table.Column<string>(nullable: true),
                    isDeleted = table.Column<bool>(nullable: false)
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
                    table.ForeignKey(
                        name: "FK_Payments_Ref_DegreeRiskClasses_DegreeRiskClassId",
                        column: x => x.DegreeRiskClassId,
                        principalTable: "Ref_DegreeRiskClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    Permission = table.Column<string>(nullable: true),
                    Scope = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatePolicies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatePolicies_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserForActions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContractId = table.Column<int>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false),
                    ExecutorId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserForActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserForActions_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
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
                name: "AuthUserScopes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuthUserId = table.Column<int>(nullable: true),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false),
                    Scope = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthUserScopes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IconRecords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuthRoleId = table.Column<int>(nullable: false),
                    AuthUserId = table.Column<int>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false),
                    DisplayField = table.Column<string>(maxLength: 500, nullable: true),
                    IconId = table.Column<int>(nullable: false),
                    Note = table.Column<string>(maxLength: 2000, nullable: true),
                    ValueField = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IconRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IconRecords_Icons_IconId",
                        column: x => x.IconId,
                        principalTable: "Icons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Bin = table.Column<string>(nullable: true),
                    CompanyName = table.Column<string>(nullable: true),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    HasIin = table.Column<bool>(nullable: true),
                    IconRecordId = table.Column<int>(nullable: true),
                    Iin = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: false),
                    MiddleName = table.Column<string>(nullable: true),
                    Pwdhash = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: false),
                    UserType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuthUsers_IconRecords_IconRecordId",
                        column: x => x.IconRecordId,
                        principalTable: "IconRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthUsers_IconRecordId",
                table: "AuthUsers",
                column: "IconRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthUserScopes_AuthUserId",
                table: "AuthUserScopes",
                column: "AuthUserId");

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
                name: "IX_CostWorks_ContractId",
                table: "CostWorks",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_CostWorks_PriceListId",
                table: "CostWorks",
                column: "PriceListId");

            migrationBuilder.CreateIndex(
                name: "IX_DeclarantDetails_BankId",
                table: "DeclarantDetails",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_DeclarantDetails_CurrencyId",
                table: "DeclarantDetails",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_DeclarantDetails_DeclarantId",
                table: "DeclarantDetails",
                column: "DeclarantId");

            migrationBuilder.CreateIndex(
                name: "IX_Declarants_CountryId",
                table: "Declarants",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Declarants_OrganizationFormId",
                table: "Declarants",
                column: "OrganizationFormId");

            migrationBuilder.CreateIndex(
                name: "IX_IconRecords_AuthUserId",
                table: "IconRecords",
                column: "AuthUserId");

            migrationBuilder.CreateIndex(
                name: "IX_IconRecords_IconId",
                table: "IconRecords",
                column: "IconId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Payments_DegreeRiskClassId",
                table: "Payments",
                column: "DegreeRiskClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Ref_PriceLists_PriceTypeId",
                table: "Ref_PriceLists",
                column: "PriceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ref_PriceLists_ServiceTypeId",
                table: "Ref_PriceLists",
                column: "ServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ref_ServiceTypes_ApplicationTypeId",
                table: "Ref_ServiceTypes",
                column: "ApplicationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ref_ServiceTypes_ParentId",
                table: "Ref_ServiceTypes",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_StatePolicies_ContractId",
                table: "StatePolicies",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_UserForActions_ContractId",
                table: "UserForActions",
                column: "ContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthUserScopes_AuthUsers_AuthUserId",
                table: "AuthUserScopes",
                column: "AuthUserId",
                principalTable: "AuthUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IconRecords_AuthUsers_AuthUserId",
                table: "IconRecords",
                column: "AuthUserId",
                principalTable: "AuthUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            string[] files = Directory.GetFiles(Directory.GetParent(Directory.GetCurrentDirectory()).FullName + @"\SQLScripts\Teme", "*.sql");
            foreach (var file in files)
            {
                //string sql = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, MIGRATION_SQL_SCRIPT_FILE_NAME);
                migrationBuilder.Sql(File.ReadAllText(file));
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthUsers_IconRecords_IconRecordId",
                table: "AuthUsers");

            migrationBuilder.DropTable(
                name: "AuthUserScopes");

            migrationBuilder.DropTable(
                name: "CostWorks");

            migrationBuilder.DropTable(
                name: "PaymentEquipments");

            migrationBuilder.DropTable(
                name: "PaymentPackagings");

            migrationBuilder.DropTable(
                name: "PaymentPlatforms");

            migrationBuilder.DropTable(
                name: "Ref_ClassifierMedicalAreas");

            migrationBuilder.DropTable(
                name: "Ref_NomenclatureCodeMedProducts");

            migrationBuilder.DropTable(
                name: "Ref_StorageConditions");

            migrationBuilder.DropTable(
                name: "Ref_ValueAddedTaxes");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "StatePolicies");

            migrationBuilder.DropTable(
                name: "UserForActions");

            migrationBuilder.DropTable(
                name: "Ref_PriceLists");

            migrationBuilder.DropTable(
                name: "Ref_EquipmentTypes");

            migrationBuilder.DropTable(
                name: "Ref_PackagingTypes");

            migrationBuilder.DropTable(
                name: "Ref_Measures");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Ref_PriceTypes");

            migrationBuilder.DropTable(
                name: "Ref_ServiceTypes");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Ref_DegreeRiskClasses");

            migrationBuilder.DropTable(
                name: "Ref_ApplicationTypes");

            migrationBuilder.DropTable(
                name: "DeclarantDetails");

            migrationBuilder.DropTable(
                name: "Ref_Banks");

            migrationBuilder.DropTable(
                name: "Ref_Currencies");

            migrationBuilder.DropTable(
                name: "Declarants");

            migrationBuilder.DropTable(
                name: "Ref_Countries");

            migrationBuilder.DropTable(
                name: "Ref_OrganizationForms");

            migrationBuilder.DropTable(
                name: "IconRecords");

            migrationBuilder.DropTable(
                name: "AuthUsers");

            migrationBuilder.DropTable(
                name: "Icons");
        }
    }
}
