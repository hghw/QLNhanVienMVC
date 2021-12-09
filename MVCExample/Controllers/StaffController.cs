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
using static MVCExample.Helper;
using Npgsql;
using System.Data;

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
            using(NpgsqlConnection conn = GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM nhan_vien";
                NpgsqlCommand cmm = new NpgsqlCommand(sql, conn);
            }
            return View();
        }
        private static NpgsqlConnection GetConnection()
        {
            /*chuoi connect Server=127.0.0.1;Port=5433;Database=NhanVienDB;User Id = postgres; Password=123;*/
            return new NpgsqlConnection(@"Server=localhost;Port=5433;User Id = postgres; Password=123;Database=NhanVienDB;");
        }
        private static void TestConnection()
        {
            using (NpgsqlConnection conn = GetConnection())
            {
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    Console.WriteLine("Ket noi thanh cong");
                }
                else
                {
                    Console.WriteLine("Ket noi that bai");

                }
            }
        }
        [HttpGet]
        [NoDirectAccess]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Staff model)
        {
            using (NpgsqlConnection conn = GetConnection())
            {
                string sql = @"INSERT INTO public.nhan_vien (
ma_nhanvien, ho_ten, ngay_sinh, sdt, dia_chi, chuc_vu) VALUES (
'NV-0001
', 'Hoang ABC', '05/05/1995'::date, '0376414848', 'Lang Son', 'Truong Phong');";
                NpgsqlCommand cmm = new NpgsqlCommand(sql, conn);

                conn.Open();
                int n = cmm.ExecuteNonQuery();
                if (n == 1)
                {
                    Console.WriteLine("Insert thanh cong");
                }
                else
                {
                    Console.WriteLine("Insert that bai");

                }
            }

            List<Staff> list = HttpContext.Session.GetObjectFromJson<List<Staff>>("list");
            Staff staff = new Staff();
            model.maNhanVien = staff.getMaNhanVien(list);
            for (int i = 0; i < list.Count; i++)
            {
                if (model.maNhanVien == list[i].maNhanVien)
                {
                    model.maNhanVien = staff.getMaNhanVienAdd1(list);
                }
                // kiem tra hoten ngay sinh trung khi them
                if (model.hoTen == list[i].hoTen && model.ngaySinh == list[i].ngaySinh)
                {
                    return Json(new { status = "LOI" });
                }
            }
            list.Add(model);
            HttpContext.Session.SetObjectAsJson("list", list);
            return Json(new { data = list, status = "OK" });
        }
        [HttpGet]
        [NoDirectAccess]

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
        [NoDirectAccess]
        public IActionResult Update(Staff staff)
        {
            List<Staff> list = HttpContext.Session.GetObjectFromJson<List<Staff>>("list");
            // var nv = list.Where(s => s.maNhanVien == std.maNhanVien).FirstOrDefault();
            string id = staff.maNhanVien;
            for (int i = 0; i < list.Count; i++)
            {
                // kiem tra hoten ngay sinh trung khi them
                if (staff.hoTen == list[i].hoTen && staff.ngaySinh == list[i].ngaySinh)
                {
                    return Json(new { status = "LOI" });
                }
                if (id == list[i].maNhanVien)
                {
                    // list[i].maNhanVien = staff.maNhanVien;
                    list[i].hoTen = staff.hoTen;
                    list[i].ngaySinh = staff.ngaySinh;
                    list[i].sdt = staff.sdt;
                    list[i].diaChi = staff.diaChi;
                    list[i].chucVu = staff.chucVu;
                }
            }

            HttpContext.Session.SetObjectAsJson("list", list);
            return Json(new { data = list, status = "OK" });

        }
        [HttpGet]
        [NoDirectAccess]
        public IActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Delete(string id)
        {
            List<Staff> list = HttpContext.Session.GetObjectFromJson<List<Staff>>("list");

            for (int i = 0; i < list.Count; i++)
            {
                if (id == list[i].maNhanVien)
                {
                    list.RemoveAt(i);
                    HttpContext.Session.SetObjectAsJson("list", list);
                    return Json(new { status = "OK" });
                }
            }
            return Json(new { status = "OKE" });
        }
        public IActionResult Search(string keyword)
        {

            List<Staff> list = HttpContext.Session.GetObjectFromJson<List<Staff>>("list");
            //search
            if (!String.IsNullOrEmpty(keyword))
            {
                var searchs = list.Where(s => s.hoTen.ToLower().Contains(keyword)
                || s.hoTen.Contains(keyword)
                || s.hoTen.ToUpper().Contains(keyword)
                || s.diaChi.Contains(keyword)
                || s.diaChi.ToUpper().Contains(keyword)
                || s.diaChi.ToLower().Contains(keyword)
                );
                return View("Search", searchs);
            }
            // if (!String.IsNullOrEmpty(keyword))
            // {
            //     var searchs = list.Where(s => s.maNhanVien.Contains(keyword));
            //     return View("Index", searchs);
            // }
            return View("Index", list);
        }
        public void SetAlert(string messenge, string type)
        {
            TempData["AlertMessenge"] = messenge;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
