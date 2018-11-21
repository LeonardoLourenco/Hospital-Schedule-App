using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalSchedule.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nurse",
                columns: table => new
                {
                    NurseID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Specialties = table.Column<string>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    CellPhoneNumber = table.Column<string>(nullable: false),
                    CCBI = table.Column<string>(nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    YoungestChildBirthDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nurse", x => x.NurseID);
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    NurseName = table.Column<string>(nullable: false),
                    OperationBlockName = table.Column<string>(nullable: false),
                    ShiftType = table.Column<string>(nullable: false),
                    OperationBlockFK = table.Column<int>(nullable: false)
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
                    ShiftName = table.Column<string>(nullable: true),
                    StartingHour = table.Column<DateTime>(nullable: false),
                    FinishingHour = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shift", x => x.ShiftID);
                });

            migrationBuilder.CreateTable(
                name: "Nurse_Schedule",
                columns: table => new
                {
                    Nurse_ScheduleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ScheduleId = table.Column<int>(nullable: false),
                    ScheduleFK = table.Column<int>(nullable: false),
                    NurseID = table.Column<int>(nullable: false),
                    NurseFK = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nurse_Schedule", x => x.Nurse_ScheduleID);
                    table.ForeignKey(
                        name: "FK_Nurse_Schedule_Nurse_NurseID",
                        column: x => x.NurseID,
                        principalTable: "Nurse",
                        principalColumn: "NurseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Nurse_Schedule_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedule",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OperationBlock",
                columns: table => new
                {
                    OperationBlockID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BlockName = table.Column<string>(nullable: false),
                    TypeOfShift = table.Column<string>(nullable: false),
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
                name: "IX_Nurse_Schedule_NurseID",
                table: "Nurse_Schedule",
                column: "NurseID");

            migrationBuilder.CreateIndex(
                name: "IX_Nurse_Schedule_ScheduleId",
                table: "Nurse_Schedule",
                column: "ScheduleId");

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
                name: "Nurse_Schedule");

            migrationBuilder.DropTable(
                name: "OperationBlock");

            migrationBuilder.DropTable(
                name: "Shift_Schedule");

            migrationBuilder.DropTable(
                name: "Nurse");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropTable(
                name: "Shift");
        }
    }
}
