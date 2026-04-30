using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BadgeCatalog.Adapters.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNameImgUrlForTemplateID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "BadgeClasses",
                newName: "TemplateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TemplateId",
                table: "BadgeClasses",
                newName: "ImageUrl");
        }
    }
}
