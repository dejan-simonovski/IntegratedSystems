﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConcertApp.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class nmg3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_AspNetUsers_UserId1",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_UserId1",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Ticket");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Ticket",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_UserId",
                table: "Ticket",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_AspNetUsers_UserId",
                table: "Ticket",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_AspNetUsers_UserId",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_UserId",
                table: "Ticket");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Ticket",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Ticket",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_UserId1",
                table: "Ticket",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_AspNetUsers_UserId1",
                table: "Ticket",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
