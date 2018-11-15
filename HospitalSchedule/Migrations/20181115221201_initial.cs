using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalSchedule.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nurses",
                columns: table => new
                {
                    NurseID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Specialties = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    CellPhoneNumber = table.Column<string>(nullable: false),
                    YoungestChildBirthDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nurses", x => x.NurseID);
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    FinishedDate = table.Column<DateTime>(nullable: false),
                    OperationBlockFK = table.Column<int>(nullable: false),
                    AtiveSchedule = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.ScheduleId);
                });

            migrationBuilder.CreateTable(
                name: "Shift",
                columns: table => new
                {
                    ShiftID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Request = table.Column<string>(nullable: true),
                    Accept = table.Column<string>(nullable: true),
                    ShiftName = table.Column<string>(nullable: true),
                    StartingHour = table.Column<DateTime>(nullable: false),
                    FinishingHour = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shift", x => x.ShiftID);
                });

            migrationBuilder.CreateTable(
                name: "OperationBlock",
                columns: table => new
                {
                    OperationBlockID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BlockName = table.Column<string>(nullable: false),
                    MaxNumOfNurses = table.Column<int>(nullable: false),
                    CurrentNurses = table.Column<int>(nullable: false),
                    ScheduleFK = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationBlock", x => x.OperationBlockID);
                    table.ForeignKey(
                        name: "FK_OperationBlock_Schedule_ScheduleFK",
                        column: x => x.ScheduleFK,
                        principalTable: "Schedule",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shift_Schedule",
                columns: table => new
                {
                    Shift_ScheduleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ShiftDate = table.Column<DateTime>(nullable: false),
                    ScheduleId = table.Column<int>(nullable: false),
                    ScheduleFK = table.Column<int>(nullable: false),
                    ShiftID = table.Column<int>(nullable: false),
                    ShiftFK = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shift_Schedule", x => x.Shift_ScheduleID);
                    table.ForeignKey(
                        name: "FK_Shift_Schedule_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedule",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shift_Schedule_Shift_ShiftID",
                        column: x => x.ShiftID,
                        principalTable: "Shift",
                        principalColumn: "ShiftID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OperationBlock_ScheduleFK",
                table: "OperationBlock",
                column: "ScheduleFK",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shift_Schedule_ScheduleId",
                table: "Shift_Schedule",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Shift_Schedule_ShiftID",
                table: "Shift_Schedule",
                column: "ShiftID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Nurses");

            migrationBuilder.DropTable(
                name: "OperationBlock");

            migrationBuilder.DropTable(
                name: "Shift_Schedule");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropTable(
                name: "Shift");
        }
    }
}
