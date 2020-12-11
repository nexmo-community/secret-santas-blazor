using Microsoft.EntityFrameworkCore.Migrations;

namespace SecretSanta.Migrations
{
    public partial class inital_create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Participants",
                columns: table => new
                {
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    GiftIdeas = table.Column<string>(type: "text", nullable: true),
                    RequestId = table.Column<string>(type: "text", nullable: true),
                    MatchForeignKey = table.Column<string>(type: "text", nullable: true),
                    Role = table.Column<string>(type: "text", nullable: true),
                    HasGiver = table.Column<bool>(type: "boolean", nullable: false),
                    GiverForeignKey = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.PhoneNumber);
                    table.ForeignKey(
                        name: "FK_Participants_Participants_GiverForeignKey",
                        column: x => x.GiverForeignKey,
                        principalTable: "Participants",
                        principalColumn: "PhoneNumber",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Participants_Participants_MatchForeignKey",
                        column: x => x.MatchForeignKey,
                        principalTable: "Participants",
                        principalColumn: "PhoneNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Participants_GiverForeignKey",
                table: "Participants",
                column: "GiverForeignKey");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_MatchForeignKey",
                table: "Participants",
                column: "MatchForeignKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Participants");
        }
    }
}
