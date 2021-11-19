using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCExample.Models;
using MVCSamples.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using MVCSamples.Extensions;
using System.Linq;

namespace MVCExample.Controllers
{

    public class StaffController : Controller
    {

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

        private readonly ILogger<StaffController> _logger;

        public StaffController(ILogger<StaffController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            /*Get create*/
            List<Staff> list = HttpContext.Session.GetObjectFromJson<List<Staff>>("list");
           
            HttpContext.Session.SetObjectAsJson("list", list);

            HttpContext.Session.SetObjectAsJson("list", listNhanVien);
            return View("Index", list);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Staff model)
        {
            List<Staff> list = HttpContext.Session.GetObjectFromJson<List<Staff>>("list");
            list.Add(model);
            HttpContext.Session.SetObjectAsJson("list", list);

            return View("Index", list);
        }
        public IActionResult Edit(string? id)
        {
            List<Staff> list = HttpContext.Session.GetObjectFromJson<List<Staff>>("list");

            var std = list.Where(s => s.maNhanVien == id).FirstOrDefault();
            return View(std);
        }
        [HttpPost]
        public IActionResult Edit(Staff std)
        {
            List<Staff> list = HttpContext.Session.GetObjectFromJson<List<Staff>>("list");
            var nv = list.Where(s => s.maNhanVien == std.maNhanVien).FirstOrDefault();
            list.Remove(nv);
            list.Add(std);
            return RedirectToAction("Index");
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
