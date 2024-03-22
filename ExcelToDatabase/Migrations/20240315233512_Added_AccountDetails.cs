using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExcelToDatabase.Migrations
{
    /// <inheritdoc />
    public partial class Added_AccountDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.CreateTable(
                name: "Pay_Account",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ACTINDX = table.Column<int>(type: "int", nullable: true),
                    PayPointId = table.Column<int>(type: "int", nullable: true),
                    EarningId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pay_Account", x => x.AccountId);
                });


        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GL00100");

            migrationBuilder.DropTable(
                name: "Pay_Account");

            migrationBuilder.DropTable(
                name: "Pay_Earning");

            migrationBuilder.DropTable(
                name: "Pay_Paypoint");

            migrationBuilder.DropTable(
                name: "Pay_UploadInstance");

            migrationBuilder.DropTable(
                name: "Pay_VIP");

            migrationBuilder.DropTable(
                name: "Pay_Month");

            migrationBuilder.DropColumn(
                name: "UploadInstanceId",
                table: "Pay_Deduction");
        }
    }
}
