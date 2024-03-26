using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ExcelToDatabase.Models
{
    public class Pay_UploadInstance
    {
        [Key]
        public int UploadInstanceID { get; set; }
        public Pay_Month Month { get; set;}
        public int MonthId { get; set; }
        public int Year { get; set;}
        public DateTime? DateCreated { get; set; } 
        public String Site { get; set; }
    }
}
