using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Script.Services;

namespace Wcf_SV
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWCF_SV" in both code and config file together.
    [ServiceContract]

    public interface IWCF_SV
    {
        [OperationContract]
        [WebGet(UriTemplate = "getAllSV_s", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        List<SV_DTO> getAllSV();

        [OperationContract]
        [WebInvoke(UriTemplate = "themSV_s", Method = "POST", RequestFormat = WebMessageFormat.Json)]
        bool themSV(SV_DTO sv);

        [OperationContract]
        [WebInvoke(UriTemplate = "suaSV_s", Method = "PUT")]
        bool suaSV(SV_DTO sv);

        [OperationContract]
        [WebInvoke(UriTemplate = "xoaSV_s", Method = "DELETE")]
        bool xoaSV(string  id);

    }

    //[DataContract]
    //public class SINH_VIEN
    //{
    //    [DataMember]
    //    public string MaSV { get; set; }
    //    [DataMember]
    //    public string TenSV { get; set; }
    //    [DataMember]
    //    public string GioiTinh { get; set; }
    //    [DataMember]
    //    public int NamNhapHoc { get; set; }
    //}
}
