using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StiktifyShop.Migrations
{
    /// <inheritdoc />
    public partial class v6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductRatings_ProductItems_ProductItemId",
                table: "ProductRatings");

            migrationBuilder.RenameColumn(
                name: "ProductItemId",
                table: "ProductRatings",
                newName: "VariantId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductRatings_ProductItemId",
                table: "ProductRatings",
                newName: "IX_ProductRatings_VariantId");

            migrationBuilder.AddColumn<string>(
                name: "OptionId",
                table: "ProductRatings",
                type: "character varying(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OrderId",
                table: "ProductRatings",
                type: "character varying(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRatings_OptionId",
                table: "ProductRatings",
                column: "OptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRatings_OrderId",
                table: "ProductRatings",
                column: "OrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRatings_Orders_OrderId",
                table: "ProductRatings",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRatings_ProductOptions_OptionId",
                table: "ProductRatings",
                column: "OptionId",
                principalTable: "ProductOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRatings_ProductVariants_VariantId",
                table: "ProductRatings",
                column: "VariantId",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductRatings_Orders_OrderId",
                table: "ProductRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductRatings_ProductOptions_OptionId",
                table: "ProductRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductRatings_ProductVariants_VariantId",
                table: "ProductRatings");

            migrationBuilder.DropIndex(
                name: "IX_ProductRatings_OptionId",
                table: "ProductRatings");

            migrationBuilder.DropIndex(
                name: "IX_ProductRatings_OrderId",
                table: "ProductRatings");

            migrationBuilder.DropColumn(
                name: "OptionId",
                table: "ProductRatings");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "ProductRatings");

            migrationBuilder.RenameColumn(
                name: "VariantId",
                table: "ProductRatings",
                newName: "ProductItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductRatings_VariantId",
                table: "ProductRatings",
                newName: "IX_ProductRatings_ProductItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRatings_ProductItems_ProductItemId",
                table: "ProductRatings",
                column: "ProductItemId",
                principalTable: "ProductItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
