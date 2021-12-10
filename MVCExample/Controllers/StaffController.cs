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
using Microsoft.Extensions.Configuration;

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
        [HttpGet]
        public IActionResult Index()
        {
            List<Staff> list = new List<Staff>();
            string sqlDataSource = _configuration.GetConnectionString("StaffConnect");
            NpgsqlDataReader myReader;
            using(NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                string sql = @"SELECT * FROM nhan_vien";
                using (NpgsqlCommand myCommand = new NpgsqlCommand(sql, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    list.Add(myCommand);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return View("Index", list);
        }
        /*[HttpGet]
        public IActionResult Create()
        {
            return View();
        }*/
        [HttpPost]
        public IActionResult Create(Staff model)
        {
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StaffConnect");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                string sql = @"INSERT INTO nhan_vien (ma_nhanvien, ho_ten, ngay_sinh, sdt, dia_chi, chuc_vu)
                VALUES (@maNhanVien,@hoTen, @ngaySinh, @sdt, @diaChi, @chucVu);";
                using (NpgsqlCommand myCommand = new NpgsqlCommand(sql, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ma_nhanvien", model.maNhanVien);
                    myCommand.Parameters.AddWithValue("@ho_ten", model.hoTen);
                    myCommand.Parameters.AddWithValue("@ngay_sinh", model.ngaySinh);
                    myCommand.Parameters.AddWithValue("@sdt", model.sdt);
                    myCommand.Parameters.AddWithValue("@dia_chi", model.diaChi);
                    myCommand.Parameters.AddWithValue("@chuc_vu", model.chucVu);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }


            }
            return Json(table);
        }
        [HttpGet]

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
        public IActionResult Update(Staff staff)
        {
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StaffConnect");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                string sql = @"UPDATE nhan_vien 
                set ho_ten = @hoTen,
                sdt = @sdt,
                dia_chi = @diaChi,
                chuc_vu = @chucVu
                where ma_nhanvien = @maNhanVien";
                using (NpgsqlCommand myCommand = new NpgsqlCommand(sql, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ma_nhanvien", staff.maNhanVien);
                    myCommand.Parameters.AddWithValue("@ho_ten", staff.hoTen);
                    myCommand.Parameters.AddWithValue("@ngay_sinh", staff.ngaySinh);
                    myCommand.Parameters.AddWithValue("@sdt", staff.sdt);
                    myCommand.Parameters.AddWithValue("@dia_chi", staff.diaChi);
                    myCommand.Parameters.AddWithValue("@chuc_vu", staff.chucVu);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return Json(table);

        }
        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Delete(string id)
        {
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StaffConnect");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                string sql = @"delete from nhan_vien
                where ma_nhanvien = @maNhanVien";
                using (NpgsqlCommand myCommand = new NpgsqlCommand(sql, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ma_nhanvien", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }


            }
            return Json(table);
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
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
