using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;



namespace MVCExample.Models
{
    public class Staff
    {
        [Display(Name = "Mã Nhân Viên")]
        public string maNhanVien { set; get; }

        [Display(Name = "Họ tên")]
        public string hoTen { set; get; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ngày/Tháng/Năm")]
        public Nullable<System.DateTime> ngaySinh { get; set; }

        [Display(Name = "Số điện thoại")]
        public string sdt { set; get; }

        [Display(Name = "Địa chỉ")]
        public string diaChi { set; get; }

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
