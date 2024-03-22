using ExcelToDatabase.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Pay_VIP> Pay_VIP { get; set; } = default!;
    public DbSet<GL00100> GL00100 { get; set; } = default!;
    public DbSet<Pay_Paypoint> Pay_Paypoint { get; set; } = default!;
    public DbSet<Pay_Account> Pay_Accounts { get; set; } = default!;
    public DbSet<Pay_Earning> Pay_Earning { get; set; } = default!; 
    public DbSet<Pay_Month> Pay_Month { get; set; } = default!;
    public DbSet<ExcelToDatabase.Models.Pay_UploadInstance> Pay_UploadInstance { get; set; } = default!;
    public DbSet<ExcelToDatabase.Models.Pay_Deductions> Pay_Deduction { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Ignore the Pay_Month entity during migrations
        //modelBuilder.Ignore<Pay_Account>();
        //modelBuilder.Ignore<Pay_Paypoint>();
        //modelBuilder.Ignore<Pay_Earning>();
        //modelBuilder.Ignore<Pay_VIP>();
        //modelBuilder.Ignore<Pay_Month>();
        //modelBuilder.Ignore<Pay_UploadInstance>();
    }
}
