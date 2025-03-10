using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Heroes.Migrations
{
    /// <inheritdoc />
    public partial class init4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SuitColors",
                table: "Heroes",
                newName: "SuitColor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SuitColor",
                table: "Heroes",
                newName: "SuitColors");
        }
    }
}
