using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExcelToDatabase.Models
{
    public class Pay_Earning
    {
        [Key]
        public System.Int64 EarningId { get; set; }
        public string Earning { get; set; }
        //public string EarningDescription { get; set; } = "No Desc";
        //This is the ID for the parent table[
        //[NotMapped]
        //public int? AccountId { get; set; }
    }
}
