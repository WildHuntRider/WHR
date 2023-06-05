using System.Collections.Generic;
using System.Diagnostics;

namespace WHR.XML.Utility.JCP
{
    internal class PhDGetter
    {
        public List<string> GetPHDs()
        {
            var resultNames = new List<string>();
            var arguments = $"-classpath  \"C:\\jcpv20\\Signer.jar;C:\\jcpv20\\javadoc\\*\" Signer -phd";
            var psi = new ProcessStartInfo("java", arguments)
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };
            using (var process = Process.Start(psi))
            {
                if (process != null)
                {
                    while (!process.StandardOutput.EndOfStream)
                    {
                        var line = process.StandardOutput.ReadLine();
                        resultNames.Add(line);
                    }
                    process.WaitForExit();
                }
            }
            return resultNames;
        }
    }
}