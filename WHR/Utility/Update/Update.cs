using System;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Windows;
using WHR.Controls;
using WHR.XML.Utility;

namespace WHR.Utility.Update
{
    public class Update
    {
        public void CheckAndUpdateVersionWHRXML(string exename, string exepath, string curver)
        {
            using (WebClient client = new WebClient())
            {
                if (Internet.OK())
                {
                    client.Headers["User-Agent"] = "Mozilla/5.0";
                    string readver = client.DownloadString("https://api.github.com/repos/WildHuntRider/WHR/releases/latest");
                    Match match = Regex.Match(readver, "\"tag_name\":\"(.*?)\"");
                    string pattern = $"https://github.com/WildHuntRider/WHR/releases/download/{match.Groups[1].Value}/WHR.XML.exe";

                    if (Convert.ToDouble(curver, CultureInfo.InvariantCulture) < Convert.ToDouble(match.Groups[1].Value, CultureInfo.InvariantCulture))
                    {
                        if (MetroMessageBox.Show("Доступна новая версия. Обновить?", "Информация", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) != MessageBoxResult.Yes)
                            return;
                        {
                            client.DownloadFile($"{pattern}", "new.exe");
                            CMD.Cmd($"taskkill /f /im \"{exename}\" && timeout /t 1 && del \"{exepath}\" && ren new.exe \"{exename}\" && \"{exepath}\"");
                        }
                    }
                }
                else
                {
                    MetroMessageBox.Show("Нет доступа в сеть", "Упсс..", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }
    }
}
