using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewImageUrlsAndBookingToEnglish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_AspNetUsers_KlantId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "Totaalprijs",
                table: "Bookings",
                newName: "Totalprice");

            migrationBuilder.RenameColumn(
                name: "KlantId",
                table: "Bookings",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "Datum",
                table: "Bookings",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "Bevestigd",
                table: "Bookings",
                newName: "Confirmed");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_KlantId",
                table: "Bookings",
                newName: "IX_Bookings_CustomerId");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 1,
                column: "Image",
                value: "/img/animals/monkey.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 2,
                column: "Image",
                value: "/img/animals/elephant.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 3,
                column: "Image",
                value: "/img/animals/zebra.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 4,
                column: "Image",
                value: "/img/animals/lion.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 5,
                column: "Image",
                value: "/img/animals/dog.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 6,
                column: "Image",
                value: "/img/animals/donkey.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 7,
                column: "Image",
                value: "/img/animals/cow.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 8,
                column: "Image",
                value: "/img/animals/duck.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 9,
                column: "Image",
                value: "/img/animals/chicken.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 10,
                column: "Image",
                value: "/img/animals/penguin.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 11,
                column: "Image",
                value: "/img/animals/polar-bear.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 12,
                column: "Image",
                value: "/img/animals/sea-lion.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 13,
                column: "Image",
                value: "/img/animals/camel.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 14,
                column: "Image",
                value: "/img/animals/snake.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 15,
                column: "Image",
                value: "/img/animals/t-rex.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 16,
                column: "Image",
                value: "/img/animals/unicorn.png");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_AspNetUsers_CustomerId",
                table: "Bookings",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_AspNetUsers_CustomerId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "Totalprice",
                table: "Bookings",
                newName: "Totaalprijs");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Bookings",
                newName: "Datum");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Bookings",
                newName: "KlantId");

            migrationBuilder.RenameColumn(
                name: "Confirmed",
                table: "Bookings",
                newName: "Bevestigd");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_CustomerId",
                table: "Bookings",
                newName: "IX_Bookings_KlantId");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 1,
                column: "Image",
                value: "/images/aap.jpg");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 2,
                column: "Image",
                value: "/images/olifant.jpg");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 3,
                column: "Image",
                value: "/images/zebra.jpg");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 4,
                column: "Image",
                value: "/images/leeuw.jpg");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 5,
                column: "Image",
                value: "/images/hond.jpg");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 6,
                column: "Image",
                value: "/images/ezel.jpg");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 7,
                column: "Image",
                value: "/images/koe.jpg");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 8,
                column: "Image",
                value: "/images/eend.jpg");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 9,
                column: "Image",
                value: "/images/kuiken.jpg");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 10,
                column: "Image",
                value: "/images/pinguin.jpg");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 11,
                column: "Image",
                value: "/images/ijsbeer.jpg");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 12,
                column: "Image",
                value: "/images/zeehond.jpg");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 13,
                column: "Image",
                value: "/images/kameel.jpg");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 14,
                column: "Image",
                value: "/images/slang.jpg");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 15,
                column: "Image",
                value: "/images/trex.jpg");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 16,
                column: "Image",
                value: "/images/unicorn.jpg");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_AspNetUsers_KlantId",
                table: "Bookings",
                column: "KlantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
