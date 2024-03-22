using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExcelToDatabase.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pay_Account",
                columns: table => new
                {
                    ACTINDX = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PayPointId = table.Column<int>(type: "int", nullable: false),
                    EarningId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pay_Account", x => x.ACTINDX);
                });

            migrationBuilder.CreateTable(
                name: "Pay_Earning",
                columns: table => new
                {
                    EarningId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Earning = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EarningDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pay_Earning", x => x.EarningId);
                });

            migrationBuilder.CreateTable(
                name: "Pay_InstancePayroll",
                columns: table => new
                {
                    InstancePayrollId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MonthId = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Site = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pay_InstancePayroll", x => x.InstancePayrollId);
                });

            migrationBuilder.CreateTable(
                name: "Pay_Month",
                columns: table => new
                {
                    MonthId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MonthName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pay_Month", x => x.MonthId);
                });

            migrationBuilder.CreateTable(
                name: "Pay_Paypoint",
                columns: table => new
                {
                    PayPointId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PayPointCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayPointDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pay_Paypoint", x => x.PayPointId);
                });

            migrationBuilder.CreateTable(
                name: "Pay_VIP",
                columns: table => new
                {
                    EmployeeCode = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullNames = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayPoint = table.Column<int>(type: "int", nullable: true),
                    EDSALARY = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDANNUALBONUS = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDSUBSIDYNON_TAX = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDSUBSIDYTAXABLE = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDCARALLTAXABLE = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDCARALLNONTAX = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDOVERTIME1_5 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDOVERTIME2_0 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDLEAVEPAIDOUT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDUNPAIDLEAVE = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDBACKPAYSALARY = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDACTINGALL = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDTELEPHONEALL = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDFURNITUREALL = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDWATER_ELEC = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDTRAVELALL = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDHOUSING = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDREFUNDS = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDCARRUNNINGCOS = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDRENTALALLOWANC = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDTRANSPORTALLOW = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDCASHBONUS = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDS_TCLAIM = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDSEPARATIONGRAT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDS_TCLAIM_2 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDREMOTENESSALLO = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDREMOTENESSALL = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDCASHBONUS_2 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDALLOBACKPAY = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDBACKPAYNONTAX = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDBACKPAYTAXABLE = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDBACKPAYBONUS = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDFIXEDOVERTIME = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDT_SHIRTREFUND = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDOVERTIMBACKPAY = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDHOUSALLBACKPAY = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDTRANSBACKPAY = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDACTINGBACKPAY = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDCASHBBACKPAY = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDREMOTENESSBP = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDBACKPAYSUBSIDY = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDHOUSINGNONTAX = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDHOUSINGTAXABLE = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDBPMANNONTAX = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDBPMANTAXABLE = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDCARALLBPTAX = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDCARALLBPN_TAX = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDRUNCOSTBP = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pay_VIP", x => x.EmployeeCode);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pay_Account");

            migrationBuilder.DropTable(
                name: "Pay_Earning");

            migrationBuilder.DropTable(
                name: "Pay_InstancePayroll");

            migrationBuilder.DropTable(
                name: "Pay_Month");

            migrationBuilder.DropTable(
                name: "Pay_Paypoint");

            migrationBuilder.DropTable(
                name: "Pay_VIP");
        }
    }
}
