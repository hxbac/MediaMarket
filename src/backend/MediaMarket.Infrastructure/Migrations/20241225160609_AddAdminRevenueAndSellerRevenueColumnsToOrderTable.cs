using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaMarket.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminRevenueAndSellerRevenueColumnsToOrderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AdminRevenue",
                table: "Orders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "SellerRevenue",
                table: "Orders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminRevenue",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SellerRevenue",
                table: "Orders");
        }
    }
}
