using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BoPlats.Migrations
{
    public partial class FirstCommit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Apartment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adress = table.Column<string>(maxLength: 50, nullable: false),
                    Elevator = table.Column<bool>(nullable: false),
                    NumberOfRooms = table.Column<string>(maxLength: 10, nullable: false),
                    Balcony = table.Column<bool>(nullable: false),
                    SquareMeter = table.Column<string>(nullable: false),
                    Floor = table.Column<string>(nullable: false),
                    Rent = table.Column<string>(nullable: false),
                    RegisterTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apartment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Apply",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    Salary = table.Column<string>(nullable: false),
                    socSecNum = table.Column<string>(nullable: false),
                    ApartmentForeginKey = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apply", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Apply_Apartment_ApartmentForeginKey",
                        column: x => x.ApartmentForeginKey,
                        principalTable: "Apartment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Apply_ApartmentForeginKey",
                table: "Apply",
                column: "ApartmentForeginKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Apply");

            migrationBuilder.DropTable(
                name: "Apartment");
        }
    }
}
