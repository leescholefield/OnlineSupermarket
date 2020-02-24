using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineSupermarket.Migrations
{
    public partial class PromotionImageNameField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "promotions",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "promotions");
        }
    }
}
