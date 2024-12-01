using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaMarket.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStripeAccountIdToUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountId",
                table: "Withdrawals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StripeAccountId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Withdrawals");

            migrationBuilder.DropColumn(
                name: "StripeAccountId",
                table: "Users");
        }
    }
}
