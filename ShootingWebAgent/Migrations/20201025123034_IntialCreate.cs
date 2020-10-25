using Microsoft.EntityFrameworkCore.Migrations;

namespace ShootingWebAgent.Migrations
{
    public partial class IntialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clubs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true),
                    UUID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clubs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Competition",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Evaluation = table.Column<string>(nullable: true),
                    DateTime = table.Column<string>(nullable: true),
                    UUID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competition", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DisagJsons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MessageType = table.Column<string>(nullable: true),
                    MessageVerb = table.Column<string>(nullable: true),
                    Sequential = table.Column<bool>(nullable: false),
                    Ranges = table.Column<int>(nullable: false),
                    UUID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisagJsons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MenuID = table.Column<string>(nullable: true),
                    MenuPointName = table.Column<string>(nullable: true),
                    MenuItemName = table.Column<string>(nullable: true),
                    UUID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shooters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Firstname = table.Column<string>(nullable: true),
                    Lastname = table.Column<string>(nullable: true),
                    Birthyear = table.Column<int>(nullable: false),
                    InternalID = table.Column<string>(nullable: true),
                    Identification = table.Column<string>(nullable: true),
                    Team = table.Column<string>(nullable: true),
                    ClubId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shooters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shooters_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Objects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DecimalValue = table.Column<double>(nullable: false),
                    ShotDateTime = table.Column<string>(nullable: true),
                    TLStatus = table.Column<string>(nullable: true),
                    LastTLChange = table.Column<int>(nullable: false),
                    Source = table.Column<string>(nullable: true),
                    Range = table.Column<int>(nullable: false),
                    ShooterId = table.Column<int>(nullable: true),
                    CompetitionId = table.Column<int>(nullable: true),
                    DiscType = table.Column<string>(nullable: true),
                    X = table.Column<int>(nullable: false),
                    Y = table.Column<int>(nullable: false),
                    Distance = table.Column<double>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    FullValue = table.Column<int>(nullable: false),
                    DecValue = table.Column<double>(nullable: false),
                    Run = table.Column<int>(nullable: false),
                    IsValid = table.Column<bool>(nullable: false),
                    IsWarmup = table.Column<bool>(nullable: false),
                    IsHot = table.Column<bool>(nullable: false),
                    IsDummy = table.Column<bool>(nullable: false),
                    IsInnerten = table.Column<bool>(nullable: false),
                    IsShootoff = table.Column<bool>(nullable: false),
                    MenuItemId = table.Column<int>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    UUID = table.Column<string>(nullable: true),
                    DisagJsonId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Objects_Competition_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Objects_DisagJsons_DisagJsonId",
                        column: x => x.DisagJsonId,
                        principalTable: "DisagJsons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Objects_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Objects_Shooters_ShooterId",
                        column: x => x.ShooterId,
                        principalTable: "Shooters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Objects_CompetitionId",
                table: "Objects",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Objects_DisagJsonId",
                table: "Objects",
                column: "DisagJsonId");

            migrationBuilder.CreateIndex(
                name: "IX_Objects_MenuItemId",
                table: "Objects",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Objects_ShooterId",
                table: "Objects",
                column: "ShooterId");

            migrationBuilder.CreateIndex(
                name: "IX_Shooters_ClubId",
                table: "Shooters",
                column: "ClubId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Objects");

            migrationBuilder.DropTable(
                name: "Competition");

            migrationBuilder.DropTable(
                name: "DisagJsons");

            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "Shooters");

            migrationBuilder.DropTable(
                name: "Clubs");
        }
    }
}
