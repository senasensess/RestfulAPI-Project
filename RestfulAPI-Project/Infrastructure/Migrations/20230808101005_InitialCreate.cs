using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestfulAPI_Project.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Password", "Status", "UpdatedDate", "UserName" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 8, 8, 13, 10, 5, 111, DateTimeKind.Local).AddTicks(2255), null, "123", 1, null, "sinaemre" },
                    { 2, new DateTime(2023, 8, 8, 13, 10, 5, 111, DateTimeKind.Local).AddTicks(2260), null, "123", 1, null, "sinaemre2" },
                    { 3, new DateTime(2023, 8, 8, 13, 10, 5, 111, DateTimeKind.Local).AddTicks(2262), null, "123", 1, null, "sinaemre3" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Description", "Name", "Status", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 8, 8, 13, 10, 5, 111, DateTimeKind.Local).AddTicks(2019), null, "Et ve tavuk ürünleri bulunur!!", "Kasap", 1, null },
                    { 2, new DateTime(2023, 8, 8, 13, 10, 5, 111, DateTimeKind.Local).AddTicks(2034), null, "Meyve ve sebzeler bulunur!!", "Manav", 1, null },
                    { 3, new DateTime(2023, 8, 8, 13, 10, 5, 111, DateTimeKind.Local).AddTicks(2035), null, "Süt ürünleri bulunur!!", "Şarküteri", 1, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
