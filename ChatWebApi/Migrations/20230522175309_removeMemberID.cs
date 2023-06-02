﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatWebApi.Migrations
{
    /// <inheritdoc />
    public partial class removeMemberID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Members_MemberId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_MemberId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "Messages");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_FromId",
                table: "Messages",
                column: "FromId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Members_FromId",
                table: "Messages",
                column: "FromId",
                principalTable: "Members",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Members_FromId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_FromId",
                table: "Messages");

            migrationBuilder.AddColumn<Guid>(
                name: "MemberId",
                table: "Messages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Messages_MemberId",
                table: "Messages",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Members_MemberId",
                table: "Messages",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
