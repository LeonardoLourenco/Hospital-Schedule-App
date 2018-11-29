﻿using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalSchedule.Migrations
{
    public partial class initial : Migration
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
                    BirthDate = table.Column<DateTime>(nullable: false),
                    CC = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Specialties = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    CellPhoneNumber = table.Column<string>(nullable: false),
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
                    OperationBlockFK = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    NurseName = table.Column<string>(nullable: false),
                    OperationBlockName = table.Column<string>(nullable: false),
                    ShiftType = table.Column<string>(nullable: false)
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
                name: "Shift_Schedule",
                columns: table => new
                {
                    Shift_ScheduleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ShiftDate = table.Column<DateTime>(nullable: false),
                    ScheduleFK = table.Column<int>(nullable: false),
                    ShiftFK = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shift_Schedule", x => x.Shift_ScheduleID);
                });

            migrationBuilder.CreateTable(
                name: "Nurses_Schedule",
                columns: table => new
                {
                    Nurse_ScheduleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NurseID = table.Column<int>(nullable: false),
                    ScheduleID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nurses_Schedule", x => x.Nurse_ScheduleID);
                    table.ForeignKey(
                        name: "FK_Nurses_Schedule_Schedule_NurseID",
                        column: x => x.NurseID,
                        principalTable: "Schedule",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Nurses_Schedule_Nurse_ScheduleID",
                        column: x => x.ScheduleID,
                        principalTable: "Nurse",
                        principalColumn: "NurseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OperationsBlock",
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
                    table.PrimaryKey("PK_OperationsBlock", x => x.OperationBlockID);
                    table.ForeignKey(
                        name: "FK_OperationsBlock_Schedule_ScheduleFK",
                        column: x => x.ScheduleFK,
                        principalTable: "Schedule",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Nurses_Schedule_NurseID",
                table: "Nurses_Schedule",
                column: "NurseID");

            migrationBuilder.CreateIndex(
                name: "IX_Nurses_Schedule_ScheduleID",
                table: "Nurses_Schedule",
                column: "ScheduleID");

            migrationBuilder.CreateIndex(
                name: "IX_OperationsBlock_ScheduleFK",
                table: "OperationsBlock",
                column: "ScheduleFK",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Nurses_Schedule");

            migrationBuilder.DropTable(
                name: "OperationsBlock");

            migrationBuilder.DropTable(
                name: "Shift");

            migrationBuilder.DropTable(
                name: "Shift_Schedule");

            migrationBuilder.DropTable(
                name: "Nurse");

            migrationBuilder.DropTable(
                name: "Schedule");
        }
    }
}
