using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ryzen.Shop.Catalog.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updatePrmotions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinimumSpendDiscountAmount",
                table: "Promotions");

            migrationBuilder.AlterColumn<bool>(
                name: "GetOneFree",
                table: "Promotions",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "GetOneFree",
                table: "Promotions",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MinimumSpendDiscountAmount",
                table: "Promotions",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
