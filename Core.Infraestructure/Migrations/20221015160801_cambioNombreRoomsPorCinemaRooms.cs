using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Infraestructure.Migrations
{
    public partial class cambioNombreRoomsPorCinemaRooms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Cinemas_CinemaId",
                table: "Rooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms");

            migrationBuilder.RenameTable(
                name: "Rooms",
                newName: "CinemaRooms");

            migrationBuilder.RenameIndex(
                name: "IX_Rooms_CinemaId",
                table: "CinemaRooms",
                newName: "IX_CinemaRooms_CinemaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CinemaRooms",
                table: "CinemaRooms",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CinemaRooms_Cinemas_CinemaId",
                table: "CinemaRooms",
                column: "CinemaId",
                principalTable: "Cinemas",
                principalColumn: "CinemaId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CinemaRooms_Cinemas_CinemaId",
                table: "CinemaRooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CinemaRooms",
                table: "CinemaRooms");

            migrationBuilder.RenameTable(
                name: "CinemaRooms",
                newName: "Rooms");

            migrationBuilder.RenameIndex(
                name: "IX_CinemaRooms_CinemaId",
                table: "Rooms",
                newName: "IX_Rooms_CinemaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Cinemas_CinemaId",
                table: "Rooms",
                column: "CinemaId",
                principalTable: "Cinemas",
                principalColumn: "CinemaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
