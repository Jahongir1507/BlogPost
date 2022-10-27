using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeebApp.Migrations
{
    public partial class Post_added_FK_to_Status : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_StatusId",
                table: "Posts",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Statuses_StatusId",
                table: "Posts",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Statuses_StatusId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_StatusId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Posts");
        }
    }
}
