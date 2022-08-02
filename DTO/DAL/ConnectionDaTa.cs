using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ConnectionDaTa
    {
        protected SqlConnection Conn =new SqlConnection(@"Data Source=DESKTOP-QQCBI9B\SQLEXPRESS;Initial Catalog=QLSINHVIEN;Integrated Security=False;uid=sa;pwd=123");
    }
}
