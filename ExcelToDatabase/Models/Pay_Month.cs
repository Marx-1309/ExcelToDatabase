using System.ComponentModel.DataAnnotations;

namespace ExcelToDatabase.Models
{
    public class Pay_Month
    {
        [Key]
        public int MonthId { get; set; }
        public string MonthName { get; set; }
    }
}
