using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Migrations
{
    public partial class WishAddId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BookCount",
                table: "Wishes",
                newName: "Quantity");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Wishes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Wishes");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Wishes",
                newName: "BookCount");
        }
    }
}
