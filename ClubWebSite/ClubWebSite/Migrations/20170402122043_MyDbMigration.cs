using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubWebSite.Migrations
{
    public partial class MyDbMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Actives",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Address = table.Column<string>(nullable: true),
                    BeginTime = table.Column<DateTime>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    IsEnroll = table.Column<bool>(nullable: false),
                    Logo = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PeopleNumber = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actives", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Actives_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enrolls",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ActiveID = table.Column<int>(nullable: false),
                    Contact = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrolls", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Enrolls_Actives_ActiveID",
                        column: x => x.ActiveID,
                        principalTable: "Actives",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actives_UserID",
                table: "Actives",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Enrolls_ActiveID",
                table: "Enrolls",
                column: "ActiveID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enrolls");

            migrationBuilder.DropTable(
                name: "Actives");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
