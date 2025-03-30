using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mvcproj.Migrations
{
    /// <inheritdoc />
    public partial class st : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_Hotels_HotelID",
                table: "Staffs");

            migrationBuilder.AlterColumn<int>(
                name: "HotelID",
                table: "Staffs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_Hotels_HotelID",
                table: "Staffs",
                column: "HotelID",
                principalTable: "Hotels",
                principalColumn: "HotelID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_Hotels_HotelID",
                table: "Staffs");

            migrationBuilder.AlterColumn<int>(
                name: "HotelID",
                table: "Staffs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_Hotels_HotelID",
                table: "Staffs",
                column: "HotelID",
                principalTable: "Hotels",
                principalColumn: "HotelID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
