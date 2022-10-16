using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Infraestructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cinemas",
                columns: table => new
                {
                    CinemaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModificationDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(50)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "varchar(50)", nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: true),
                    Address = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cinemas", x => x.CinemaId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModificationDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(50)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "varchar(50)", nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    ApiKey = table.Column<string>(type: "varchar(250)", nullable: true),
                    Name = table.Column<string>(type: "varchar(50)", nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModificationDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(50)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "varchar(50)", nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: true),
                    Description = table.Column<string>(type: "varchar(50)", nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    CinemaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_Cinemas_CinemaId",
                        column: x => x.CinemaId,
                        principalTable: "Cinemas",
                        principalColumn: "CinemaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_CinemaId",
                table: "Rooms",
                column: "CinemaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Cinemas");
        }
    }
}
