using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;



namespace MVCExample.Models
{
    public class Staff
    {
        [Display(Name = "Mã Nhân Viên")]
        public string ma_nhanvien { set; get; }

        [Display(Name = "Họ tên")]
        public string ho_ten { set; get; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ngày/Tháng/Năm")]
        public Nullable<System.DateTime> ngay_sinh { get; set; }

        [Display(Name = "Số điện thoại")]
        public string sdt { set; get; }

        [Display(Name = "Địa chỉ")]
        public string dia_chi { set; get; }

        [Display(Name = "Chức vụ")]
        public string chuc_vu { set; get; }

        public string getma_nhanvien(List<Staff> listNhanVien)
        {
            float maCount = (float)(listNhanVien.Count + 1) / 1000;
            string ma_nhanvien = Convert.ToString(maCount);
            ma_nhanvien = ma_nhanvien.Replace(",", "");
            ma_nhanvien = "NV-" + ma_nhanvien;
            return ma_nhanvien;
        }
        public string getma_nhanvienAdd1(List<Staff> listNhanVien)
        {
            float maCount = (float)(listNhanVien.Count + 2) / 1000;
            string ma_nhanvien = Convert.ToString(maCount);
            ma_nhanvien = ma_nhanvien.Replace(",", "");
            ma_nhanvien = "NV-" + ma_nhanvien;
            return ma_nhanvien;
        }
    }

}
