using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using WHR.Controls;

namespace WHR.XML.Utility.JCP
{
    internal class Signer
    {
        private string PathToTmpFiles = Path.Combine(Directory.GetCurrentDirectory(), "temp");
        public string SignedXml(string xml, string singName)
        {
            var arguments = $"-classpath \"C:\\jcpv20\\Signer.jar;C:\\jcpv20\\javadoc\\*\" Signer \"{xml.Replace("\"", "\\\"").Replace("\r", "").Replace("\n", "")}\" {singName}";
            var psi = new ProcessStartInfo("java", arguments)
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            using (var process = Process.Start(psi))
            {
                var output = process.StandardOutput.ReadToEnd();
                var error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (string.IsNullOrEmpty(output) && !string.IsNullOrEmpty(error))
                {
                    MetroMessageBox.Show($"Ошибка при подписании: {error}", "Упсс..", MessageBoxButton.OK, MessageBoxImage.Error);
                    return xml;
                }

                return output.Trim();
            }
        }

        private string SignedXmlToTMP(string xml, string singName)
        {
            var xmlFileName = Path.Combine(PathToTmpFiles, "tmp.xml");
            Directory.CreateDirectory(PathToTmpFiles);
            File.WriteAllText(xmlFileName, xml);

            var arguments = $"-classpath \"C:\\jcpv20\\Signer.jar;C:\\jcpv20\\javadoc\\*\" Signer \"{xmlFileName}\" \"{singName}\"";
            var psi = new ProcessStartInfo("java", arguments)
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            var output = new List<string>();
            var error = new List<string>();
            using (var result = new Process())
            {
                result.StartInfo = psi;
                result.ErrorDataReceived += (sender, e) => { error.Add(e.Data); };
                result.Start();
                result.BeginErrorReadLine();
                while (!result.StandardOutput.EndOfStream)
                {
                    output.Add(result.StandardOutput.ReadLine());
                }
                result.WaitForExit();
                result.CancelErrorRead();
            }
            if (output.Count == 0 && error.Count > 0)
            {
                MetroMessageBox.Show($"Ошибка при подписании: {string.Join(Environment.NewLine, error)}", "Упсс..", MessageBoxButton.OK, MessageBoxImage.Error);
                return xml;
            }
            File.Delete(xmlFileName);
            Directory.Delete(PathToTmpFiles, true);
            return output.FirstOrDefault();
        }
    }
}
