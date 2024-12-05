using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Application.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Animals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Infix = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    CustomerCardType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Bevestigd = table.Column<bool>(type: "bit", nullable: false),
                    Totaalprijs = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    KlantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Users_KlantId",
                        column: x => x.KlantId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    AnimalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingDetails_Animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingDetails_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "Id", "Image", "Name", "Price", "Type" },
                values: new object[,]
                {
                    { 1, "/images/aap.jpg", "Aap", 25.0m, "Jungle" },
                    { 2, "/images/olifant.jpg", "Olifant", 100.0m, "Jungle" },
                    { 3, "/images/zebra.jpg", "Zebra", 50.0m, "Jungle" },
                    { 4, "/images/leeuw.jpg", "Leeuw", 150.0m, "Jungle" },
                    { 5, "/images/hond.jpg", "Hond", 20.0m, "Farm" },
                    { 6, "/images/ezel.jpg", "Ezel", 30.0m, "Farm" },
                    { 7, "/images/koe.jpg", "Koe", 50.0m, "Farm" },
                    { 8, "/images/eend.jpg", "Eend", 15.0m, "Farm" },
                    { 9, "/images/kuiken.jpg", "Kuiken", 10.0m, "Farm" },
                    { 10, "/images/pinguin.jpg", "Pinguïn", 80.0m, "Snow" },
                    { 11, "/images/ijsbeer.jpg", "IJsbeer", 200.0m, "Snow" },
                    { 12, "/images/zeehond.jpg", "Zeehond", 60.0m, "Snow" },
                    { 13, "/images/kameel.jpg", "Kameel", 70.0m, "Desert" },
                    { 14, "/images/slang.jpg", "Slang", 40.0m, "Desert" },
                    { 15, "/images/trex.jpg", "T-Rex", 500.0m, "Vip" },
                    { 16, "/images/unicorn.jpg", "Unicorn", 1000.0m, "Vip" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "CustomerCardType", "Email", "FirstName", "Infix", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "Straat 1, 1234 AB", null, "jan@example.com", "Jan", null, "Jansen", "0612345678" },
                    { 2, "Straat 2, 1234 CD", "Silver", "pite@example.com", "Piet", null, "Pietersen", "0612345678" },
                    { 3, "Straat 3, 1234 EF", "Gold", "karin@example.com", "Karin", null, "Klaassen", "0612345678" },
                    { 4, "Straat 4, 1234 GH", "Platinum", "sophie@example.com", "Sophie", "de", "Groot", "0612345678" }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "Bevestigd", "Datum", "KlantId", "Totaalprijs" },
                values: new object[] { 1, false, new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 150.0m });

            migrationBuilder.InsertData(
                table: "BookingDetails",
                columns: new[] { "Id", "AnimalId", "BookingId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 3, 1 },
                    { 3, 7, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_AnimalId",
                table: "BookingDetails",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_BookingId",
                table: "BookingDetails",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_KlantId",
                table: "Bookings",
                column: "KlantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingDetails");

            migrationBuilder.DropTable(
                name: "Animals");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
