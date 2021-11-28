using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;



namespace MVCExample.Models
{
    public class Staff
    {
        [Required(ErrorMessage = "Nhập mã nhân viên")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "Chiều dài không chính xác")]
        [Display(Name = "Mã Nhân Viên")]
        public string maNhanVien { set; get; }

        [Required(ErrorMessage = "Nhập tên")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Chiều dài không chính xác")]
        [Display(Name = "Họ tên")]
        public string hoTen { set; get; }

        [Required(ErrorMessage = "Nhập ngày sinh")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ngày/Tháng/Năm")]
        public Nullable<System.DateTime> ngaySinh { get; set; }

        [Required(ErrorMessage = "Nhập sđt")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "Chiều dài không chính xác")]
        [Display(Name = "Số điện thoại")]
        public string sdt { set; get; }

        [Required(ErrorMessage = "Nhập địa chỉ")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Chiều dài không chính xác")]
        [Display(Name = "Địa chỉ")]
        public string diaChi { set; get; }

        [Required(ErrorMessage = "Nhập chức vụ")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Chiều dài không chính xác")]
        [Display(Name = "Chức vụ")]
        public string chucVu { set; get; }

        public string getMaNhanVien(List<Staff> listNhanVien)
        {
            float maCount = (float)(listNhanVien.Count + 1) / 1000;
            string maNhanVien = Convert.ToString(maCount);
            maNhanVien = maNhanVien.Replace(".", "");
            maNhanVien = "NV-" + maNhanVien;
            return maNhanVien;
        }
        public string getMaNhanVienAdd1(List<Staff> listNhanVien)
        {
            float maCount = (float)(listNhanVien.Count + 2) / 1000;
            string maNhanVien = Convert.ToString(maCount);
            maNhanVien = maNhanVien.Replace(".", "");
            maNhanVien = "NV-" + maNhanVien;
            return maNhanVien;
        }
    }

}
