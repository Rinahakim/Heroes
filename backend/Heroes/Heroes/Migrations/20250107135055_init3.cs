using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Heroes.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SuitColors");

            migrationBuilder.AddColumn<string>(
                name: "SuitColors",
                table: "Heroes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SuitColors",
                table: "Heroes");

            migrationBuilder.CreateTable(
                name: "SuitColors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeroId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuitColors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuitColors_Heroes_HeroId",
                        column: x => x.HeroId,
                        principalTable: "Heroes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SuitColors_HeroId",
                table: "SuitColors",
                column: "HeroId");
        }
    }
}
