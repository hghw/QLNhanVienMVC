using Microsoft.AspNetCore.Mvc;
using MVCExample.Models;
using MVCSamples.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Npgsql;
using Microsoft.Extensions.Configuration;
using Dapper;
using OfficeOpenXml;
using System.Linq;
using System.IO;
using System.Drawing;
using OfficeOpenXml.Style;

using Dapper.FastCrud;

using Microsoft.EntityFrameworkCore;

namespace MVCExample.Controllers
{
    enum ErrorCode { OK = 1, ERROR = 2, Duplicate = 3 }

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
            return View();
        }

        public IActionResult GetPaging(int txtPhongban, string txtSearch, int page)
        {
            try
            {
                List<nhan_vien> list = new List<nhan_vien>();//model staff
                string sqlDataSource = _configuration.GetConnectionString("StaffConnect");
                string sql = "SELECT * FROM nhan_vien ORDER BY ma_nhanvien ASC";

                using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
                {
                    using (var context = new nhan_vienContext())
                    {
                        // list = myCon.Query<nhan_vien>(sql).ToList();
                        // list = context.nhan_viens.ToList();

                        list = myCon.Find<nhan_vien>().ToList();

                        var posts = list;
                        //search
                        if (!String.IsNullOrEmpty(txtSearch) && (txtPhongban == 0))
                        {
                            string sqlSearch = @"Select * from nhan_vien 
                        where LOWER(ho_ten) like LOWER('%" + txtSearch + "%') Or UPPER(ho_ten) like UPPER('%" + txtSearch + "%') Or LOWER(dia_chi) like LOWER('%" + txtSearch + "%') Or UPPER(dia_chi) like UPPER('%" + txtSearch + "%') Order By ma_nhanvien ASC";
                            list = myCon.Query<nhan_vien>(sqlSearch).ToList();
                            posts = list.ToList();
                            foreach (var item in posts)
                            {
                                string sqlPB = "SELECT * FROM phong_ban where phongban_id = " + item.phongban_id + "";
                                var PBquery = myCon.Query<phong_ban>(sqlPB).FirstOrDefault();//  
                                item.phong_ban = PBquery;
                            }
                            return Json(new { posts = posts });
                        }
                        //phong ban search
                        if (String.IsNullOrEmpty(txtSearch) && txtPhongban > 0)
                        {
                            string sqlSearch = @"Select * from nhan_vien 
                        where phongban_id = " + txtPhongban + " Order By ma_nhanvien ASC";
                            list = myCon.Query<nhan_vien>(sqlSearch).ToList();
                            posts = list.ToList();
                            foreach (var item in posts)
                            {
                                string sqlPB = "SELECT * FROM phong_ban where phongban_id = " + item.phongban_id + "";
                                var PBquery = myCon.Query<phong_ban>(sqlPB).FirstOrDefault();//  
                                item.phong_ban = PBquery;
                            }
                            return Json(new { posts = posts });
                        }
                        //ket hop 2 dieu kien
                        if (!String.IsNullOrEmpty(txtSearch) && txtPhongban > 0)
                        {
                            string sqltotal = @"Select * from nhan_vien 
                    where (LOWER(ho_ten) like LOWER('%" + txtSearch + "%') Or UPPER(ho_ten) like UPPER('%" + txtSearch + "%') Or LOWER(dia_chi) like LOWER('%" + txtSearch + "%') Or UPPER(dia_chi) like UPPER('%" + txtSearch + "%')) and phongban_id = " + txtPhongban + " Order By ma_nhanvien ASC";
                            list = myCon.Query<nhan_vien>(sqltotal).ToList();
                            posts = list.ToList();
                            foreach (var item in posts)
                            {
                                string sqlPB = "SELECT * FROM phong_ban where phongban_id = " + item.phongban_id + "";
                                var PBquery = myCon.Query<phong_ban>(sqlPB).FirstOrDefault();//  
                                item.phong_ban = PBquery;
                            }
                            return Json(new { posts = posts });
                        }

                        //page

                        int ITEMS_PER_PAGE = 10;

                        string sqlPageCount = @"SELECT COUNT(ma_nhanvien)FROM nhan_vien;";
                        int totalRecord = myCon.Query<int>(sqlPageCount).FirstOrDefault();
                        int countPages = (int)Math.Ceiling((double)totalRecord / ITEMS_PER_PAGE);
                        if (page == 0)
                        {
                            page = 1;
                        }
                        if (page > countPages)
                        {
                            page = countPages;
                        }
                        int PageNumber = (page - 1) * ITEMS_PER_PAGE;
                        // tổng số trang
                        string sqlPage = @"SELECT * FROM nhan_vien ORDER BY ma_nhanvien OFFSET " + PageNumber + " ROWS FETCH NEXT " + (ITEMS_PER_PAGE) + " ROWS ONLY";
                        posts = myCon.Query<nhan_vien>(sqlPage).ToList();//  

                        foreach (var item in posts)
                        {
                            string sqlPB = "SELECT * FROM phong_ban where phongban_id = " + item.phongban_id + "";
                            var PBquery = myCon.Query<phong_ban>(sqlPB).FirstOrDefault();//  
                            item.phong_ban = PBquery;
                        }

                        return Json(new
                        {
                            page = page,
                            countPages = countPages,
                            posts = posts
                        });
                    }

                }
            }
            catch (Exception)
            {
                return Json(new
                {
                    status = ErrorCode.ERROR.ToString()// bug offset sql
                });
            }

        }
        public IActionResult GetDropdown()
        {
            var listPhongBan = new List<phong_ban>();
            string sqlDataSource = _configuration.GetConnectionString("StaffConnect");
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                string sqlDropdown = "SELECT * FROM phong_ban";
                listPhongBan = myCon.Query<phong_ban>(sqlDropdown).ToList();

                return Json(new
                {
                    data = listPhongBan
                });
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(nhan_vien model)
        {
            List<nhan_vien> list = new List<nhan_vien>();
            nhan_vien staff = new nhan_vien();
            string sqlDataSource = _configuration.GetConnectionString("StaffConnect");
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                string sqlAll = "Select * from nhan_vien Order By ma_nhanvien ASC"; //truy van csdl de dem soluong csdl
                var listAll = myCon.Query<nhan_vien>(sqlAll).ToList(); //get ma nhan vien +1
                model.ma_nhanvien = staff.getma_nhanvien(listAll);
                for (int i = 0; i < listAll.Count; i++)
                {
                    if (model.ma_nhanvien == listAll[i].ma_nhanvien)
                    {
                        model.ma_nhanvien = staff.getma_nhanvienAdd1(listAll);
                    }
                    if (model.ho_ten == listAll[i].ho_ten && model.ngay_sinh == listAll[i].ngay_sinh)
                    {
                        return Json(new { status = ErrorCode.Duplicate.ToString() });
                    }
                }

                string sql = @"INSERT INTO nhan_vien (ma_nhanvien, ho_ten, ngay_sinh, sdt, dia_chi, chuc_vu, phongban_id)
                VALUES ('" + model.ma_nhanvien +
                "', @ho_ten, @ngay_sinh, @sdt, @dia_chi, @chuc_vu, @phongban_id);";
                var affectedRows = myCon.Execute(sql, model);
                if (affectedRows != 0)
                {
                    return Json(new
                    {
                        data = listAll,
                        status = ErrorCode.OK.ToString()
                    });
                }
                else
                {
                    return Json(new
                    {
                        status = ErrorCode.ERROR.ToString()
                    });
                }
            }

        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            nhan_vien staff = new nhan_vien();

            string sqlDataSource = _configuration.GetConnectionString("StaffConnect");
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                staff = myCon.Query<nhan_vien>("Select * From nhan_vien WHERE ma_nhanvien ='" + id + "'", new { id }).FirstOrDefault();
                return View(staff);
            }
        }
        [HttpPost]
        public IActionResult Update(nhan_vien staff)
        {
            string sqlDataSource = _configuration.GetConnectionString("StaffConnect");
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                string sqlAll = "Select * from nhan_vien where ma_nhanvien != '" + staff.ma_nhanvien + "'"; // loai tru nhan  vien nay de so sanh Duplicate hay khong 
                var listAll = myCon.Query<nhan_vien>(sqlAll).ToList();
                for (int i = 0; i < listAll.Count; i++)
                {
                    if (staff.ho_ten == listAll[i].ho_ten && staff.ngay_sinh == listAll[i].ngay_sinh)
                    {
                        return Json(new { status = ErrorCode.Duplicate.ToString() });
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
                "',phongban_id='" + staff.phongban_id +
                "' WHERE ma_nhanvien='" + staff.ma_nhanvien + "'";

                var rowAffect = myCon.Execute(sqlQuery);
                if (rowAffect != 0)
                {
                    return Json(new
                    {
                        data = listAll,
                        status = ErrorCode.OK.ToString()
                    });
                }
                else
                {
                    return Json(new
                    {
                        status = ErrorCode.ERROR.ToString()
                    });
                }

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
                var listAll = myCon.Query<nhan_vien>(sqlAll).ToList();
                string sql = @"delete from nhan_vien
                where ma_nhanvien = '" + id + "'";
                var affectedRows = myCon.Execute(sql);
                if (affectedRows != 0)
                {
                    return Json(new { status = ErrorCode.OK.ToString() });
                }
                else
                {
                    return Json(new
                    {
                        status = ErrorCode.ERROR.ToString()
                    });
                }
            }
        }
        public void DownloadExcel()
        {
            string sqlDataSource = _configuration.GetConnectionString("StaffConnect");
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                string sqlAll = "Select * from nhan_vien  Order By ma_nhanvien ASC"; //truy van csdl de dem soluong csdl
                var listAll = myCon.Query<nhan_vien>(sqlAll).ToList(); //get ma nhan vien +1
                foreach (var item in listAll)
                {
                    string sqlPB = "SELECT * FROM phong_ban where phongban_id = " + item.phongban_id + "";
                    var PBquery = myCon.Query<phong_ban>(sqlPB).FirstOrDefault();//  
                    item.phong_ban = PBquery;
                }
                using (ExcelPackage Ep = new ExcelPackage())
                {
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Staff");
                    Sheet.Cells["A3"].Value = "Mã nhân viên";
                    Sheet.Cells["B3"].Value = "Họ tên";
                    Sheet.Cells["C3"].Value = "Ngày sinh";
                    Sheet.Cells["D3"].Value = "SĐT";
                    Sheet.Cells["E3"].Value = "Địa chỉ";
                    Sheet.Cells["F3"].Value = "Chức vụ";
                    Sheet.Cells["G3"].Value = "Phòng ban";
                    int row = 4;
                    foreach (var item in listAll)
                    {
                        Sheet.Cells[string.Format("A{0}", row)].Value = item.ma_nhanvien;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.ho_ten;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.ngay_sinh;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.sdt;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.dia_chi;
                        Sheet.Cells[string.Format("F{0}", row)].Value = item.chuc_vu;
                        Sheet.Cells[string.Format("G{0}", row)].Value = item.phong_ban.ten_phong_ban;
                        Sheet.Cells[string.Format("C{0}", row)].Style.Numberformat.Format = "dd/mm/yyyy";
                        row++;
                    }
                    FormatExcel(Sheet, listAll);
                    Ep.SaveAs(new FileInfo("nhanvien.xlsx"));
                }
            }
        }
        private void FormatExcel(ExcelWorksheet worksheet, List<nhan_vien> list)
        {

            worksheet.DefaultColWidth = 15;
            worksheet.DefaultRowHeight = 20;
            /*worksheet.Cells.Style.WrapText = true;*/
            using (ExcelRange range = worksheet.Cells["A1:G2"])
            {
                range.Merge = true;
                range.Value = "Quản lý nhân viên";
                range.Style.Font.SetFromFont(new Font("Times New Roman", 14));
                range.Style.Font.Bold = true;
            }
            using (ExcelRange range = worksheet.Cells["A3:G3"])
            {
                range.Style.Font.Bold = true;
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                range.Style.Font.SetFromFont(new Font("Times New Roman", 11));
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                range.Style.Border.Bottom.Color.SetColor(Color.Black);
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.LightSeaGreen);
            }
            using (ExcelRange range = worksheet.Cells["A1:G" + (list.Count + 3) + ""])
            {
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                range.Style.ShrinkToFit = true;
                range.Style.Border.BorderAround(ExcelBorderStyle.Thin);
            }

        }

        [HttpGet]
        public IActionResult ExportExcel()
        {
            if (System.IO.File.Exists("nhanvien.xlsx"))
            {
                return Json(new
                {
                    status = ErrorCode.ERROR.ToString()
                });
            }
            DownloadExcel();
            return Json(new
            {
                status = ErrorCode.OK.ToString()
            });
        }
        // public IActionResult PhongBan(){

        //     return ;
        // }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
