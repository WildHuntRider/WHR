using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WHR.Utility.Update
{
    public static class UpdateJava
    {
        public static Cookie cookie = new Cookie("bm_mi", "", "/", ".java.com");
        public const string uriJava = "https://www.java.com/content/published/api/v1.1/items?q=id%20eq%20%22COREFA37E773006D4BE183DB8D7F504C5718%22&channelToken=1f7d2611846d4457b213dfc9048724dc";

        public static (string, string) GetPageContent()
        {
            string java86 = string.Empty;
            string java64 = string.Empty;

            Task.Run(() =>
            {
                try
                {
                    HttpWebRequest http = WebRequest.CreateHttp(uriJava);
                    CookieContainer cookieContainer = new CookieContainer();
                    cookieContainer.Add(cookie);
                    http.CookieContainer = cookieContainer;

                    using (HttpWebResponse webResponse = (HttpWebResponse)http.GetResponseAsync().GetAwaiter().GetResult())
                    {
                        using (Stream stream = webResponse.GetResponseStream())
                        {
                            using (StreamReader streamReader = new StreamReader(stream))
                            {
                                string content = streamReader.ReadToEndAsync().GetAwaiter().GetResult();

                                Match java = Regex.Match(content, "secID\":\"(.*?)\"(.*?)\"win_offline_bundle\":\"(.*?)\",\"win_x64_bundle\":\"(.*?)\"");

                                java86 = $"https://javadl.oracle.com/webapps/download/AutoDL?BundleId={java.Groups[3].Value}_{java.Groups[1].Value}";
                                java64 = $"https://javadl.oracle.com/webapps/download/AutoDL?BundleId={java.Groups[4].Value}_{java.Groups[1].Value}";
                            }
                        }
                    }
                }
                catch
                {
                    // Обработка ошибок
                }
            }).GetAwaiter().GetResult();

            return (java86, java64);
        }
    }
}

