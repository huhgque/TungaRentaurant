using Microsoft.EntityFrameworkCore.Migrations;

namespace TungaRestaurant.Migrations
{
    public partial class fixfood : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BranchId",
                table: "Foods",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Foods_BranchId",
                table: "Foods",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_Branches_BranchId",
                table: "Foods",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foods_Branches_BranchId",
                table: "Foods");

            migrationBuilder.DropIndex(
                name: "IX_Foods_BranchId",
                table: "Foods");

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
