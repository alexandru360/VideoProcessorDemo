using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class Update_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hls264",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FileName = table.Column<string>(maxLength: 250, nullable: true),
                    FileContents = table.Column<byte[]>(type: "Blob", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hls264", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MultibitHls",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FileName = table.Column<string>(maxLength: 250, nullable: true),
                    FileContents = table.Column<byte[]>(type: "Blob", nullable: true),
                    GroupName = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultibitHls", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hls264");

            migrationBuilder.DropTable(
                name: "MultibitHls");
        }
    }
}
