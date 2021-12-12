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
using Npgsql;
using System.Data;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace MVCExample.Controllers
{
    public class StaffController : Controller
    {

        /*private readonly ILogger<StaffController> _logger;

        public StaffController(ILogger<StaffController> logger)
        {
            _logger = logger;
        }*/
        private readonly IConfiguration _configuration;
        public StaffController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            List<Staff> list = new List<Staff>();
            string sqlDataSource = _configuration.GetConnectionString("StaffConnect");
            string sql = "SELECT * FROM nhan_vien";

            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                list = myCon.Query<Staff>(sql).ToList();
            }
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
            List<Staff> list = new List<Staff>();

            string sqlDataSource = _configuration.GetConnectionString("StaffConnect");
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                // string ma_nhanvien2 = model.getma_nhanvien(list);
                // string sqlma_nhanvien = @"INSERT INTO nhan_vien (ma_nhanvien) VALUES (" + ma_nhanvien2 + ");";
                string sql = @"INSERT INTO nhan_vien (ho_ten, ngay_sinh, sdt, dia_chi, chuc_vu)
                VALUES (@ho_ten, @ngay_sinh, @sdt, @dia_chi, @chuc_vu);";

                var affectedRows = myCon.Execute(sql, model);
                // var affectedRows2 = myCon.Execute(sqlma_nhanvien, model);

            }
            return Json(new { data = list, status = "OK" });

        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            Staff staff = new Staff();

            string sqlDataSource = _configuration.GetConnectionString("StaffConnect");
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                staff = myCon.Query<Staff>("Select * From nhan_vien WHERE ma_nhanvien =" + id, new { id }).FirstOrDefault();
                return View(staff);
            }
        }
        [HttpPost]
        public IActionResult Update(Staff staff)
        {
            string sqlDataSource = _configuration.GetConnectionString("StaffConnect");
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                string sqlQuery = "UPDATE nhan_vien SET ho_ten='" + staff.ho_ten +
                // "',ngay_sinh='" + staff.ngay_sinh +    
                "',sdt='" + staff.sdt +
                "',dia_chi='" + staff.dia_chi +
                "',chuc_vu='" + staff.chuc_vu +
                "' WHERE ma_nhanvien=" + staff.ma_nhanvien;

                var rowAffect = myCon.Execute(sqlQuery);
            }
            return Json(new { status = "OK" });
        }
        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Delete(string id)
        {
            List<Staff> list = new List<Staff>();

            string sqlDataSource = _configuration.GetConnectionString("StaffConnect");
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                string sql = @"delete from nhan_vien
                where ma_nhanvien = " + id;
                var affectedRows = myCon.Execute(sql);
                return Json(new { status = "OK" });
            }
        }
        public IActionResult Search(string keyword)
        {

            List<Staff> list = HttpContext.Session.GetObjectFromJson<List<Staff>>("list");
            //search
            if (!String.IsNullOrEmpty(keyword))
            {
                var searchs = list.Where(s => s.ho_ten.ToLower().Contains(keyword)
                || s.ho_ten.Contains(keyword)
                || s.ho_ten.ToUpper().Contains(keyword)
                || s.dia_chi.Contains(keyword)
                || s.dia_chi.ToUpper().Contains(keyword)
                || s.dia_chi.ToLower().Contains(keyword)
                );
                return View("Search", searchs);
            }
            // if (!String.IsNullOrEmpty(keyword))
            // {
            //     var searchs = list.Where(s => s.ma_nhanvien.Contains(keyword));
            //     return View("Index", searchs);
            // }
            return View("Index", list);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
