using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI_Service.Migrations
{
    public partial class InitCreateFKinPrdctMvmnts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductMovementss",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductMovementss_ProductId",
                table: "ProductMovementss",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMovementss_Products_ProductId",
                table: "ProductMovementss",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductMovementss_Products_ProductId",
                table: "ProductMovementss");

            migrationBuilder.DropIndex(
                name: "IX_ProductMovementss_ProductId",
                table: "ProductMovementss");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductMovementss");
        }
    }
}
