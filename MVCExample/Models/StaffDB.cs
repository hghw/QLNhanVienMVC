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
                    ngaySinh = Convert.ToDateTime("09/01/2000"),
                    sdt = "0314646552",
                    diaChi = "Ha Noi",
                    chucVu = "Giam Doc"

                },
                new Staff {
                   maNhanVien = "NV-0002",
                    hoTen = "Huy Duong",
                    ngaySinh = Convert.ToDateTime("12/01/2001"),
                    sdt = "4540651",
                    diaChi = "Thai Nguyen",
                    chucVu = "Nhan Vien"
                },
                new Staff {
                    maNhanVien = "NV-0003",
                    hoTen = "Hoang Duong",
                    ngaySinh = Convert.ToDateTime("18/12/2008"),
                    sdt = "01616565",
                    diaChi = "Lang Son",
                    chucVu = "Truong phong"
                },
                new Staff {
                    maNhanVien = "NV-0004",
                    hoTen = "Duong Van B",
                    ngaySinh = Convert.ToDateTime("10/04/1998"),
                    sdt = "0894948498",
                    diaChi = "Ha Nam",
                    chucVu = "Nhan Vien"
                },
                new Staff {
                    maNhanVien = "NV-0005",
                    hoTen = "Nguyen Van A",
                    ngaySinh = Convert.ToDateTime("23/05/1999"),
                    sdt = "056153221",
                    diaChi = "Bac Giang",
                    chucVu = "Nhan Vien"
                }
            };

            return listNhanVien;
        }
        
    }
}