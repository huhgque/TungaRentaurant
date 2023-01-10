using Microsoft.EntityFrameworkCore.Migrations;

namespace TungaRestaurant.Migrations
{
    public partial class adduserpreferbranch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BranchId",
                table: "Foods",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PreferBranchId",
                table: "AspNetUsers",
                nullable: true);

            

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PreferBranchId",
                table: "AspNetUsers",
                column: "PreferBranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Branch_PreferBranchId",
                table: "AspNetUsers",
                column: "PreferBranchId",
                principalTable: "Branch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Branch_PreferBranchId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Foods_Branch_BranchId",
                table: "Foods");

            migrationBuilder.DropIndex(
                name: "IX_Foods_BranchId",
                table: "Foods");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PreferBranchId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PreferBranchId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "BranchId",
                table: "Foods",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
