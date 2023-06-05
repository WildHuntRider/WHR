using System;
using System.Text;

namespace WHR.XML.Utility
{
    public class WorkWithException
    {
        public static string GetFullStack(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(ex.Message);
            sb.Append(Environment.NewLine);
            sb.Append(ex.StackTrace);
            sb.Append(Environment.NewLine);
            var innEx = ex.InnerException;
            while (innEx != null)
            {
                sb.Append(innEx.Message);
                sb.Append(Environment.NewLine);
                sb.Append(innEx.StackTrace);
                sb.Append(Environment.NewLine);
                innEx = innEx.InnerException;
            }
            return sb.ToString();
        }
        public static string GetAllMessages(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(ex.Message);
            sb.Append(Environment.NewLine);
            var innEx = ex.InnerException;
            while (innEx != null)
            {
                sb.Append(innEx.Message);
                sb.Append(Environment.NewLine);
                innEx = innEx.InnerException;
            }
            return sb.ToString();
        }
    }
}
