using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BaiTapLon_LTCSDL
{
    class kiemtradn_class
    {
        connectData_Class cnd;
        public Boolean KiemTraDangNhap(string ten, string pas)
        {
            cnd = new connectData_Class();
            DataTable dt = cnd.Getdata("select * from dbo.TaiKhoan tk where tk.TenDangNhap = N'" + ten + "' and tk.MatKhau = N'" + pas + "'");
            Boolean check = false;
            if (dt != null)
            {
                if (dt.Rows.Count == 1)
                {
                    check = true;

                }
                else
                {
                    check = false;
                }
            }
            return check;

        }
    }
}
