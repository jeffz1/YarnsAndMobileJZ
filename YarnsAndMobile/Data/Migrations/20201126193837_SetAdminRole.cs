using Microsoft.EntityFrameworkCore.Migrations;

namespace YarnsAndMobile.Data.Migrations
{
    public partial class SetAdminRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3608b611-1bee-4fc1-8fd2-0a71c3613c65", "e5b03758-ba6d-4ddf-a8c2-331da7ef0217", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3608b611-1bee-4fc1-8fd2-0a71c3613c65");
        }
    }
}
