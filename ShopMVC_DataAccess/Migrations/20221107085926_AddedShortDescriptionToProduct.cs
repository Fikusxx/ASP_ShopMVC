﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopMVC_DataAccess.Migrations
{
    public partial class AddedShortDescriptionToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Products");
        }
    }
}
