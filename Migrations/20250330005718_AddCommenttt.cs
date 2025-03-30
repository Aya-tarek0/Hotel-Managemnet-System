using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mvcproj.Migrations
{
    /// <inheritdoc />
    public partial class AddCommenttt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Guests_GuestID",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Rooms_RoomID",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.RenameTable(
                name: "Comment",
                newName: "Comments");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_RoomID",
                table: "Comments",
                newName: "IX_Comments_RoomID");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_GuestID",
                table: "Comments",
                newName: "IX_Comments_GuestID");

            migrationBuilder.AlterColumn<string>(
                name: "GuestID",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "CommentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Guests_GuestID",
                table: "Comments",
                column: "GuestID",
                principalTable: "Guests",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Rooms_RoomID",
                table: "Comments",
                column: "RoomID",
                principalTable: "Rooms",
                principalColumn: "RoomID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Guests_GuestID",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Rooms_RoomID",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "Comment");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_RoomID",
                table: "Comment",
                newName: "IX_Comment_RoomID");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_GuestID",
                table: "Comment",
                newName: "IX_Comment_GuestID");

            migrationBuilder.AlterColumn<string>(
                name: "GuestID",
                table: "Comment",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                column: "CommentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Guests_GuestID",
                table: "Comment",
                column: "GuestID",
                principalTable: "Guests",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Rooms_RoomID",
                table: "Comment",
                column: "RoomID",
                principalTable: "Rooms",
                principalColumn: "RoomID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
