using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHR.XML.Utility.SMEV
{
    public interface ISMEV
    {
        public enum SOAPOperation { GET_REQUEST, GET_RESPONSE, ACK, SEND_RESPONSE, SEND_REQUEST }
        string Address { get; set; }
        string Request(SOAPOperation operation, string strRequest);
    }
}
