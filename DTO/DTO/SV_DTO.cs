using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    [DataContract]
    public class SV_DTO
    {
        [DataMember]
        private string maSV;
        [DataMember]
        private string tenSV;
        [DataMember]
        private string gioiTinh;
        [DataMember]
        private int namNhapHoc;

        public string MaSV { get => maSV; set => maSV = value; }
        public string TenSV { get => tenSV; set => tenSV = value; }
        public string GioiTinh { get => gioiTinh; set => gioiTinh = value; }
        public int NamNhapHoc { get => namNhapHoc; set => namNhapHoc = value; }

        public SV_DTO()
        {

        }
        public SV_DTO(string MaSV, string TenSV, string GioiTinh, int NamNhapHoc)
        {
            maSV = MaSV;
            tenSV = TenSV;
            gioiTinh = GioiTinh;
            namNhapHoc = NamNhapHoc;
        }

      
    }
}
