using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineStore.Migrations
{
    /// <inheritdoc />
    public partial class updatecart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Carts",
                table: "Carts");

            migrationBuilder.AddColumn<string>(
                name: "SellerId",
                table: "Carts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carts",
                table: "Carts",
                columns: new[] { "ProductId", "UserId", "SellerId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Carts",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Carts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carts",
                table: "Carts",
                columns: new[] { "ProductId", "UserId" });
        }
    }
}
