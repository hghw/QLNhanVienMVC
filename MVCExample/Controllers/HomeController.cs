using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCSamples.Models;
using MVCSamples.Extensions;
using MVCExample.Models;

namespace MVCSamples.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        List<Staff> listNhanVien = new List<Staff>() {
                new Staff {
                    maNhanVien = "01",
                    hoTen = "Hoang ABC",
                    ngaySinh = Convert.ToDateTime("09/01/2000"),
                    sdt = "0314646552",
                    diaChi = "Ha Noi",
                    chucVu = "Giam Doc"

                },
                new Staff {
                   maNhanVien = "02",
                    hoTen = "Huy Duong",
                    ngaySinh = Convert.ToDateTime("12/01/2001"),
                    sdt = "4540651",
                    diaChi = "Thai Nguyen",
                    chucVu = "Nhan Vien"
                },
                new Staff {
                    maNhanVien = "03",
                    hoTen = "Hoang Duong",
                    ngaySinh = Convert.ToDateTime("18/12/2008"),
                    sdt = "01616565",
                    diaChi = "Lang Son",
                    chucVu = "Truong phong"
                },
                new Staff {
                    maNhanVien = "04",
                    hoTen = "Duong Van B",
                    ngaySinh = Convert.ToDateTime("10/04/1998"),
                    sdt = "0894948498",
                    diaChi = "Ha Nam",
                    chucVu = "Nhan Vien"
                },
                new Staff {
                    maNhanVien = "05",
                    hoTen = "Nguyen Van A",
                    ngaySinh = Convert.ToDateTime("23/05/1999"),
                    sdt = "056153221",
                    diaChi = "Bac Giang",
                    chucVu = "Nhan Vien"
                }
            };
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            HttpContext.Session.SetObjectAsJson("list", listNhanVien);
            return View();
        }

        public IActionResult Privacy()
        {
            //var test = HttpContext.Session.GetObjectFromJson<List<string>>("Test");
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
