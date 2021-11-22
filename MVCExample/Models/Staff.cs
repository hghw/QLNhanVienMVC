using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCExample.Models
{
    public class Staff
    {
        public string maNhanVien { set; get; }
        public string hoTen { set; get; }
        public string ngaySinh { set; get; }
        public string sdt { set; get; }
        public string diaChi { set; get; }
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
