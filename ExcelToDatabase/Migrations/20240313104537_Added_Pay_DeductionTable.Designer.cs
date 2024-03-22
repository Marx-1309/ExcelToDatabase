﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ExcelToDatabase.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240313104537_Added_Pay_DeductionTable")]
    partial class Added_Pay_DeductionTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ExcelToDatabase.Models.Pay_Account", b =>
                {
                    b.Property<int>("ACTINDX")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ACTINDX"));

                    b.Property<int>("EarningId")
                        .HasColumnType("int");

                    b.Property<int>("PayPointId")
                        .HasColumnType("int");

                    b.HasKey("ACTINDX");

                    b.ToTable("Pay_Account");
                });

            modelBuilder.Entity("ExcelToDatabase.Models.Pay_Deductions", b =>
                {
                    b.Property<int>("EmployeeCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeCode"));

                    b.Property<decimal>("CcPension")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CcSocialSecurity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("DdPension")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("DdSocialSecurity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("FullNames")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PayPoint")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeCode");

                    b.ToTable("Pay_Deduction");
                });

            modelBuilder.Entity("ExcelToDatabase.Models.Pay_Earning", b =>
                {
                    b.Property<int?>("EarningId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("EarningId"));

                    b.Property<string>("Earning")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EarningDescription")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EarningId");

                    b.ToTable("Pay_Earning");
                });

            modelBuilder.Entity("ExcelToDatabase.Models.Pay_Month", b =>
                {
                    b.Property<int>("MonthId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MonthId"));

                    b.Property<string>("MonthName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MonthId");

                    b.ToTable("Pay_Month");
                });

            modelBuilder.Entity("ExcelToDatabase.Models.Pay_Paypoint", b =>
                {
                    b.Property<long>("PayPointId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("PayPointId"));

                    b.Property<string>("PayPointCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PayPointDescription")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PayPointId");

                    b.ToTable("Pay_Paypoint");
                });

            modelBuilder.Entity("ExcelToDatabase.Models.Pay_UploadInstance", b =>
                {
                    b.Property<int>("UploadInstanceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UploadInstanceID"));

                    b.Property<int>("MonthId")
                        .HasColumnType("int");

                    b.Property<string>("Site")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.Property<DateTime?>("initDate")
                        .HasColumnType("datetime2");

                    b.HasKey("UploadInstanceID");

                    b.HasIndex("MonthId");

                    b.ToTable("Pay_UploadInstance");
                });

            modelBuilder.Entity("ExcelToDatabase.Models.Pay_VIP", b =>
                {
                    b.Property<int>("EmployeeCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeCode"));

                    b.Property<decimal?>("EDACTINGALL")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDACTINGBACKPAY")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDALLOBACKPAY")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDANNUALBONUS")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDBACKPAYBONUS")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDBACKPAYNONTAX")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDBACKPAYSALARY")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDBACKPAYSUBSIDY")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDBACKPAYTAXABLE")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDBPMANNONTAX")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDBPMANTAXABLE")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDCARALLBPN_TAX")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDCARALLBPTAX")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDCARALLNONTAX")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDCARALLTAXABLE")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDCARRUNNINGCOS")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDCASHBBACKPAY")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDCASHBONUS")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDCASHBONUS_2")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDFIXEDOVERTIME")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDFURNITUREALL")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDHOUSALLBACKPAY")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDHOUSING")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDHOUSINGNONTAX")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDHOUSINGTAXABLE")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDLEAVEPAIDOUT")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDOVERTIMBACKPAY")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDOVERTIME1_5")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDOVERTIME2_0")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDREFUNDS")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDREMOTENESSALL")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDREMOTENESSALLO")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDREMOTENESSBP")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDRENTALALLOWANC")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDRUNCOSTBP")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDSALARY")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDSEPARATIONGRAT")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDSUBSIDYNON_TAX")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDSUBSIDYTAXABLE")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDS_TCLAIM")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDS_TCLAIM_2")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDTELEPHONEALL")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDTRANSBACKPAY")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDTRANSPORTALLOW")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDTRAVELALL")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDT_SHIRTREFUND")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDUNPAIDLEAVE")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EDWATER_ELEC")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("FullNames")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PayPoint")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeCode");

                    b.ToTable("Pay_VIP");
                });

            modelBuilder.Entity("ExcelToDatabase.Models.Pay_UploadInstance", b =>
                {
                    b.HasOne("ExcelToDatabase.Models.Pay_Month", "Month")
                        .WithMany()
                        .HasForeignKey("MonthId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Month");
                });
#pragma warning restore 612, 618
        }
    }
}
