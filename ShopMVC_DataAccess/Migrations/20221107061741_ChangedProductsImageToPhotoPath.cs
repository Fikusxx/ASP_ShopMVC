using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopMVC_DataAccess.Migrations
{
    public partial class ChangedProductsImageToPhotoPath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Products",
                newName: "PhotoPath");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhotoPath",
                table: "Products",
                newName: "Image");
        }
    }
}
