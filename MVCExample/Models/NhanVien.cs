using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCExample.Models
{
    public class NhanVien
    {
        public string maNhanVien { get; set; }
        public string hoTen { get; set; }
        public string ngaySinh { get; set; }
        public string sdt { get; set; }
        public string chucVu { get; set; }

        /*public string getMaNhanVien(List<NhanVien> listNhanVien)
        {
            float maCount = (float)(listNhanVien.Count + 1) / 1000;
            string maNhanVien = Convert.ToString(maCount);
            maNhanVien = maNhanVien.Replace(".", "");
            maNhanVien = "NV-" + maNhanVien;
            Console.WriteLine(maNhanVien);
            return maNhanVien;
        }*/
    }
}
