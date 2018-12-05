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
                    Duration = table.Column<int>(nullable: false)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rules");

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
