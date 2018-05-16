using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Teme.Shared.Data.Migrations
{
    public partial class Icon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Payments",
                nullable: false,
                defaultValue: false);

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
                        name: "FK_IconRecords_AuthUsers_AuthUserId",
                        column: x => x.AuthUserId,
                        principalTable: "AuthUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IconRecords_Icons_IconId",
                        column: x => x.IconId,
                        principalTable: "Icons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IconRecords_AuthUserId",
                table: "IconRecords",
                column: "AuthUserId");

            migrationBuilder.CreateIndex(
                name: "IX_IconRecords_IconId",
                table: "IconRecords",
                column: "IconId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IconRecords");

            migrationBuilder.DropTable(
                name: "Icons");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Payments");
        }
    }
}
