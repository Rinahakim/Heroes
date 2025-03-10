using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Heroes.Migrations
{
    /// <inheritdoc />
    public partial class init10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Heroes_AspNetUsers_TrainerId",
                table: "Heroes");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Heroes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.CreateIndex(
                name: "IX_Heroes_AppUserId",
                table: "Heroes",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Heroes_AspNetUsers_AppUserId",
                table: "Heroes",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Heroes_AspNetUsers_TrainerId",
                table: "Heroes",
                column: "TrainerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Heroes_AspNetUsers_AppUserId",
                table: "Heroes");

            migrationBuilder.DropForeignKey(
                name: "FK_Heroes_AspNetUsers_TrainerId",
                table: "Heroes");

            migrationBuilder.DropIndex(
                name: "IX_Heroes_AppUserId",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Heroes");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Heroes_AspNetUsers_TrainerId",
                table: "Heroes",
                column: "TrainerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
