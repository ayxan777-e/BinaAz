using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePropertyMedia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyMedia_PropertyAd_PropertyAdId",
                table: "PropertyMedia");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyMedia",
                table: "PropertyMedia");

            migrationBuilder.RenameTable(
                name: "PropertyMedia",
                newName: "PropertyMedias");

            migrationBuilder.RenameColumn(
                name: "MediaUrl",
                table: "PropertyMedias",
                newName: "ObjectKey");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyMedia_PropertyAdId",
                table: "PropertyMedias",
                newName: "IX_PropertyMedias_PropertyAdId");

            migrationBuilder.AlterColumn<string>(
                name: "MediaType",
                table: "PropertyMedias",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "PropertyMedias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyMedias",
                table: "PropertyMedias",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyMedias_PropertyAd_PropertyAdId",
                table: "PropertyMedias",
                column: "PropertyAdId",
                principalTable: "PropertyAd",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyMedias_PropertyAd_PropertyAdId",
                table: "PropertyMedias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyMedias",
                table: "PropertyMedias");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "PropertyMedias");

            migrationBuilder.RenameTable(
                name: "PropertyMedias",
                newName: "PropertyMedia");

            migrationBuilder.RenameColumn(
                name: "ObjectKey",
                table: "PropertyMedia",
                newName: "MediaUrl");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyMedias_PropertyAdId",
                table: "PropertyMedia",
                newName: "IX_PropertyMedia_PropertyAdId");

            migrationBuilder.AlterColumn<string>(
                name: "MediaType",
                table: "PropertyMedia",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyMedia",
                table: "PropertyMedia",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyMedia_PropertyAd_PropertyAdId",
                table: "PropertyMedia",
                column: "PropertyAdId",
                principalTable: "PropertyAd",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
