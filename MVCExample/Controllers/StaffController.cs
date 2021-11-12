using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCExample.Models;
using MVCSamples.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MVCExample.Controllers
{
    public class StaffController : Controller
    {
        List<NhanVien> listNhanVien = new List<NhanVien>();
        NhanVien nv = new NhanVien();
        private readonly ILogger<StaffController> _logger;

        public StaffController(ILogger<StaffController> logger)
        {
            _logger = logger;
        }
        
        public IActionResult Index(List<NhanVien> listNhanVien)
        {
            return View(listNhanVien);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(NhanVien model)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                nv.maNhanVien = model.maNhanVien;
                nv.hoTen = model.hoTen;
                nv.ngaySinh = model.ngaySinh;
                nv.sdt = model.sdt;
                nv.chucVu = model.chucVu;
                message = nv.maNhanVien + nv.hoTen + nv.ngaySinh + nv.sdt + nv.chucVu;
                listNhanVien.Add(nv);
            }
            else
            {
                message = "Failed to create the product. Please try again";
            }
            return Content(message);
        }
        public IActionResult Edit()
        {
            //var test = HttpContext.Session.GetObjectFromJson<List<string>>("Test");
            Console.WriteLine("Đang xây dựng");
            return View();
        }
        public IActionResult Update()
        {
            //var test = HttpContext.Session.GetObjectFromJson<List<string>>("Test");
            Console.WriteLine("Đang xây dựng");
            return View();
        }
        public IActionResult Delete()
        {
            //var test = HttpContext.Session.GetObjectFromJson<List<string>>("Test");
            Console.WriteLine("Đang xây dựng");
            return View();
        }
        public IActionResult Report()
        {
            //var test = HttpContext.Session.GetObjectFromJson<List<string>>("Test");
            Console.WriteLine("Đang xây dựng");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
