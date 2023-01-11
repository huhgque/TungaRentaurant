using Microsoft.EntityFrameworkCore.Migrations;

namespace TungaRestaurant.Migrations
{
    public partial class fixOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserInfoId",
                table: "Orders",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserInfoId",
                table: "Orders",
                column: "UserInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_UserInfoId",
                table: "Orders",
                column: "UserInfoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_UserInfoId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserInfoId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UserInfoId",
                table: "Orders");
        }
    }
}
