using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCExample.Models;
using MVCSamples.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using MVCSamples.Extensions;

namespace MVCExample.Controllers
{

    public class StaffController : Controller
    {
        List<Staff> listNhanVien = new List<Staff>();
        Staff staff = new Staff();

        private readonly ILogger<StaffController> _logger;

        public StaffController(ILogger<StaffController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
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

            HttpContext.Session.SetObjectAsJson("list", listNhanVien);

            // listNhanVien.Add(model);
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
            List<Staff> list = HttpContext.Session.GetObjectFromJson<List<Staff>>("list");
            /*var sessionStaff = HttpContext.Session.GetObjectFromJson<List<Staff>>("list");*/
            list.Add(model);
            HttpContext.Session.SetObjectAsJson("list", list);


            return View("Index", list);
        }
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                return RedirectToAction("Index", listNhanVien);
            }
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
