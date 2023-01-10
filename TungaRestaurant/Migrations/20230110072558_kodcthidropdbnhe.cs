using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TungaRestaurant.Migrations
{
    public partial class kodcthidropdbnhe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ReservationEnd",
                table: "Reservations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservationEnd",
                table: "Reservations");
        }
    }
}
