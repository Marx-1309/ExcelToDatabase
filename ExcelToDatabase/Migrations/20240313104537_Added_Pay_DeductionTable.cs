using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExcelToDatabase.Migrations
{
    /// <inheritdoc />
    public partial class Added_Pay_DeductionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "initDate",
                table: "Pay_UploadInstance",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "Pay_Deduction",
                columns: table => new
                {
                    EmployeeCode = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullNames = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PayPoint = table.Column<int>(type: "int", nullable: false),
                    DdSocialSecurity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DdPension = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CcSocialSecurity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CcPension = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pay_Deduction", x => x.EmployeeCode);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pay_UploadInstance_MonthId",
                table: "Pay_UploadInstance",
                column: "MonthId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pay_UploadInstance_Pay_Month_MonthId",
                table: "Pay_UploadInstance",
                column: "MonthId",
                principalTable: "Pay_Month",
                principalColumn: "MonthId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pay_UploadInstance_Pay_Month_MonthId",
                table: "Pay_UploadInstance");

            migrationBuilder.DropTable(
                name: "Pay_Deduction");

            migrationBuilder.DropIndex(
                name: "IX_Pay_UploadInstance_MonthId",
                table: "Pay_UploadInstance");

            migrationBuilder.AlterColumn<DateTime>(
                name: "initDate",
                table: "Pay_UploadInstance",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
