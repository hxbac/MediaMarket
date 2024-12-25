using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaMarket.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEventDiscountTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicableType",
                table: "EventDiscounts");

            migrationBuilder.AddColumn<long>(
                name: "DiscountMinValue",
                table: "EventDiscounts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountMinValue",
                table: "EventDiscounts");

            migrationBuilder.AddColumn<int>(
                name: "ApplicableType",
                table: "EventDiscounts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
