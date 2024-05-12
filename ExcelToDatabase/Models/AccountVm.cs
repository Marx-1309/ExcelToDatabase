namespace ExcelToDatabase.Models
{
    public class AccountVm
    {
        public int AccountId { get; set; }
        public string? AccountName { get; set; }
        public int PayPointId { get; set; }
        public string? PayPointName { get; set; }
        public int EarningId { get; set; }
        public string? EarningName { get; set; }
        public int ACTINDX { get; set; }
        public string? GlAccountName { get; set; }
        public GL00100 GL00100s { get; set; }
        public Pay_Paypoint PayPaypoints { get; set; }
        public Pay_Earning PayEarnings { get; set; }
    }
}
