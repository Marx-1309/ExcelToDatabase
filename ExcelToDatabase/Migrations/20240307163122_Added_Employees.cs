using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExcelToDatabase.Migrations
{
    /// <inheritdoc />
    public partial class Added_Employees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeCode = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullNames = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PayPoint = table.Column<int>(type: "int", nullable: false),
                    EDSALARY = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDANNUALBONUS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDSUBSIDYNON_TAX = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDSUBSIDYTAXABLE = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDCARALLTAXABLE = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDCARALLNONTAX = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDOVERTIME1_5 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDOVERTIME2_0 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDLEAVEPAIDOUT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDUNPAIDLEAVE = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDBACKPAYSALARY = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDACTINGALL = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDTELEPHONEALL = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDFURNITUREALL = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDWATER_ELEC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDTRAVELALL = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDHOUSING = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDREFUNDS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDCARRUNNINGCOS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDRENTALALLOWANC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDTRANSPORTALLOW = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDCASHBONUS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDS_TCLAIM = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDSEPARATIONGRAT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDS_TCLAIM_2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDREMOTENESSALLO = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDREMOTENESSALL = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDCASHBONUS_2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDALLOBACKPAY = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDBACKPAYNONTAX = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDBACKPAYTAXABLE = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDBACKPAYBONUS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDFIXEDOVERTIME = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDT_SHIRTREFUND = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDOVERTIMBACKPAY = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDHOUSALLBACKPAY = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDTRANSBACKPAY = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDACTINGBACKPAY = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDCASHBBACKPAY = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDREMOTENESSBP = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDBACKPAYSUBSIDY = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDHOUSINGNONTAX = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDHOUSINGTAXABLE = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDBPMANNONTAX = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDBPMANTAXABLE = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDCARALLBPTAX = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDCARALLBPN_TAX = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EDRUNCOSTBP = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeCode);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Marks = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });
        }
    }
}
