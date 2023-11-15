using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WHR.XML.Utility.SMEV
{
    internal class TestSMEV : BaseSMEV
    {
        public override string Address { get; set; } = "http://smev3-n0.test.gosuslugi.ru:7500/smev/v1.1/ws";

        public override string Request(ISMEV.SOAPOperation operation, string strRequest)
        {
            string _result = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Address);
            request.Method = "POST";
            request.ContentType = "text/xml;charset=UTF-8";
            request.Headers.Add("SOAPAction", ToString(operation));
            request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");
            request.KeepAlive = true;
            request.UserAgent = "WHR";
            request.Timeout *= 2;
            request.ReadWriteTimeout *= 2;
            using (StreamWriter _streamWriter = new StreamWriter(request.GetRequestStream()))
                _streamWriter.Write(strRequest);
            try
            {
                WebResponse _response = request.GetResponse();
                using (StreamReader _streamReader = new StreamReader(_response.GetResponseStream()))
                    _result = _streamReader.ReadToEnd();
            }
            catch (WebException ex)
            {
                return new StreamReader((ex.Response.GetResponseStream())).ReadToEnd();
                throw new AggregateException($"Запрос:{Environment.NewLine}{strRequest}{Environment.NewLine}Ответ:{Environment.NewLine}{new StreamReader((ex.Response.GetResponseStream())).ReadToEnd()}");
            }
            return _result;
        }
    }
}
