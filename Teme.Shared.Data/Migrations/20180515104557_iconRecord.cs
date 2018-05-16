using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Teme.Shared.Data.Migrations
{
    public partial class iconRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IconRecordId",
                table: "AuthUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuthUsers_IconRecordId",
                table: "AuthUsers",
                column: "IconRecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthUsers_IconRecords_IconRecordId",
                table: "AuthUsers",
                column: "IconRecordId",
                principalTable: "IconRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthUsers_IconRecords_IconRecordId",
                table: "AuthUsers");

            migrationBuilder.DropIndex(
                name: "IX_AuthUsers_IconRecordId",
                table: "AuthUsers");

            migrationBuilder.DropColumn(
                name: "IconRecordId",
                table: "AuthUsers");
        }
    }
}
