using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aljas_Consultation.Data.Migrations
{
    public partial class AddPeriod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Period",
                table: "Consultation");

            migrationBuilder.AddColumn<int>(
                name: "PeriodId",
                table: "Consultation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Period",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Period", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consultation_PeriodId",
                table: "Consultation",
                column: "PeriodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultation_Period_PeriodId",
                table: "Consultation",
                column: "PeriodId",
                principalTable: "Period",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultation_Period_PeriodId",
                table: "Consultation");

            migrationBuilder.DropTable(
                name: "Period");

            migrationBuilder.DropIndex(
                name: "IX_Consultation_PeriodId",
                table: "Consultation");

            migrationBuilder.DropColumn(
                name: "PeriodId",
                table: "Consultation");

            migrationBuilder.AddColumn<string>(
                name: "Period",
                table: "Consultation",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
