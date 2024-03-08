using ExcelDataReader;
using ExcelToDatabase.Data;
using ExcelToDatabase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text;
using System.Linq;

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
                            do
                            {
                                bool isHeaderSkipped = false;

                                var employees = await _context.Employees.AsTracking().Where(c => c.EmployeeCode > 0).ToListAsync();
                                if (employees.Any())
                                {
                                    _context.Employees.RemoveRange(employees);
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
                                        ViewBag.Message = "uploadSuccess";
                                        return View();
                                    }

                                    //Student s = new Student();
                                    //s.Name = reader.GetValue(1).ToString();
                                    //s.Marks = Convert.ToInt32(reader.GetValue(2).ToString());
                                    Employee employee = new Employee();

                                    if(_context.Employees.Any(e=>e.EmployeeCode == Convert.ToInt32(reader.GetValue(0))))
                                    {
                                        ViewBag.Message = "A record with the same ID already exist";
                                        return View();
                                    }

                                    

                                    employee.EmployeeCode = Convert.ToInt32(reader.GetValue(0));
                                    employee.Surname = reader.GetValue(1)?.ToString();
                                    employee.FullNames = reader.GetValue(2)?.ToString();
                                    employee.PayPoint = Convert.ToInt32(reader.GetValue(3));
                                    employee.EDSALARY = Convert.ToDecimal(reader.GetValue(4));
                                    employee.EDANNUALBONUS = Convert.ToDecimal(reader.GetValue(5));
                                    employee.EDSUBSIDYNON_TAX = Convert.ToDecimal(reader.GetValue(6));
                                    employee.EDSUBSIDYTAXABLE = Convert.ToDecimal(reader.GetValue(7));
                                    employee.EDCARALLTAXABLE = Convert.ToDecimal(reader.GetValue(8));
                                    employee.EDCARALLNONTAX = Convert.ToDecimal(reader.GetValue(9));
                                    employee.EDOVERTIME1_5 = Convert.ToDecimal(reader.GetValue(10));
                                    employee.EDOVERTIME2_0 = Convert.ToDecimal(reader.GetValue(11));
                                    employee.EDLEAVEPAIDOUT = Convert.ToDecimal(reader.GetValue(12));
                                    employee.EDUNPAIDLEAVE = Convert.ToDecimal(reader.GetValue(13));
                                    employee.EDBACKPAYSALARY = Convert.ToDecimal(reader.GetValue(14));
                                    employee.EDACTINGALL = Convert.ToDecimal(reader.GetValue(15));
                                    employee.EDTELEPHONEALL = Convert.ToDecimal(reader.GetValue(16));
                                    employee.EDFURNITUREALL = Convert.ToDecimal(reader.GetValue(17));
                                    employee.EDWATER_ELEC = Convert.ToDecimal(reader.GetValue(18));
                                    employee.EDTRAVELALL = Convert.ToDecimal(reader.GetValue(19));
                                    employee.EDHOUSING = Convert.ToDecimal(reader.GetValue(20));
                                    employee.EDREFUNDS = Convert.ToDecimal(reader.GetValue(21));
                                    employee.EDCARRUNNINGCOS = Convert.ToDecimal(reader.GetValue(22));
                                    employee.EDRENTALALLOWANC = Convert.ToDecimal(reader.GetValue(23));
                                    employee.EDTRANSPORTALLOW = Convert.ToDecimal(reader.GetValue(24));
                                    employee.EDCASHBONUS = Convert.ToDecimal(reader.GetValue(25));
                                    employee.EDS_TCLAIM = Convert.ToDecimal(reader.GetValue(26));
                                    employee.EDSEPARATIONGRAT = Convert.ToDecimal(reader.GetValue(27));
                                    employee.EDREMOTENESSALLO = Convert.ToDecimal(reader.GetValue(28));
                                    employee.EDREMOTENESSALL = Convert.ToDecimal(reader.GetValue(29));
                                    employee.EDCASHBONUS = Convert.ToDecimal(reader.GetValue(30));
                                    employee.EDALLOBACKPAY = Convert.ToDecimal(reader.GetValue(31));
                                    employee.EDBACKPAYNONTAX = Convert.ToDecimal(reader.GetValue(32));
                                    employee.EDBACKPAYTAXABLE = Convert.ToDecimal(reader.GetValue(33));
                                    employee.EDBACKPAYBONUS = Convert.ToDecimal(reader.GetValue(34));
                                    employee.EDFIXEDOVERTIME = Convert.ToDecimal(reader.GetValue(35));
                                    employee.EDT_SHIRTREFUND = Convert.ToDecimal(reader.GetValue(36));
                                    employee.EDOVERTIMBACKPAY = Convert.ToDecimal(reader.GetValue(37));
                                    employee.EDHOUSALLBACKPAY = Convert.ToDecimal(reader.GetValue(38));
                                    employee.EDTRANSBACKPAY = Convert.ToDecimal(reader.GetValue(39));
                                    employee.EDACTINGBACKPAY = Convert.ToDecimal(reader.GetValue(40));
                                    employee.EDCASHBBACKPAY = Convert.ToDecimal(reader.GetValue(41));
                                    employee.EDREMOTENESSBP = Convert.ToDecimal(reader.GetValue(42));
                                    employee.EDBACKPAYSUBSIDY = Convert.ToDecimal(reader.GetValue(43));
                                    employee.EDHOUSINGNONTAX = Convert.ToDecimal(reader.GetValue(44));
                                    employee.EDHOUSINGTAXABLE = Convert.ToDecimal(reader.GetValue(45));
                                    employee.EDBPMANNONTAX = Convert.ToDecimal(reader.GetValue(46));
                                    employee.EDBPMANTAXABLE = Convert.ToDecimal(reader.GetValue(47));
                                    employee.EDCARALLBPTAX = Convert.ToDecimal(reader.GetValue(48));
                                    employee.EDCARALLBPN_TAX = Convert.ToDecimal(reader.GetValue(49));
                                    employee.EDRUNCOSTBP = Convert.ToDecimal(reader.GetValue(50));


                                    _context.Add(employee);
                                    await _context.SaveChangesAsync();
                                    uploadCount++;
                                }
                            } while (reader.NextResult());

                            ViewBag.Message = "success";
                        }
                    }
                }
                ViewBag.Message = "empty";
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