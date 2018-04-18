using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Teme.Shared.Data.Migrations
{
    public partial class reference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ref_ClassifierMedicalAreas");

            migrationBuilder.DropTable(
                name: "Ref_NomenclatureCodeMedProducts");
        }
    }
}
