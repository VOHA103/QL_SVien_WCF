using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAL
{
   public class SV_DAL:ConnectionDaTa
    {
        public List<T> ConvertToList<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>()
                    .Select(c => c.ColumnName)
                    .ToList();
            var properties = typeof(T).GetProperties();
            return dt.AsEnumerable().Select(row =>
            {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name))
                    {
                        PropertyInfo pI = objT.GetType().GetProperty(pro.Name);
                        pro.SetValue(objT, row[pro.Name] == DBNull.Value ? null : Convert.ChangeType(row[pro.Name], pI.PropertyType));
                    }
                }
                return objT;
            }).ToList();
        }
        public List<SV_DTO> getAllSV()
        {
            if (Conn.State == ConnectionState.Closed)
                Conn.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM SINH_VIEN", Conn);
            DataTable qlSV = new DataTable();
            da.Fill(qlSV);
            Conn.Close();
         
            return ConvertToList<SV_DTO>(qlSV);
        }
        public bool themSV(SV_DTO sv)
        {
            try
            {
                Conn.Open();
                string sql = string.Format("INSERT INTO SINH_VIEN(TenSV, GioiTinh, NamNhapHoc,MaSV) VALUES ('N{0}', 'N{1}', '{2}','{3}')", sv.TenSV, sv.GioiTinh, sv.NamNhapHoc, sv.MaSV);

                SqlCommand cmd = new SqlCommand(sql, Conn);

                if (cmd.ExecuteNonQuery() > 0)
                    return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("lỗi hoặc trùng khóa chính", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            return false;
        }

        public bool suaSV(SV_DTO sv)
        {
            try
            {
                Conn.Open();

                string sql = string.Format("UPDATE SINH_VIEN SET TenSV=N'{0}',GioiTinh=N'{1}',NamNhapHoc='{2}' WHERE MaSV = '{3}'", sv.TenSV, sv.GioiTinh, sv.NamNhapHoc, sv.MaSV);

                SqlCommand cmd = new SqlCommand(sql, Conn);

                if (cmd.ExecuteNonQuery() > 0)
                    return true;

            }
            catch (Exception e)
            {
                throw;

            }
            finally
            {
                Conn.Close();
            }

            return false;
        }
        public bool xaoSV(string SV_ID)
        {
            try
            {
                Conn.Open();

                string SQL = string.Format("DELETE FROM SINH_VIEN WHERE MaSV = '{0}'", SV_ID);

                SqlCommand cmd = new SqlCommand(SQL, Conn);

                if (cmd.ExecuteNonQuery() > 0)
                    return true;

            }
            catch (Exception e)
            {
                throw;

            }
            finally
            {
                Conn.Close();
            }

            return false;
        }
    }
}
