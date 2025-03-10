using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Heroes.Migrations
{
    /// <inheritdoc />
    public partial class init6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainers_AspNetUsers_Id",
                table: "Trainers");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Trainers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Trainers_AppUserId",
                table: "Trainers",
                column: "AppUserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Trainers_AspNetUsers_AppUserId",
                table: "Trainers",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainers_AspNetUsers_AppUserId",
                table: "Trainers");

            migrationBuilder.DropIndex(
                name: "IX_Trainers_AppUserId",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Trainers");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainers_AspNetUsers_Id",
                table: "Trainers",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
