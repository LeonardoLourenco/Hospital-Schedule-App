using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalSchedule.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            /*migrationBuilder.AddColumn<bool>(
                name: "HasChild",
                table: "Rules",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "InBeetweenHoursShift",
                table: "Rules",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NurseAge",
                table: "Rules",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShiftDuration",
                table: "Rules",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WeeklyHours",
                table: "Rules",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YoungestChildAge",
                table: "Rules",
                nullable: false,
                defaultValue: 0);*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "Rules");
        }
    }
}
