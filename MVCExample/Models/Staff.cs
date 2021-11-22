using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCExample.Models
{
    public class Staff
    {
        [Required(ErrorMessage = "Phải có mã nhân viên")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Chiều dài không chính xác")]
        [Display(Name = "Mã Nhân Viên")]
        public string maNhanVien { set; get; }

        [Required(ErrorMessage = "Phải có tên")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Chiều dài không chính xác")]
        [Display(Name = "Họ tên")]
        public string hoTen { set; get; }

        [Required(ErrorMessage = "Phải có ngày sinh")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Ngày sinh")]
        public Nullable<System.DateTime> ngaySinh { get; set; }

        [Required(ErrorMessage = "Phải có sđt")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Chiều dài không chính xác")]
        [Display(Name = "Số điện thoại")]
        public string sdt { set; get; }

        [Required(ErrorMessage = "Phải có địa chỉ")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Chiều dài không chính xác")]
        [Display(Name = "Địa chỉ")]
        public string diaChi { set; get; }

        [Required(ErrorMessage = "Phải có chức vụ")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Chiều dài không chính xác")]
        [Display(Name = "Chức vụ")]
        public string chucVu { set; get; }
        /*public string getMaNhanVien(List<Staff> listNhanVien)
        {
            float maCount = (float)(listNhanVien.Count + 1) / 1000;
            string maNhanVien = Convert.ToString(maCount);
            maNhanVien = maNhanVien.Replace(".", "");
            maNhanVien = "NV-" + maNhanVien;
            return maNhanVien;
        }*/
    }

}
