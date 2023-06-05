using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WHR.XML.Utility.SMEV
{
    internal class ProdSMEV : BaseSMEV
    {
        public override string Address { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override string Request(ISMEV.SOAPOperation operation, string strRequest)
        {
            Regex regex = new Regex("\"");
            strRequest = regex.Replace(strRequest, "\\\"");
            string json = "{\"soapUrl\":\"http://172.20.3.12:7500/smev/v1.1/ws\",\"soapAction\":\"\",\"soapContent\":\"" + strRequest + "\"}";
            string postData = $"{json}";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            WebRequest request = WebRequest.Create("http://192.168.120.80:55116/adapter-web/rest/admin/soaprequest");
            request.Method = "POST";
            request.ContentType = "application/json; charset=UTF-8";
            request.ContentLength = byteArray.Length;

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse response = request.GetResponse();
            using (dataStream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(dataStream))
                    return reader.ReadToEnd();
            }
        }
    }
}
