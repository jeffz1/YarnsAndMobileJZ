using Microsoft.EntityFrameworkCore.Migrations;

namespace YarnsAndMobile.Data.Migrations
{
    public partial class AddSalePriceToBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CurrentSalePrice",
                table: "Books",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentSalePrice",
                table: "Books");
        }
    }
}
