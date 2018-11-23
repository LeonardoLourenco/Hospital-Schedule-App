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
                    NurseId = table.Column<int>(nullable: false)
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
                    table.PrimaryKey("PK_Nurse", x => x.NurseId);
                });

            migrationBuilder.CreateTable(
                name: "OperationBlock",
                columns: table => new
                {
                    OperationBlockId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BlockName = table.Column<string>(nullable: false),
                    TypeOfShift = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationBlock", x => x.OperationBlockId);
                });

            migrationBuilder.CreateTable(
                name: "Rules",
                columns: table => new
                {
                    RulesId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rules", x => x.RulesId);
                });

            migrationBuilder.CreateTable(
                name: "Shift",
                columns: table => new
                {
                    ShiftId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ShiftName = table.Column<string>(nullable: true),
                    StartingHour = table.Column<DateTime>(nullable: false),
                    FinishingHour = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shift", x => x.ShiftId);
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    NurseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.ScheduleId);
                    table.ForeignKey(
                        name: "FK_Schedule_Nurse_NurseId",
                        column: x => x.NurseId,
                        principalTable: "Nurse",
                        principalColumn: "NurseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Shift_Schedule_OperationBlock",
                columns: table => new
                {
                    Shift_Schedule_OperationBlockId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ShiftDate = table.Column<DateTime>(nullable: false),
                    ScheduleId = table.Column<int>(nullable: false),
                    ShiftId = table.Column<int>(nullable: false),
                    OperationBlockId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shift_Schedule_OperationBlock", x => x.Shift_Schedule_OperationBlockId);
                    table.ForeignKey(
                        name: "FK_Shift_Schedule_OperationBlock_OperationBlock_OperationBlockId",
                        column: x => x.OperationBlockId,
                        principalTable: "OperationBlock",
                        principalColumn: "OperationBlockId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shift_Schedule_OperationBlock_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedule",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shift_Schedule_OperationBlock_Shift_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shift",
                        principalColumn: "ShiftId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_NurseId",
                table: "Schedule",
                column: "NurseId");

            migrationBuilder.CreateIndex(
                name: "IX_Shift_Schedule_OperationBlock_OperationBlockId",
                table: "Shift_Schedule_OperationBlock",
                column: "OperationBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_Shift_Schedule_OperationBlock_ScheduleId",
                table: "Shift_Schedule_OperationBlock",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Shift_Schedule_OperationBlock_ShiftId",
                table: "Shift_Schedule_OperationBlock",
                column: "ShiftId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rules");

            migrationBuilder.DropTable(
                name: "Shift_Schedule_OperationBlock");

            migrationBuilder.DropTable(
                name: "OperationBlock");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropTable(
                name: "Shift");

            migrationBuilder.DropTable(
                name: "Nurse");
        }
    }
}
