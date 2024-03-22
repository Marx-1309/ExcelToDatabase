using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExcelToDatabase.Models
{
    public class Pay_Paypoint
    {
        [Key]
        public System.Int64 PayPointId { get; set; }
        public string? PayPointCode { get; set; }
        public string? PayPointDescription { get; set; }
        //This is the ID for the parent table[
        //[NotMapped]
        //public int? AccountId { get; set; }
    }
}
