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
        // [BindProperty(SupportsGet = true, Name = "p")]
        public int currentPage { get; set; }
        public int countPages { get; set; }
        public IActionResult Index(int page)
        {
            List<Staff> list = new List<Staff>();
            string sqlDataSource = _configuration.GetConnectionString("StaffConnect");
            string sql = "SELECT * FROM nhan_vien Order By ma_nhanvien ASC";

            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                list = myCon.Query<Staff>(sql).ToList();
                int ITEMS_PER_PAGE = 3;

                int totalItems = list.Count;
                countPages = (int)Math.Ceiling((double)totalItems / ITEMS_PER_PAGE);

                if (page == 0)
                {
                    page = 1;
                }
                if (page > countPages)
                {
                    page = countPages;
                }

                var posts = list.Skip(ITEMS_PER_PAGE * (page - 1)).Take(ITEMS_PER_PAGE).ToList();

                ViewData["Page"] = page;
                ViewData["countPages"] = countPages;

            return View(posts);
            }
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
            Staff staff = new Staff();
            string sqlDataSource = _configuration.GetConnectionString("StaffConnect");
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                string sqlAll = "Select * from nhan_vien"; //truy van csdl de dem soluong csdl
                var listAll = myCon.Query<Staff>(sqlAll).ToList(); //get ma nhan vien +1
                model.ma_nhanvien = staff.getma_nhanvien(listAll);
                for (int i = 0; i < listAll.Count; i++)
                {
                    if (model.ma_nhanvien == listAll[i].ma_nhanvien)
                    {
                        model.ma_nhanvien = staff.getma_nhanvienAdd1(listAll);
                    }
                    if (model.ho_ten == listAll[i].ho_ten && model.ngay_sinh == listAll[i].ngay_sinh)
                    {
                        return Json(new { status = "LOI" });
                    }
                }
                string sql = @"INSERT INTO nhan_vien (ma_nhanvien, ho_ten, ngay_sinh, sdt, dia_chi, chuc_vu)
                VALUES ('" + model.ma_nhanvien +
                "', @ho_ten, @ngay_sinh, @sdt, @dia_chi, @chuc_vu);";
                var affectedRows = myCon.Execute(sql, model);
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
                staff = myCon.Query<Staff>("Select * From nhan_vien WHERE ma_nhanvien ='" + id + "'", new { id }).FirstOrDefault();
                return View(staff);
            }
        }
        [HttpPost]
        public IActionResult Update(Staff staff)
        {
            string sqlDataSource = _configuration.GetConnectionString("StaffConnect");
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                string sqlAll = "Select * from nhan_vien"; //truy van csdl de dem soluong csdl
                var listAll = myCon.Query<Staff>(sqlAll).ToList(); //get ma nhan vien +1
                for (int i = 0; i < listAll.Count; i++)
                {
                    if (staff.ho_ten == listAll[i].ho_ten && staff.ngay_sinh == listAll[i].ngay_sinh)
                    {
                        return Json(new { status = "LOI" });
                    }
                    if (staff.ho_ten == listAll[i].ho_ten && staff.ngay_sinh == listAll[i].ngay_sinh)
                    {
                        return Json(new { status = "LOI" });
                    }
                }

                string sqlQuery = "UPDATE nhan_vien SET ho_ten='" + staff.ho_ten +
                "',ngay_sinh='" + staff.ngay_sinh +
                "',sdt='" + staff.sdt +
                "',dia_chi='" + staff.dia_chi +
                "',chuc_vu='" + staff.chuc_vu +
                "' WHERE ma_nhanvien='" + staff.ma_nhanvien+ "'";

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
            string sqlDataSource = _configuration.GetConnectionString("StaffConnect");
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                string sql = @"delete from nhan_vien
                where ma_nhanvien = '" + id + "'";
                var affectedRows = myCon.Execute(sql);
                return Json(new { status = "OK" });
            }
        }
        public IActionResult Search(string keyword)
        {
            List<Staff> list = new List<Staff>();
            string sqlDataSource = _configuration.GetConnectionString("StaffConnect");
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                string sql = @"Select * from nhan_vien
                where ho_ten like '%" + keyword + "%' Or dia_chi like '%" + keyword +"%'";
                list = myCon.Query<Staff>(sql).ToList();

                return View(list);
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
