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
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Xml;

namespace GUI
{
   
    public partial class fr_QLSV : Form
    {
        string getSV = "http://localhost:63649/WCF_SV.svc/getAllSV_s";
        string themSV = "http://localhost:63649/WCF_SV.svc/themSV_s";
        string suasv = "http://localhost:63649/WCF_SV.svc/suaSV_s";
        string xoasv = "http://localhost:63649/WCF_SV.svc/xoaSV_s";


        ServiceReference1.WCF_SVClient  cl ;
        SV_DTO dtsv  = new SV_DTO();

        public void RSV() { 
        WebClient proxy = new WebClient();
        proxy.DownloadStringAsync(new Uri(getSV));  
            proxy.DownloadStringCompleted +=proxy_DownloadStringCompleted;  
        }

    void proxy_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
     {
        Stream stream = new MemoryStream(Encoding.Unicode.GetBytes(e.Result));
        DataContractJsonSerializer obj = new DataContractJsonSerializer(typeof(List<SV_DTO>));
        List<SV_DTO> result = obj.ReadObject(stream) as List<SV_DTO>;
            dataG_sv.DataSource = result;
    }

      public fr_QLSV()
        {
            InitializeComponent();
            cl = new ServiceReference1.WCF_SVClient();
        }

        //public void docTT_XML()
        //{
        //    XmlDocument read = new XmlDocument();
        //    read.Load("D:\\QL_SVien_WCF\\DTO\\GUI\\config.xml");
        //    XmlNodeList nodelist = read.SelectNodes("/Server/data");
        //    foreach(XmlNode node in nodelist)
        //    {
        //        string link = node["link"].InnerText;
        //    }

        //}
        private void fr_QLSV_Load(object sender, EventArgs e)
        {
            RSV();
            //dataG_sv.DataSource = cl.getAllSV(); 


            /// sai ở lớp này không sử dụng lớp kết nối cơ sở dữ liệu 
            // gọi thông qua WCF 

            //Hà chú ý GUI gọi dịch vụ bên WCF để dùng , ko gọi trực tiếp DAO(lớp kết nối CSLD)
            //DTO là thứ mà tất cả GUI,WCF, DAO gọi để dùng

        }

        private void btnThem_Click(object sender, EventArgs e)
        {

            //try
            //{
            //    if (txt_ID.Text == "" && txtTen.Text == "" && txtGT.Text == "" && txtNamNH.Text == "")
            //    {
            //        MessageBox.Show("Nhập đầy đủ thông tin");
            //        return;
            //    }
            //    else
            //    {

                    SV_DTO sv = new SV_DTO(txt_ID.Text, txtTen.Text, txtGT.Text.Trim(), int.Parse(txtNamNH.Text.Trim()));

                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(SV_DTO));
                    MemoryStream mem = new MemoryStream();
                    ser.WriteObject(mem, sv);
                    string data = Encoding.Default.GetString(mem.ToArray(), 0, (int)mem.Length);
                    WebClient webClient = new WebClient();
                    webClient.Headers["Content-type"] = "application/json";
                    webClient.Encoding = Encoding.Default;
                    webClient.UploadString((themSV), "POST", data);
                    MessageBox.Show("Thêm thành công");
                    reset();
                    RSV();
                  
            //    }

            //}
            //catch
            //{
            //}
            
            
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            SV_DTO sv = new SV_DTO(txt_ID.Text, txtTen.Text, txtGT.Text.Trim(), int.Parse(txtNamNH.Text.Trim()));

            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(SV_DTO));
            ser.WriteObject(ms, sv);
            client.UploadData((suasv), "PUT", ms.ToArray());

            MessageBox.Show("Sua TT SV thanh cong", "Thông báo");
            reset();
            RSV();

            //try
            //{
            //    if (txt_ID.Text != "" && txtTen.Text != "" && txtGT.Text != "" && txtNamNH.Text != "")
            //    {
            //        SV_DTO sv = new SV_DTO(txt_ID.Text, txtTen.Text, txtGT.Text.Trim(), int.Parse(txtNamNH.Text.Trim()));

            //        if (cl.suaSV(sv))
            //        {
            //            MessageBox.Show("Sửa thành công");
            //            dataG_sv.DataSource = cl.getAllSV();
            //            reset();
            //        }
            //        else
            //        {
            //            MessageBox.Show("Sửa ko thành công");
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("Hãy chọn vận chuyển cần sửa");
            //    }
            //}
            //catch
            //{
            //    throw;
            //}
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {


            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            string id = dataG_sv.SelectedRows[0].Cells[0].Value.ToString().Trim();
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(SV_DTO));
            ser.WriteObject(ms, id);
            client.UploadData((xoasv), "DELETE", ms.ToArray());

            MessageBox.Show("xoa TT SV thanh cong", "Thông báo");
            reset();
            RSV();
            //try
            //{
            //    if (dataG_sv.SelectedRows.Count < 0)
            //    {
            //        MessageBox.Show("Chọn sinh viên mà bạn muốn xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return;
            //    }

            //        string id = dataG_sv.SelectedRows[0].Cells[0].Value.ToString().Trim();
            //        if (cl.xoaSV(id))
            //        {
            //            MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            dataG_sv.DataSource = cl.getAllSV();
            //            reset();
            //        }
            //        else
            //        {
            //            MessageBox.Show("Xóa không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        }
            //}
            //catch
            //{
            //    throw;
            //}
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

        private void dataG_sv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
