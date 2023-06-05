using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHR.XML.Utility
{
    internal class CMD
    {
        public static void Cmd(string line)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "cmd",
                Arguments = $"/c{line}",
                WindowStyle = ProcessWindowStyle.Hidden,
            });
        }
    }
}
