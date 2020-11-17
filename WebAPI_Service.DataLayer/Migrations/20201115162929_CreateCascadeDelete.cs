using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI_Service.DataLayer.Migrations
{
    public partial class CreateCascadeDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductMovementss_Products_ProductId",
                table: "ProductMovementss");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductUoms_UomId",
                table: "Products");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMovementss_Products_ProductId",
                table: "ProductMovementss",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductUoms_UomId",
                table: "Products",
                column: "UomId",
                principalTable: "ProductUoms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductMovementss_Products_ProductId",
                table: "ProductMovementss");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductUoms_UomId",
                table: "Products");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMovementss_Products_ProductId",
                table: "ProductMovementss",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductUoms_UomId",
                table: "Products",
                column: "UomId",
                principalTable: "ProductUoms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
