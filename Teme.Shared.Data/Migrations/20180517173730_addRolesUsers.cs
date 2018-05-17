using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Teme.Shared.Data.Migrations
{
    public partial class addRolesUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolesPermissions_Roles_RoleId",
                table: "RolesPermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolesPermissions",
                table: "RolesPermissions");

            migrationBuilder.RenameTable(
                name: "RolesPermissions",
                newName: "Permissions");

            migrationBuilder.RenameIndex(
                name: "IX_RolesPermissions_RoleId",
                table: "Permissions",
                newName: "IX_Permissions_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Permissions",
                table: "Permissions",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AuthUserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    DateUpdate = table.Column<DateTime>(nullable: false),
                    RoleId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthUserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuthUserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuthUserRoles_AuthUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AuthUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthUserRoles_RoleId",
                table: "AuthUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthUserRoles_UserId",
                table: "AuthUserRoles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Roles_RoleId",
                table: "Permissions",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Roles_RoleId",
                table: "Permissions");

            migrationBuilder.DropTable(
                name: "AuthUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Permissions",
                table: "Permissions");

            migrationBuilder.RenameTable(
                name: "Permissions",
                newName: "RolesPermissions");

            migrationBuilder.RenameIndex(
                name: "IX_Permissions_RoleId",
                table: "RolesPermissions",
                newName: "IX_RolesPermissions_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolesPermissions",
                table: "RolesPermissions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RolesPermissions_Roles_RoleId",
                table: "RolesPermissions",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
