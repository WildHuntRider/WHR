using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHR.XML.Utility.SMEV
{
    public abstract class BaseSMEV : ISMEV
    {
        public abstract string Address { get; set; }
        public abstract string Request(ISMEV.SOAPOperation operation, string strRequest);
        protected string ToString(ISMEV.SOAPOperation operation)
        {
            switch (operation)
            {
                case ISMEV.SOAPOperation.GET_REQUEST:
                    return "urn:GetRequest";
                case ISMEV.SOAPOperation.GET_RESPONSE:
                    return "urn:GetResponse";
                case ISMEV.SOAPOperation.ACK:
                    return "urn:Ack";
                case ISMEV.SOAPOperation.SEND_RESPONSE:
                    return "urn:SendResponse";
                case ISMEV.SOAPOperation.SEND_REQUEST:
                    return "urn:SendRequest";
                default:
                    throw new Exception("Не указан код для данной операции.");
            }
        }
    }
}
