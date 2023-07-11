using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRLeaveManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedNullableEmployeeDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "WorkEmail",
                table: "EmployeeDetails",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Town",
                table: "EmployeeDetails",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "TaxCode",
                table: "EmployeeDetails",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "PostCode",
                table: "EmployeeDetails",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "PersonalEmail",
                table: "EmployeeDetails",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "NINumber",
                table: "EmployeeDetails",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "BankName",
                table: "EmployeeDetails",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "BankAddress",
                table: "EmployeeDetails",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Address2",
                table: "EmployeeDetails",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Address1",
                table: "EmployeeDetails",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "AccountName",
                table: "EmployeeDetails",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "EmployeeDetails",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 6, 20, 16, 3, 48, 253, DateTimeKind.Utc).AddTicks(8877), new DateTime(2023, 6, 20, 16, 3, 48, 253, DateTimeKind.Utc).AddTicks(8877) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "DateCreated", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 6, 20, 16, 3, 48, 253, DateTimeKind.Utc).AddTicks(9319), new DateTime(2023, 6, 20, 16, 3, 48, 253, DateTimeKind.Utc).AddTicks(9318), new DateTime(2023, 6, 20, 16, 3, 48, 253, DateTimeKind.Utc).AddTicks(9319) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "DateCreated", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 6, 20, 16, 3, 48, 253, DateTimeKind.Utc).AddTicks(9321), new DateTime(2023, 6, 20, 16, 3, 48, 253, DateTimeKind.Utc).AddTicks(9321), new DateTime(2023, 6, 20, 16, 3, 48, 253, DateTimeKind.Utc).AddTicks(9322) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "WorkEmail",
                table: "EmployeeDetails",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Town",
                table: "EmployeeDetails",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TaxCode",
                table: "EmployeeDetails",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PostCode",
                table: "EmployeeDetails",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PersonalEmail",
                table: "EmployeeDetails",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NINumber",
                table: "EmployeeDetails",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BankName",
                table: "EmployeeDetails",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BankAddress",
                table: "EmployeeDetails",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address2",
                table: "EmployeeDetails",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address1",
                table: "EmployeeDetails",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AccountName",
                table: "EmployeeDetails",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "EmployeeDetails",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 5, 24, 16, 4, 39, 83, DateTimeKind.Utc).AddTicks(4199), new DateTime(2023, 5, 24, 16, 4, 39, 83, DateTimeKind.Utc).AddTicks(4200) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "DateCreated", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 5, 24, 16, 4, 39, 83, DateTimeKind.Utc).AddTicks(4586), new DateTime(2023, 5, 24, 16, 4, 39, 83, DateTimeKind.Utc).AddTicks(4586), new DateTime(2023, 5, 24, 16, 4, 39, 83, DateTimeKind.Utc).AddTicks(4587) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "DateCreated", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 5, 24, 16, 4, 39, 83, DateTimeKind.Utc).AddTicks(4588), new DateTime(2023, 5, 24, 16, 4, 39, 83, DateTimeKind.Utc).AddTicks(4588), new DateTime(2023, 5, 24, 16, 4, 39, 83, DateTimeKind.Utc).AddTicks(4589) });
        }
    }
}
