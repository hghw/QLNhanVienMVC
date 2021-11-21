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

        public IActionResult Index(string keyword)
        {
            /*Get create*/
            List<Staff> list = HttpContext.Session.GetObjectFromJson<List<Staff>>("list");

            //search
            if (!String.IsNullOrEmpty(keyword))
            {
                var searchs = list.Where(s => s.maNhanVien.Contains(keyword));
                return View("Index", searchs);
            }
            //end search
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
        [HttpGet]
        public IActionResult Delete(string id)
        {
            List<Staff> list = HttpContext.Session.GetObjectFromJson<List<Staff>>("list");
            HttpContext.Session.SetString("id", id); //luu id sua

            for (int i = 0; i < list.Count; i++)
            {
                if (id == list[i].maNhanVien)
                {
                    return PartialView("Delete", list[i]);
                }
            }
            return View("Delete");
        }
        [HttpPost]
        public IActionResult Delete(Staff staff)
        {
            List<Staff> list = HttpContext.Session.GetObjectFromJson<List<Staff>>("list");
            string id = HttpContext.Session.GetString("id"); //get id sua

            for (int i = 0; i < list.Count; i++)
            {
                if (id == list[i].maNhanVien)
                {
                    list.RemoveAt(i);
                    HttpContext.Session.SetObjectAsJson("list", list);

                    return View("Index", list);
                }
            }
            return View("Index", list);
        }
        // public IActionResult Search(string keyword)
        // {

        //     List<Staff> list = HttpContext.Session.GetObjectFromJson<List<Staff>>("list");

        //     if (!String.IsNullOrEmpty(keyword))
        //     {
        //         var searchs = list.Where(s => s.maNhanVien.Contains(keyword));
        //         return View("Index", searchs);
        //     }
        //     return View();
        // }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
