using System.ComponentModel.DataAnnotations;

namespace ExcelToDatabase.Models
{
    public class Pay_Account
    {
        [Key]
        public long? AccountId {  get; set; }
        public int? ACTINDX { get; set; }
        public long? PayPointId { get; set; }
        public long? EarningId { get; set; }
        public DateTime? DateCreated { get; set; }
        //List<Pay_Paypoint>? Pay_Paypoints { get; set; }
        //List<Pay_Earning>? Pay_Earnings { get; set; }
    }
}
