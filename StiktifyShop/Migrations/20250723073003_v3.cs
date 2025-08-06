using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StiktifyShop.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_ProductItems_ProductItemId",
                table: "Carts");

            migrationBuilder.RenameColumn(
                name: "ProductItemId",
                table: "Carts",
                newName: "VariantId");

            migrationBuilder.RenameIndex(
                name: "IX_Carts_ProductItemId",
                table: "Carts",
                newName: "IX_Carts_VariantId");

            migrationBuilder.AddColumn<string>(
                name: "ImageUri",
                table: "Carts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OptionId",
                table: "Carts",
                type: "character varying(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductId",
                table: "Carts",
                type: "character varying(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_OptionId",
                table: "Carts",
                column: "OptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ProductId",
                table: "Carts",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_ProductOptions_OptionId",
                table: "Carts",
                column: "OptionId",
                principalTable: "ProductOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_ProductVariants_VariantId",
                table: "Carts",
                column: "VariantId",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Products_ProductId",
                table: "Carts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_ProductOptions_OptionId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_ProductVariants_VariantId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Products_ProductId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_OptionId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_ProductId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "ImageUri",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "OptionId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Carts");

            migrationBuilder.RenameColumn(
                name: "VariantId",
                table: "Carts",
                newName: "ProductItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Carts_VariantId",
                table: "Carts",
                newName: "IX_Carts_ProductItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_ProductItems_ProductItemId",
                table: "Carts",
                column: "ProductItemId",
                principalTable: "ProductItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
