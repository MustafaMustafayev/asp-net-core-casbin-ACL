using Microsoft.EntityFrameworkCore.Migrations;

namespace CasbinACLvsRBAC.Migrations
{
    public partial class casbin2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CasbinRule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PType = table.Column<string>(nullable: true),
                    V0 = table.Column<string>(nullable: true),
                    V1 = table.Column<string>(nullable: true),
                    V2 = table.Column<string>(nullable: true),
                    V3 = table.Column<string>(nullable: true),
                    V4 = table.Column<string>(nullable: true),
                    V5 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CasbinRule", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CasbinRule");
        }
    }
}
