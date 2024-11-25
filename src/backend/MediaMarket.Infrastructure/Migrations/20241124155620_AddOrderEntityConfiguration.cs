using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaMarket.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderEntityConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Users_UserId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_products_ProductId",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_UserId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Order");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameIndex(
                name: "IX_Order_ProductId",
                table: "Orders",
                newName: "IX_Orders_ProductId");

            migrationBuilder.AlterColumn<byte>(
                name: "Status",
                table: "Orders",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CreatedBy",
                table: "Orders",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_CreatedBy",
                table: "Orders",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_products_ProductId",
                table: "Orders",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_CreatedBy",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_products_ProductId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CreatedBy",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_ProductId",
                table: "Order",
                newName: "IX_Order_ProductId");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Order",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldDefaultValue: (byte)0);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Order",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserId",
                table: "Order",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Users_UserId",
                table: "Order",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_products_ProductId",
                table: "Order",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
