using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnoProducts.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCreatorUserIDToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CreatorUserID",
                schema: "identity",
                table: "Products",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "CreatorUserID",
                schema: "identity",
                table: "Products",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
