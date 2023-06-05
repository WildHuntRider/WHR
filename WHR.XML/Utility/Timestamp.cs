using System;

namespace WHR.XML.Utility
{
    internal class Timestamp
    {
        public static String GetTimestamp(DateTime value)
        {
            DateTimeOffset now = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Local);
            return now.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz");
        }

    }
}
