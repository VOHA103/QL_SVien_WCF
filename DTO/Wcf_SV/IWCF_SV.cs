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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWCF_SV" in both code and config file together.
    [ServiceContract]

    public interface IWCF_SV
    {
        [OperationContract]

        List<SV_DTO> getAllSV();
        [OperationContract]
        bool themSV(SV_DTO sv);
        [OperationContract]
        bool suaSV(SV_DTO sv);
        [OperationContract]
        bool xoaSV(string  id);

    }
}
