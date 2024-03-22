namespace ExcelToDatabase.Models
{
    public class AccountVm
    {
        public List<GL00100> GL00100s { get; set; }
        public List<Pay_Paypoint> PayPaypoints { get; set; }
        public List<Pay_Earning> PayEarnings { get; set; }
    }
}
