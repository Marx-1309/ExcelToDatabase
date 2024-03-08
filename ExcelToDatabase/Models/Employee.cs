using System.ComponentModel.DataAnnotations;

namespace ExcelToDatabase.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeCode { get; set; }
        public string? Surname { get; set; }
        public string? FullNames { get; set; }
        public int? PayPoint { get; set; }
        public decimal? EDSALARY { get; set; }
        public decimal? EDANNUALBONUS { get; set; }
        public decimal? EDSUBSIDYNON_TAX { get; set; }
        public decimal? EDSUBSIDYTAXABLE { get; set; }
        public decimal? EDCARALLTAXABLE { get; set; }
        public decimal? EDCARALLNONTAX { get; set; }
        public decimal? EDOVERTIME1_5 { get; set; }
        public decimal? EDOVERTIME2_0 { get; set; }
        public decimal? EDLEAVEPAIDOUT { get; set; }
        public decimal? EDUNPAIDLEAVE { get; set; }
        public decimal? EDBACKPAYSALARY { get; set; }
        public decimal? EDACTINGALL { get; set; }
        public decimal? EDTELEPHONEALL { get; set; }
        public decimal? EDFURNITUREALL { get; set; }
        public decimal? EDWATER_ELEC { get; set; }
        public decimal? EDTRAVELALL { get; set; }
        public decimal? EDHOUSING { get; set; }
        public decimal? EDREFUNDS { get; set; }
        public decimal? EDCARRUNNINGCOS { get; set; }
        public decimal? EDRENTALALLOWANC { get; set; }
        public decimal? EDTRANSPORTALLOW { get; set; }
        public decimal? EDCASHBONUS { get; set; }
        public decimal? EDS_TCLAIM { get; set; }
        public decimal? EDSEPARATIONGRAT { get; set; }
        public decimal? EDS_TCLAIM_2 { get; set; }
        public decimal? EDREMOTENESSALLO { get; set; }
        public decimal? EDREMOTENESSALL { get; set; }
        public decimal? EDCASHBONUS_2 { get; set; }
        public decimal? EDALLOBACKPAY { get; set; }
        public decimal? EDBACKPAYNONTAX { get; set; }
        public decimal? EDBACKPAYTAXABLE { get; set; }
        public decimal? EDBACKPAYBONUS { get; set; }
        public decimal? EDFIXEDOVERTIME { get; set; }
        public decimal? EDT_SHIRTREFUND { get; set; }
        public decimal? EDOVERTIMBACKPAY { get; set; }
        public decimal? EDHOUSALLBACKPAY { get; set; }
        public decimal? EDTRANSBACKPAY { get; set; }
        public decimal? EDACTINGBACKPAY { get; set; }
        public decimal? EDCASHBBACKPAY { get; set; }
        public decimal? EDREMOTENESSBP { get; set; }
        public decimal? EDBACKPAYSUBSIDY { get; set; }
        public decimal? EDHOUSINGNONTAX { get; set; }
        public decimal? EDHOUSINGTAXABLE { get; set; }
        public decimal? EDBPMANNONTAX { get; set; }
        public decimal? EDBPMANTAXABLE { get; set; }
        public decimal? EDCARALLBPTAX { get; set; }
        public decimal? EDCARALLBPN_TAX { get; set; }
        public decimal? EDRUNCOSTBP { get; set; }
    }

}
