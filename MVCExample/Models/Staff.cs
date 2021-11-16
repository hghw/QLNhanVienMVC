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
        public string chucVu { set; get; }

        // List<Staff> listNhanVien = new List<Staff>();
        // public void listAll(){
        //     List<NhanVien> listNhanVien = new List<NhanVien>();
        //     listNhanVien.maNhanVien = maNhanVien;
        //     listNhanVien.hoTen = hoTen;
        //     listNhanVien.ngaySinh = ngaySinh;
        //     listNhanVien.sdt = sdt;
        //     listNhanVien.chucVu = chucVu;
        //     return listNhanVien;
        // }
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
