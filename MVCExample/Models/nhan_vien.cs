using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
    
namespace MVCExample.Models
{
    [Table("nhan_vien")]
    public class nhan_vien
    {
        [Key]
        public string ma_nhanvien { set; get; }
        [Display(Name = "Họ tên")]

        [StringLength(50)]
        [Column(TypeName = "varchar")]

        public string ho_ten { set; get; }
        [DataType(DataType.Date)]
        [Display(Name = "Ngày sinh")]
        [Column(TypeName = "date")]

        public DateTime ngay_sinh { get; set; }
        [Display(Name = "Số điện thoại")]

        [StringLength(10)]
        [Column(TypeName = "varchar")]

        public string sdt { set; get; }
        [Display(Name = "Địa chỉ")]

        [StringLength(100)]
        [Column(TypeName = "varchar")]

        public string dia_chi { set; get; }
        [Display(Name = "Chức vụ")]

        [StringLength(50)]
        [Column(TypeName = "varchar")]

        public string chuc_vu { set; get; }
        [Display(Name = "ID Phòng ban")]
        [Column(TypeName = "int")]
        public virtual int phongban_id { set; get; }

        // Khai bao khoa ngoai
        [ForeignKey("nhan_vien_fk")]
        public virtual phong_ban phong_ban { set; get; }

/*        public long x { set; get; }
        public long y { set; get; }*/

        public string getma_nhanvien(List<nhan_vien> listNhanVien)
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
        public string getma_nhanvienAdd1(List<nhan_vien> listNhanVien)
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