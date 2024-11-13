using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniMvcProject.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class fixedProductTag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductTags_ProductImages_ProductImageId",
                table: "ProductTags");

            migrationBuilder.DropIndex(
                name: "IX_ProductTags_ProductImageId",
                table: "ProductTags");

            migrationBuilder.DropColumn(
                name: "ProductImageId",
                table: "ProductTags");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductImageId",
                table: "ProductTags",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductTags_ProductImageId",
                table: "ProductTags",
                column: "ProductImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTags_ProductImages_ProductImageId",
                table: "ProductTags",
                column: "ProductImageId",
                principalTable: "ProductImages",
                principalColumn: "Id");
        }
    }
}
