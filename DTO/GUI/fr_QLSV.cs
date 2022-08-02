using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wcf_SV;
using DTO;

namespace GUI
{
   
    public partial class fr_QLSV : Form
    {

        ServiceReference1.WCF_SVClient  cl ;
        SV_DTO dtsv  = new SV_DTO();


      

        public fr_QLSV()
        {
            InitializeComponent();
            cl = new ServiceReference1.WCF_SVClient();
        }

        private void fr_QLSV_Load(object sender, EventArgs e)
        {

            dataG_sv.DataSource = cl.getAllSV(); 

            /// sai ở lớp này không sử dụng lớp kết nối cơ sở dữ liệu 
            // gọi thông qua WCF 

            //Hà chú ý GUI gọi dịch vụ bên WCF để dùng , ko gọi trực tiếp DAO(lớp kết nối CSLD)
            //DTO là thứ mà tất cả GUI,WCF, DAO gọi để dùng

        }

        private void btnThem_Click(object sender, EventArgs e)
        {

            try
            {
                if (txt_ID.Text == "" && txtTen.Text == "" && txtGT.Text == "" && txtNamNH.Text == "")
                {
                    MessageBox.Show("Nhập đầy đủ thông tin");
                    return;
                }
                else
                {

                    SV_DTO sv = new SV_DTO(txt_ID.Text, txtTen.Text, txtGT.Text.Trim(), int.Parse(txtNamNH.Text.Trim()));

                    if (cl.themSV(sv))
                    {
                        MessageBox.Show("Thêm thành công");
                        dataG_sv.DataSource = cl.getAllSV();
                        reset();
                    }
                    else
                    {
                        MessageBox.Show("Thêm ko thành công");
                    }
                }

            }
            catch
            {
            }
            
            
        }

        private void btnSua_Click(object sender, EventArgs e)
        {

            try
            {
                if (txt_ID.Text != "" && txtTen.Text != "" && txtGT.Text != "" && txtNamNH.Text != "")
                {
                    SV_DTO sv = new SV_DTO(txt_ID.Text, txtTen.Text, txtGT.Text.Trim(), int.Parse(txtNamNH.Text.Trim()));

                    if (cl.suaSV(sv))
                    {
                        MessageBox.Show("Sửa thành công");
                        dataG_sv.DataSource = cl.getAllSV();
                        reset();
                    }
                    else
                    {
                        MessageBox.Show("Sửa ko thành công");
                    }
                }
                else
                {
                    MessageBox.Show("Hãy chọn vận chuyển cần sửa");
                }
            }
            catch
            {
                throw;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataG_sv.SelectedRows.Count < 0)
                {
                    MessageBox.Show("Chọn sinh viên mà bạn muốn xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                    string id = dataG_sv.SelectedRows[0].Cells[0].Value.ToString().Trim();
                    if (cl.xoaSV(id))
                    {
                        MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dataG_sv.DataSource = cl.getAllSV();
                        reset();
                    }
                    else
                    {
                        MessageBox.Show("Xóa không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
            }
            catch
            {
                throw;
            }
        }

        private void dataG_sv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = new DataGridViewRow();
                row = dataG_sv.Rows[e.RowIndex];
                txt_ID.Text = row.Cells[0].Value.ToString();
                txtTen.Text = row.Cells[1].Value.ToString();
                txtGT.Text = row.Cells[2].Value.ToString().Trim();
                txtNamNH.Text = row.Cells[3].Value.ToString();
            }
            catch
            {

            }
        }
        public void reset()
        {
            txt_ID.Text = "";
            txtTen.Text = "";
            txtGT.Text = "";
            txtNamNH.Text = "";
            txt_ID.Focus();
        }
        private void txtNamNH_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
