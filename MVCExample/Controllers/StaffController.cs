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

        private readonly ILogger<StaffController> _logger;

        public StaffController(ILogger<StaffController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            /*Get create*/
            List<Staff> list = HttpContext.Session.GetObjectFromJson<List<Staff>>("list");
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
        public IActionResult Edit(string id)
        {
            List<Staff> list = HttpContext.Session.GetObjectFromJson<List<Staff>>("list");
            HttpContext.Session.SetString("id", id); //luu id sua
            for (int i = 0; i < list.Count; i++)
            {
                if (id == list[i].maNhanVien)
                {
                    return View(list[i]);
                }
            }
            // var std = list.Where(s => s.maNhanVien == id).FirstOrDefault();
            return View("Index", list);
        }
        [HttpPost]
        public IActionResult Edit(Staff staff)
        {
            List<Staff> list = HttpContext.Session.GetObjectFromJson<List<Staff>>("list");
            // var nv = list.Where(s => s.maNhanVien == std.maNhanVien).FirstOrDefault();
            string id = HttpContext.Session.GetString("id"); //get id sua
            for (int i = 0; i < list.Count; i++)
            {
                if (id == list[i].maNhanVien)
                {
                    list[i].maNhanVien = staff.maNhanVien;
                    list[i].hoTen = staff.hoTen;
                    list[i].ngaySinh = staff.ngaySinh;
                    list[i].sdt = staff.sdt;
                    list[i].chucVu = staff.chucVu;
                }
            }
            
            HttpContext.Session.SetObjectAsJson("list", list);



            return View("Index", list);
        }
        public IActionResult Update()
        {
            //var test = HttpContext.Session.GetObjectFromJson<List<string>>("Test");
            Console.WriteLine("Đang xây dựng");
            return View();
        }
        public IActionResult Delete(string id)
        {
            List<Staff> list = HttpContext.Session.GetObjectFromJson<List<Staff>>("list");
            for (int i = 0; i < list.Count; i++)
            {
                if (id == list[i].maNhanVien)
                {
                    list.RemoveAt(i);
                    HttpContext.Session.SetObjectAsJson("list", list);

                    return View("Delete");
                }
            }
            return View("Index", list);
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
