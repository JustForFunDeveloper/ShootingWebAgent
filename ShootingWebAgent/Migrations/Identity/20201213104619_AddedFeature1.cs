using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShootingWebAgent.Migrations.Identity
{
    public partial class AddedFeature1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxMatchCount",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "TrialEnd",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserMatches",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MatchId = table.Column<int>(nullable: false),
                    ShootingWebAgentUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMatches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMatches_AspNetUsers_ShootingWebAgentUserId",
                        column: x => x.ShootingWebAgentUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserMatches_ShootingWebAgentUserId",
                table: "UserMatches",
                column: "ShootingWebAgentUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserMatches");

            migrationBuilder.DropColumn(
                name: "MaxMatchCount",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TrialEnd",
                table: "AspNetUsers");
        }
    }
}
