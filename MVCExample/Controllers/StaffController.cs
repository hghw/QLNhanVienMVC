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
        List<Staff> listNhanVien = new List<Staff>();
        Staff nv = new Staff();
        private readonly ILogger<StaffController> _logger;

        public StaffController(ILogger<StaffController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(Staff model)
        {
            // nv.maNhanVien = model.maNhanVien;
            // nv.hoTen = model.hoTen;
            // nv.ngaySinh = model.ngaySinh;
            // nv.sdt = model.sdt;
            // nv.chucVu = model.chucVu;
            listNhanVien.Add(model);
            return View(listNhanVien);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Staff model)
        {
            listNhanVien.Add(model);
            return RedirectToAction("Index");
            // return View(model);
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
