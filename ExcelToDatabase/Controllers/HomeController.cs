using ExcelDataReader;
using ExcelToDatabase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text;
using System.Linq;
using Azure;

namespace ExcelToDatabase.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult UploadExcel()
        {
            ViewBag.Message = string.Empty;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadExcel(IFormFile file)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                if (file != null && file.Length > 0)
                {
                    // Assuming 'file.FileName' contains the filename
                    var fileName = file.FileName; // Assuming file.FileName is a string
                    var fileExtension = Path.GetExtension(fileName);
                    var xlsExt = ".xls";
                    var xlsxExt = ".xlsx";
                    var uploadCount = 0;

                    if (fileExtension != xlsExt && fileExtension != xlsxExt)
                    {
                        ViewBag.Message = "Please upload file with the correct extension ex. xlsx or xls!";
                        return View();
                    }   


                    var uploadsFolder = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Uploads\\";

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var filePath = Path.Combine(uploadsFolder, file.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            List<Pay_VIP> Pay_VIPsList = new List<Pay_VIP>();
                            
                            do
                            {
                                bool isHeaderSkipped = false;

                                var items = await _context.Pay_VIP.AsTracking().Where(c => c.EmployeeCode > 0).ToListAsync();
                                if (items.Any())
                                {
                                    _context.Pay_VIP.RemoveRange(items);
                                    await _context.SaveChangesAsync();
                                }

                                while (reader.Read())
                                {
                                    if (!isHeaderSkipped)
                                    {
                                        isHeaderSkipped = true;
                                        continue;
                                    }
                                    if (Convert.ToInt32(reader.GetValue(0)) == 0 && uploadCount>0)
                                    {
                                        TempData["success"] = "Success Upload";
                                        

                                        string Hello = "Hi";
                                        return View(Pay_VIPsList);
                                    }

                                    if (_context.Pay_VIP.Any(e => e.EmployeeCode == Convert.ToInt32(reader.GetValue(0))))
                                    {
                                        ViewBag.Message = "A record with the same ID already exist";
                                        return View();
                                    }

                                    //Student s = new Student();
                                    //s.Name = reader.GetValue(1).ToString();
                                    //s.Marks = Convert.ToInt32(reader.GetValue(2).ToString());
                                    Pay_VIP pay_VipItem = new Pay_VIP();

                                    

                                    pay_VipItem.EmployeeCode= Convert.ToInt32(reader.GetValue(0));
                                    pay_VipItem.Surname = reader.GetValue(1)?.ToString();
                                    pay_VipItem.FullNames = reader.GetValue(2)?.ToString();
                                    pay_VipItem.PayPoint = Convert.ToInt32(reader.GetValue(3));
                                    pay_VipItem.EDSALARY = Convert.ToDecimal(reader.GetValue(4));
                                    pay_VipItem.EDANNUALBONUS = Convert.ToDecimal(reader.GetValue(5));
                                    pay_VipItem.EDSUBSIDYNON_TAX = Convert.ToDecimal(reader.GetValue(6));
                                    pay_VipItem.EDSUBSIDYTAXABLE = Convert.ToDecimal(reader.GetValue(7));
                                    pay_VipItem.EDCARALLTAXABLE = Convert.ToDecimal(reader.GetValue(8));
                                    pay_VipItem.EDCARALLNONTAX = Convert.ToDecimal(reader.GetValue(9));
                                    pay_VipItem.EDOVERTIME1_5 = Convert.ToDecimal(reader.GetValue(10));
                                    pay_VipItem.EDOVERTIME2_0 = Convert.ToDecimal(reader.GetValue(11));
                                    pay_VipItem.EDLEAVEPAIDOUT = Convert.ToDecimal(reader.GetValue(12));
                                    pay_VipItem.EDUNPAIDLEAVE = Convert.ToDecimal(reader.GetValue(13));
                                    pay_VipItem.EDBACKPAYSALARY = Convert.ToDecimal(reader.GetValue(14));
                                    pay_VipItem.EDACTINGALL = Convert.ToDecimal(reader.GetValue(15));
                                    pay_VipItem.EDTELEPHONEALL = Convert.ToDecimal(reader.GetValue(16));
                                    pay_VipItem.EDFURNITUREALL = Convert.ToDecimal(reader.GetValue(17));
                                    pay_VipItem.EDWATER_ELEC = Convert.ToDecimal(reader.GetValue(18));
                                    pay_VipItem.EDTRAVELALL = Convert.ToDecimal(reader.GetValue(19));
                                    pay_VipItem.EDHOUSING = Convert.ToDecimal(reader.GetValue(20));
                                    pay_VipItem.EDREFUNDS = Convert.ToDecimal(reader.GetValue(21));
                                    pay_VipItem.EDCARRUNNINGCOS = Convert.ToDecimal(reader.GetValue(22));
                                    pay_VipItem.EDRENTALALLOWANC = Convert.ToDecimal(reader.GetValue(23));
                                    pay_VipItem.EDTRANSPORTALLOW = Convert.ToDecimal(reader.GetValue(24));
                                    pay_VipItem.EDCASHBONUS = Convert.ToDecimal(reader.GetValue(25));
                                    pay_VipItem.EDS_TCLAIM = Convert.ToDecimal(reader.GetValue(26));
                                    pay_VipItem.EDSEPARATIONGRAT = Convert.ToDecimal(reader.GetValue(27));
                                    pay_VipItem.EDREMOTENESSALLO = Convert.ToDecimal(reader.GetValue(28));
                                    pay_VipItem.EDREMOTENESSALL = Convert.ToDecimal(reader.GetValue(29));
                                    pay_VipItem.EDCASHBONUS = Convert.ToDecimal(reader.GetValue(30));
                                    pay_VipItem.EDALLOBACKPAY = Convert.ToDecimal(reader.GetValue(31));
                                    pay_VipItem.EDBACKPAYNONTAX = Convert.ToDecimal(reader.GetValue(32));
                                    pay_VipItem.EDBACKPAYTAXABLE = Convert.ToDecimal(reader.GetValue(33));
                                    pay_VipItem.EDBACKPAYBONUS = Convert.ToDecimal(reader.GetValue(34));
                                    pay_VipItem.EDFIXEDOVERTIME = Convert.ToDecimal(reader.GetValue(35));
                                    pay_VipItem.EDT_SHIRTREFUND = Convert.ToDecimal(reader.GetValue(36));
                                    pay_VipItem.EDOVERTIMBACKPAY = Convert.ToDecimal(reader.GetValue(37));
                                    pay_VipItem.EDHOUSALLBACKPAY = Convert.ToDecimal(reader.GetValue(38));
                                    pay_VipItem.EDTRANSBACKPAY = Convert.ToDecimal(reader.GetValue(39));
                                    pay_VipItem.EDACTINGBACKPAY = Convert.ToDecimal(reader.GetValue(40));
                                    pay_VipItem.EDCASHBBACKPAY = Convert.ToDecimal(reader.GetValue(41));
                                    pay_VipItem.EDREMOTENESSBP = Convert.ToDecimal(reader.GetValue(42));
                                    pay_VipItem.EDBACKPAYSUBSIDY = Convert.ToDecimal(reader.GetValue(43));
                                    pay_VipItem.EDHOUSINGNONTAX = Convert.ToDecimal(reader.GetValue(44));
                                    pay_VipItem.EDHOUSINGTAXABLE = Convert.ToDecimal(reader.GetValue(45));
                                    pay_VipItem.EDBPMANNONTAX = Convert.ToDecimal(reader.GetValue(46));
                                    pay_VipItem.EDBPMANTAXABLE = Convert.ToDecimal(reader.GetValue(47));
                                    pay_VipItem.EDCARALLBPTAX = Convert.ToDecimal(reader.GetValue(48));
                                    pay_VipItem.EDCARALLBPN_TAX = Convert.ToDecimal(reader.GetValue(49));
                                    pay_VipItem.EDRUNCOSTBP = Convert.ToDecimal(reader.GetValue(50));



                                    _context.Add(pay_VipItem);
                                    await _context.SaveChangesAsync();
                                    Pay_VIPsList.Add(pay_VipItem);
                                    uploadCount++;
                                }
                            } while (reader.NextResult());

                            ViewBag.Message = "success";
                        }
                    }
                }

                int i = 1;
                if(i ==2)
                {
                    
                };
                return View();
            }          
            catch(Exception ex)
            {
                ViewBag.Message = "An error encountered! "+ex.StackTrace;
                return View();
            }

        }
    }
}