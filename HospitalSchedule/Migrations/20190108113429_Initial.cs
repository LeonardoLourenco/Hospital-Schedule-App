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
                name: "OperationBlock",
                columns: table => new
                {
                    OperationBlockId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BlockName = table.Column<string>(nullable: false)
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
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    WeeklyHours = table.Column<int>(nullable: false),
                    NurseAge = table.Column<int>(nullable: false),
                    ChildAge = table.Column<int>(nullable: false),
                    InBetweenShiftTime = table.Column<string>(nullable: false)
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
                    ShiftName = table.Column<string>(nullable: false),
                    StartingHour = table.Column<string>(nullable: false),
                    Duration = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shift", x => x.ShiftId);
                });

            migrationBuilder.CreateTable(
                name: "Specialty",
                columns: table => new
                {
                    SpecialtyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialty", x => x.SpecialtyId);
                });

            migrationBuilder.CreateTable(
                name: "OperationBlock_Shifts",
                columns: table => new
                {
                    OperationBlock_ShiftsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ShiftId = table.Column<int>(nullable: false),
                    OperationBlockId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationBlock_Shifts", x => x.OperationBlock_ShiftsId);
                    table.ForeignKey(
                        name: "FK_OperationBlock_Shifts_OperationBlock_OperationBlockId",
                        column: x => x.OperationBlockId,
                        principalTable: "OperationBlock",
                        principalColumn: "OperationBlockId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OperationBlock_Shifts_Shift_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shift",
                        principalColumn: "ShiftId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Nurse",
                columns: table => new
                {
                    NurseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    CellPhoneNumber = table.Column<string>(nullable: false),
                    IDCard = table.Column<string>(nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    YoungestChildBirthDate = table.Column<DateTime>(nullable: true),
                    SpecialtyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nurse", x => x.NurseId);
                    table.ForeignKey(
                        name: "FK_Nurse_Specialty_SpecialtyId",
                        column: x => x.SpecialtyId,
                        principalTable: "Specialty",
                        principalColumn: "SpecialtyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    NurseId = table.Column<int>(nullable: false),
                    OperationBlock_ShiftsId = table.Column<int>(nullable: false)
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
                    table.ForeignKey(
                        name: "FK_Schedule_OperationBlock_Shifts_OperationBlock_ShiftsId",
                        column: x => x.OperationBlock_ShiftsId,
                        principalTable: "OperationBlock_Shifts",
                        principalColumn: "OperationBlock_ShiftsId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Schedule_Exchange1",
                columns: table => new
                {
                    Schedule_Exchange1Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ScheduleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule_Exchange1", x => x.Schedule_Exchange1Id);
                    table.ForeignKey(
                        name: "FK_Schedule_Exchange1_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedule",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Schedule_Exchange2",
                columns: table => new
                {
                    Schedule_Exchange2Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ScheduleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule_Exchange2", x => x.Schedule_Exchange2Id);
                    table.ForeignKey(
                        name: "FK_Schedule_Exchange2_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedule",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Exchange_Request",
                columns: table => new
                {
                    Exchange_RequestId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Schedule_Exchange1Id = table.Column<int>(nullable: false),
                    Schedule_Exchange2Id = table.Column<int>(nullable: false),
                    RequestState = table.Column<string>(nullable: false),
                    Date_RequestState = table.Column<DateTime>(nullable: false),
                    Date_Exchange_Request = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exchange_Request", x => x.Exchange_RequestId);
                    table.ForeignKey(
                        name: "FK_Exchange_Request_Schedule_Exchange1_Schedule_Exchange1Id",
                        column: x => x.Schedule_Exchange1Id,
                        principalTable: "Schedule_Exchange1",
                        principalColumn: "Schedule_Exchange1Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Exchange_Request_Schedule_Exchange2_Schedule_Exchange2Id",
                        column: x => x.Schedule_Exchange2Id,
                        principalTable: "Schedule_Exchange2",
                        principalColumn: "Schedule_Exchange2Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exchange_Request_Schedule_Exchange1Id",
                table: "Exchange_Request",
                column: "Schedule_Exchange1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Exchange_Request_Schedule_Exchange2Id",
                table: "Exchange_Request",
                column: "Schedule_Exchange2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Nurse_SpecialtyId",
                table: "Nurse",
                column: "SpecialtyId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationBlock_Shifts_OperationBlockId",
                table: "OperationBlock_Shifts",
                column: "OperationBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationBlock_Shifts_ShiftId",
                table: "OperationBlock_Shifts",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_NurseId",
                table: "Schedule",
                column: "NurseId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_OperationBlock_ShiftsId",
                table: "Schedule",
                column: "OperationBlock_ShiftsId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_Exchange1_ScheduleId",
                table: "Schedule_Exchange1",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_Exchange2_ScheduleId",
                table: "Schedule_Exchange2",
                column: "ScheduleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exchange_Request");

            migrationBuilder.DropTable(
                name: "Rules");

            migrationBuilder.DropTable(
                name: "Schedule_Exchange1");

            migrationBuilder.DropTable(
                name: "Schedule_Exchange2");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropTable(
                name: "Nurse");

            migrationBuilder.DropTable(
                name: "OperationBlock_Shifts");

            migrationBuilder.DropTable(
                name: "Specialty");

            migrationBuilder.DropTable(
                name: "OperationBlock");

            migrationBuilder.DropTable(
                name: "Shift");
        }
    }
}
