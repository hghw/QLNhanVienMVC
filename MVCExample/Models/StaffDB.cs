using System;
using System.Collections.Generic;
namespace MVCExample.Models
{
    public class StaffDB
    {
        public List<Staff> listAll()
        {
            List<Staff> listNhanVien = new List<Staff>()
            {
                new Staff {
                    maNhanVien = "NV-0001",
                    hoTen = "Hoang ABC",
                    ngaySinh = new DateTime(2000,01,09),
                    sdt = "0314646552",
                    diaChi = "Ha Noi",
                    chucVu = "Giam Doc"

                },
                new Staff {
                   maNhanVien = "NV-0002",
                    hoTen = "Huy Duong",
                    ngaySinh = new DateTime(1998,12,29),
                    sdt = "4540651",
                    diaChi = "Thai Nguyen",
                    chucVu = "Nhan Vien"
                },
                new Staff {
                    maNhanVien = "NV-0003",
                    hoTen = "Hoang Duong",
                    ngaySinh = new DateTime(2010,06,15),
                    sdt = "01616565",
                    diaChi = "Lang Son",
                    chucVu = "Truong phong"
                },
                new Staff {
                    maNhanVien = "NV-0004",
                    hoTen = "Duong Van B",
                    ngaySinh = new DateTime(2005,11,09),
                    sdt = "0894948498",
                    diaChi = "Ha Nam",
                    chucVu = "Nhan Vien"
                },
                new Staff {
                    maNhanVien = "NV-0005",
                    hoTen = "Nguyen Van A",
                    ngaySinh = new DateTime(1995,05,05),
                    sdt = "056153221",
                    diaChi = "Bac Giang",
                    chucVu = "Nhan Vien"
                }
            };

            return listNhanVien;
        }
        
    }
}