using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExcelToDatabase.Models
{
    public class Pay_Deductions
    {
            [Key]
            public int EmployeeCode { get; set; }
            public string Surname { get; set; }
            public string FullNames { get; set; }
            public int PayPoint { get; set; }
            public decimal DdSocialSecurity { get; set; }
            public decimal DdPension { get; set; }
            public decimal CcSocialSecurity { get; set; }
            public decimal CcPension { get; set; }
            [NotMapped]
            public Pay_UploadInstance UploadInstance { get; set; }
            public int UploadInstanceId {  get; set; }
    }
}
