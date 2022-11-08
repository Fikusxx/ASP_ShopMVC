using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrialMVC.Migrations
{
    public partial class AddedTweaksToUniqueConstraintOfOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Orders_Number_ProviderId",
                table: "Orders");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Number_ProviderId",
                table: "Orders",
                columns: new[] { "Number", "ProviderId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_Number_ProviderId",
                table: "Orders");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Orders_Number_ProviderId",
                table: "Orders",
                columns: new[] { "Number", "ProviderId" });
        }
    }
}
