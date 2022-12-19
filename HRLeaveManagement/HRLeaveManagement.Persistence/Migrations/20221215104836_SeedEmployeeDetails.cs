using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRLeaveManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedEmployeeDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "WorkPhoneNumber",
                table: "EmployeeDetails",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "WorkMobileNumber",
                table: "EmployeeDetails",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "MobileNumber",
                table: "EmployeeDetails",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "HomePhoneNumber",
                table: "EmployeeDetails",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "EmployeeDetails",
                columns: new[] { "Id", "AccountName", "AccountNumber", "Address1", "Address2", "BankAddress", "BankName", "CreatedBy", "CreatedDate", "DOB", "EmployeeId", "FirstName", "HomePhoneNumber", "LastName", "MobileNumber", "ModifiedBy", "ModifiedDate", "NINumber", "PersonalEmail", "PostCode", "SortCode", "TaxCode", "Town", "WorkEmail", "WorkMobileNumber", "WorkPhoneNumber" },
                values: new object[] { 1, "S U User", 4789023, "123 User Street", "Dee Way", "123 Bank Road", "User Bank", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1998, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "9e224968-33e4-4652-b7b7-8574d048cdb9", "System", 1493902383L, "User", 7948392731L, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "JL372998A", "user@mail.com", "DK99USR", 93123, "T89AL", "User Town", "user@localhost.com", 4898309423L, 89402939481L });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 12, 15, 10, 48, 36, 169, DateTimeKind.Local).AddTicks(4583));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2022, 12, 15, 10, 48, 36, 169, DateTimeKind.Local).AddTicks(4630));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmployeeDetails",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "WorkPhoneNumber",
                table: "EmployeeDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "WorkMobileNumber",
                table: "EmployeeDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "MobileNumber",
                table: "EmployeeDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "HomePhoneNumber",
                table: "EmployeeDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 12, 13, 17, 43, 50, 171, DateTimeKind.Local).AddTicks(3578));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2022, 12, 13, 17, 43, 50, 171, DateTimeKind.Local).AddTicks(3619));
        }
    }
}
