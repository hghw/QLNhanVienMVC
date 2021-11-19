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
                    hoTen = "Name 1",
                    ngaySinh = "Name 1",
                    sdt = "Name 1",
                    chucVu = "Name 1"

                },
                new Staff {
                   maNhanVien = "02",
                    hoTen = "Name 1",
                    ngaySinh = "Name 1",
                    sdt = "Name 1",
                    chucVu = "Name 1"
                },
                new Staff {
                    maNhanVien = "03",
                    hoTen = "Name 1",
                    ngaySinh = "Name 1",
                    sdt = "Name 1",
                    chucVu = "Name 1"
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
