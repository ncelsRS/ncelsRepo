using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Teme.Shared.Data.Migrations
{
    public partial class calculator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ref_PriceLists");

            migrationBuilder.DropTable(
                name: "Ref_ValueAddedTaxes");

            migrationBuilder.DropTable(
                name: "Ref_PriceTypes");

            migrationBuilder.DropTable(
                name: "Ref_ServiceTypes");

            migrationBuilder.DropTable(
                name: "Ref_ApplicationTypes");
        }
    }
}
