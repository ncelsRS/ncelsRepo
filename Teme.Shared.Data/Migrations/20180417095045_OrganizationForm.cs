using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Teme.Shared.Data.Migrations
{
    public partial class OrganizationForm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ref_OrganizationForms");
        }
    }
}
