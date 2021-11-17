using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCExample.Models;
using MVCSamples.Extensions;
using MVCSamples.Models;
using Newtonsoft.Json;
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
        Staff staff = new Staff();
        private readonly ILogger<StaffController> _logger;

        public StaffController(ILogger<StaffController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // HttpContext.Session.SetString("maNhanVien", "18130145");

            // HttpContext.Session.SetString("hoTen", "abc");
            // HttpContext.Session.SetString("ngaySinh", "abc");
            // HttpContext.Session.SetString("sdt", "abc");
            // HttpContext.Session.SetString("chucVu", "abc");


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
            List<Staff> listNhanVien = new List<Staff>() {
                new Staff {
                    maNhanVien = "p01",
                    hoTen = "Name 1",
                    ngaySinh = "Name 1",
                    sdt = "Name 1",
                    chucVu = "Name 1"

                },
                new Staff {
                   maNhanVien = "p01",
                    hoTen = "Name 1",
                    ngaySinh = "Name 1",
                    sdt = "Name 1",
                    chucVu = "Name 1",
                },
                new Staff {
                    maNhanVien = "p01",
                    hoTen = "Name 1",
                    ngaySinh = "Name 1",
                    sdt = "Name 1",
                    chucVu = "Name 1"
                }
            };
            MVCSamples.Extensions.SessionExtensions.SetObjectAsJson(HttpContext.Session, "listNhanVien", listNhanVien);
// https://learningprogramming.net/net/asp-net-core-mvc-5/use-session-in-asp-net-core-mvc-5/
            listNhanVien.Add(model);

            // var convertInfo = JsonConvert.SerializeObject(listNhanVien);
            // // List<Staff> result = JsonConvert.DeserializeObject<List<Staff>>(convertInfo);
            // var ses = HttpContext.Session.GetObjectFromJson<List<Staff>>("Staff");
            // HttpContext.Session.SetString("Staff", ses);          // Lấy ISession

            // return RedirectToAction("Index", listNhanVien);
            return View("Index", listNhanVien);
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
