using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCExample.Models;
using MVCSamples.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using MVCSamples.Extensions;

using Npgsql;
using System.Data;
using Microsoft.Extensions.Configuration;
using Dapper;
using OfficeOpenXml;
using System.Linq;

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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetPaging(string txtSearch, int page)
        {
            List<Staff> list = new List<Staff>();
            string sqlDataSource = _configuration.GetConnectionString("StaffConnect");
            string sql = "SELECT * FROM nhan_vien Order By ma_nhanvien ASC";
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                list = myCon.Query<Staff>(sql).ToList();
                //search
                if (!String.IsNullOrEmpty(txtSearch))
                {
                    ViewBag.txtSearch = txtSearch;
                    string sqlSearch = @"Select * from nhan_vien
                    where LOWER(ho_ten) like LOWER('%" + txtSearch + "%') Or UPPER(ho_ten) like UPPER('%" + txtSearch + "%') Or LOWER(dia_chi) like LOWER('%" + txtSearch + "%') Or UPPER(dia_chi) like UPPER('%" + txtSearch + "%')";
                    list = myCon.Query<Staff>(sqlSearch).ToList();
                }
                //page
                int ITEMS_PER_PAGE = 10;
                int skiper = ITEMS_PER_PAGE * (page - 1);

                string sqlPage = @"SELECT * FROM nhan_vien ORDER BY ma_nhanvien OFFSET "+(skiper) +" ROWS FETCH NEXT "+ (ITEMS_PER_PAGE)+ " ROWS ONLY";
                var posts = myCon.Query<Staff>(sqlPage).ToList();
                int countPages = posts.Count;//chua xong

                return Json(new
                {
                    page = page,
                    countPages = countPages,
                    posts = posts
                });
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
                string sqlAll = "Select * from nhan_vien Order By ma_nhanvien ASC"; //truy van csdl de dem soluong csdl
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


                return Json(new { status = "OK" });
            }

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
                string sqlAll = "Select * from nhan_vien where ma_nhanvien != '" + staff.ma_nhanvien + "'"; //truy van csdl de dem soluong csdl
                var listAll = myCon.Query<Staff>(sqlAll).ToList(); //get ma nhan vien +1
                for (int i = 0; i < listAll.Count; i++)
                {
                    if (staff.ho_ten == listAll[i].ho_ten && staff.ngay_sinh == listAll[i].ngay_sinh)
                    {
                        return Json(new { status = "LOI" });
                    }
                }
                //format ngay sinh no bugg
                var dateformat = (staff.ngay_sinh);
                var d = dateformat.Day;
                var m = dateformat.Month;
                var y = dateformat.Year;
                var datetie = (d + "/" + m + "/" + y);

                string sqlQuery = "UPDATE nhan_vien SET ho_ten='" + staff.ho_ten +
                "',ngay_sinh='" + datetie +
                "',sdt='" + staff.sdt +
                "',dia_chi='" + staff.dia_chi +
                "',chuc_vu='" + staff.chuc_vu +
                "' WHERE ma_nhanvien='" + staff.ma_nhanvien + "'";

                var rowAffect = myCon.Execute(sqlQuery);
                return Json(new { status = "OK" });
            }
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
                string sqlAll = "Select * from nhan_vien  Order By ma_nhanvien ASC"; //truy van csdl de dem soluong csdl
                var listAll = myCon.Query<Staff>(sqlAll).ToList(); //get ma nhan vien +1
                string sql = @"delete from nhan_vien
                where ma_nhanvien = '" + id + "'";
                var affectedRows = myCon.Execute(sql);
                var listAll2 = myCon.Query<Staff>(sqlAll).ToList(); //get ma nhan vien +1

                return Json(new { data = listAll2, status = "OK" });
            }
        }
        /*public void DownloadExcel()
        {
             string sqlDataSource = _configuration.GetConnectionString("StaffConnect");
             using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
             {
                 string sqlAll = "Select * from nhan_vien  Order By ma_nhanvien ASC"; //truy van csdl de dem soluong csdl
                 var listAll = myCon.Query<Staff>(sqlAll).ToList(); //get ma nhan vien +1

                 ExcelPackage Ep = new ExcelPackage();
                 ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Staff");
                 Sheet.Cells["A1"].Value = "Mã nhân viên";
                 Sheet.Cells["B1"].Value = "Họ tên";
                 Sheet.Cells["C1"].Value = "Ngày sinh";
                 Sheet.Cells["D1"].Value = "SĐT";
                 Sheet.Cells["E1"].Value = "Địa chỉ";
                 Sheet.Cells["F1"].Value = "Chức vụ";
                 int row = 2;
                 foreach (var item in listAll)
                 {

                     Sheet.Cells[string.Format("A{0}", row)].Value = item.ma_nhanvien;
                     Sheet.Cells[string.Format("B{0}", row)].Value = item.ho_ten;
                     Sheet.Cells[string.Format("C{0}", row)].Value = item.ngay_sinh;
                     Sheet.Cells[string.Format("D{0}", row)].Value = item.sdt;
                     Sheet.Cells[string.Format("E{0}", row)].Value = item.dia_chi;
                     Sheet.Cells[string.Format("F{0}", row)].Value = item.chuc_vu;
                     row++;
                 }
                 Sheet.Cells["A:AZ"].AutoFitColumns();
                 Session["DownloadExcel_FileManager"] = excelPackage.GetAsByteArray();  
             }
        }*/
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
