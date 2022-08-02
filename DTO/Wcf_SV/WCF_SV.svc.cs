using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Wcf_SV
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WCF_SV" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WCF_SV.svc or WCF_SV.svc.cs at the Solution Explorer and start debugging.
    public class WCF_SV : IWCF_SV
    {

        SV_DAL sv = new SV_DAL();
        public List<SV_DTO> getAllSV()
        {
            return sv.getAllSV();
        }

        public bool themSV(SV_DTO svd)
        {
            return sv.themSV(svd);
        }

        public bool suaSV(SV_DTO svd)
        {
            return sv.suaSV(svd);
        }

        public bool xoaSV(string id)
        {
            return sv.xaoSV(id);
        }
    }
}
