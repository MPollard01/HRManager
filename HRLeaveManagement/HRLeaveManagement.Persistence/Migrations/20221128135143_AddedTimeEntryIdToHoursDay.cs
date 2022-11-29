using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRLeaveManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedTimeEntryIdToHoursDay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HoursDay_TimeEntries_TimeEntryId",
                table: "HoursDay");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "HoursDay");

            migrationBuilder.AlterColumn<int>(
                name: "TimeEntryId",
                table: "HoursDay",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 11, 28, 13, 51, 43, 529, DateTimeKind.Local).AddTicks(9961));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2022, 11, 28, 13, 51, 43, 530, DateTimeKind.Local).AddTicks(7));

            migrationBuilder.AddForeignKey(
                name: "FK_HoursDay_TimeEntries_TimeEntryId",
                table: "HoursDay",
                column: "TimeEntryId",
                principalTable: "TimeEntries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HoursDay_TimeEntries_TimeEntryId",
                table: "HoursDay");

            migrationBuilder.AlterColumn<int>(
                name: "TimeEntryId",
                table: "HoursDay",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "HoursDay",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 11, 28, 10, 27, 57, 311, DateTimeKind.Local).AddTicks(4085));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2022, 11, 28, 10, 27, 57, 311, DateTimeKind.Local).AddTicks(4125));

            migrationBuilder.AddForeignKey(
                name: "FK_HoursDay_TimeEntries_TimeEntryId",
                table: "HoursDay",
                column: "TimeEntryId",
                principalTable: "TimeEntries",
                principalColumn: "Id");
        }
    }
}
