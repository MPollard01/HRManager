using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRLeaveManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedTemplateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TimeTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hours1 = table.Column<int>(type: "int", nullable: false),
                    Hours2 = table.Column<int>(type: "int", nullable: false),
                    Hours3 = table.Column<int>(type: "int", nullable: false),
                    Hours4 = table.Column<int>(type: "int", nullable: false),
                    Hours5 = table.Column<int>(type: "int", nullable: false),
                    Hours6 = table.Column<int>(type: "int", nullable: false),
                    Hours7 = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeTemplates", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 12, 2, 10, 13, 13, 380, DateTimeKind.Local).AddTicks(6986));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2022, 12, 2, 10, 13, 13, 380, DateTimeKind.Local).AddTicks(7067));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeTemplates");

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
        }
    }
}
