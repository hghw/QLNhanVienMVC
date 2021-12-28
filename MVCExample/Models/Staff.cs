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

        [Display(Name = "Ngày sinh")]
        public DateTime ngay_sinh { get; set; }
        [Display(Name = "Số điện thoại")]
        public string sdt { set; get; }
        [Display(Name = "Địa chỉ")]
        public string dia_chi { set; get; }
        [Display(Name = "Chức vụ")]
        public string chuc_vu { set; get; }
        [Display(Name = "ID Phòng ban")]
        public virtual int phongban_id { set; get; }
        
        // Khai bao khoa ngoai
        public virtual phong_ban phong_ban { set; get; }

        public string getma_nhanvien(List<Staff> listNhanVien)
        {
            int maCount = listNhanVien.Count + 1;
            string ma_nhanvien = "NV-";
            if (maCount < 10)
            {
                ma_nhanvien = ma_nhanvien + "000";
            }
            else if (maCount > 9 && maCount <= 99)
            {
                ma_nhanvien = ma_nhanvien + "00";
            }
            else if (maCount > 99 && maCount <= 999)
            {
                ma_nhanvien = ma_nhanvien + "0";
            }
            ma_nhanvien = ma_nhanvien + maCount.ToString();
            return ma_nhanvien;
        }
        public string getma_nhanvienAdd1(List<Staff> listNhanVien)
        {
            int maCount = listNhanVien.Count + 2;
            string ma_nhanvien = "NV-";
            if (maCount < 10)
            {
                ma_nhanvien = ma_nhanvien + "000";
            }
            else if (maCount > 9 && maCount <= 99)
            {
                ma_nhanvien = ma_nhanvien + "00";
            }
            else if (maCount > 99 && maCount <= 999)
            {
                ma_nhanvien = ma_nhanvien + "0";
            }
            ma_nhanvien = ma_nhanvien + maCount.ToString();
            return ma_nhanvien;
        }

    }
}