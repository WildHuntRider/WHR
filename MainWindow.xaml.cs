using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;
using System.Xml.Linq;
using System.Windows.Controls.Primitives;
using WHR.Controls;
using WHR.Utility.Update;
using WHR.XML.Utility;
using WHR.XML.Utility.JCP;
using WHR.XML.Utility.SMEV;
using static WHR.XML.Utility.GuidGenerator;
using static WHR.XML.Utility.Timestamp;

namespace WHR.XML
{
    public partial class MainWindow : MetroWindow
    {
        string GetRequestRequest = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">\n  <soap:Header/>\n  <soap:Body>\n    <ns1:GetRequestRequest xmlns:ns1=\"urn://x-artefacts-smev-gov-ru/services/message-exchange/types/1.1\" xmlns:ns2=\"urn://x-artefacts-smev-gov-ru/services/message-exchange/types/basic/1.1\">\n      <ns2:MessageTypeSelector Id=\"MainContent\">\n        <ns2:NamespaceURI></ns2:NamespaceURI>\n        <ns2:RootElementLocalName></ns2:RootElementLocalName>\n        <ns2:Timestamp></ns2:Timestamp>\n      </ns2:MessageTypeSelector>\n      <ns1:CallerInformationSystemSignature>\n        <ds:Signature xmlns:ds=\"http://www.w3.org/2000/09/xmldsig#\">\n          <ds:SignedInfo>\n            <ds:CanonicalizationMethod Algorithm=\"http://www.w3.org/2001/10/xml-exc-c14n#\"/>\n            <ds:SignatureMethod Algorithm=\"urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr34102012-gostr34112012-256\"/>\n            <ds:Reference URI=\"#MainContent\">\n              <ds:Transforms>\n                <ds:Transform Algorithm=\"http://www.w3.org/2001/10/xml-exc-c14n#\"/>\n                <ds:Transform Algorithm=\"urn://smev-gov-ru/xmldsig/transform\"/>\n              </ds:Transforms>\n              <ds:DigestMethod Algorithm=\"urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr34112012-256\"/>\n              <ds:DigestValue/>\n            </ds:Reference>\n          </ds:SignedInfo>\n          <ds:SignatureValue/>\n          <ds:KeyInfo>\n            <ds:X509Data>\n              <ds:X509Certificate/>\n            </ds:X509Data>\n          </ds:KeyInfo>\n        </ds:Signature>\n      </ns1:CallerInformationSystemSignature>\n    </ns1:GetRequestRequest>\n  </soap:Body>\n</soap:Envelope>";
        string AckRequest = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">\n  <soap:Header/>\n  <soap:Body>\n    <ns1:AckRequest xmlns:ns1=\"urn://x-artefacts-smev-gov-ru/services/message-exchange/types/1.1\" xmlns:ns2=\"urn://x-artefacts-smev-gov-ru/services/message-exchange/types/basic/1.1\">\n      <ns2:AckTargetMessage Id=\"MainContent\" accepted=\"true\"></ns2:AckTargetMessage>\n      <ns1:CallerInformationSystemSignature>\n        <ds:Signature xmlns:ds=\"http://www.w3.org/2000/09/xmldsig#\">\n          <ds:SignedInfo>\n            <ds:CanonicalizationMethod Algorithm=\"http://www.w3.org/2001/10/xml-exc-c14n#\"/>\n            <ds:SignatureMethod Algorithm=\"urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr34102012-gostr34112012-256\"/>\n            <ds:Reference URI=\"#MainContent\">\n              <ds:Transforms>\n                <ds:Transform Algorithm=\"http://www.w3.org/2001/10/xml-exc-c14n#\"/>\n                <ds:Transform Algorithm=\"urn://smev-gov-ru/xmldsig/transform\"/>\n              </ds:Transforms>\n              <ds:DigestMethod Algorithm=\"urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr34112012-256\"/>\n              <ds:DigestValue/>\n            </ds:Reference>\n          </ds:SignedInfo>\n          <ds:SignatureValue/>\n          <ds:KeyInfo>\n            <ds:X509Data>\n              <ds:X509Certificate/>\n            </ds:X509Data>\n          </ds:KeyInfo>\n        </ds:Signature>\n      </ns1:CallerInformationSystemSignature>\n    </ns1:AckRequest>\n  </soap:Body>\n</soap:Envelope>";
        string SendResponseRequest = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">\n  <soap:Header/>\n  <soap:Body>\n    <ns1:SendResponseRequest xmlns:ns1=\"urn://x-artefacts-smev-gov-ru/services/message-exchange/types/1.1\">\n      <ns1:SenderProvidedResponseData Id=\"MainContent\">\n        <ns1:MessageID></ns1:MessageID>\n        <ns1:To></ns1:To>\n        <ns2:MessagePrimaryContent xmlns:ns2=\"urn://x-artefacts-smev-gov-ru/services/message-exchange/types/basic/1.1\">\n\t</ns2:MessagePrimaryContent>\n      </ns1:SenderProvidedResponseData>\n      <ns1:CallerInformationSystemSignature>\n        <ds:Signature xmlns:ds=\"http://www.w3.org/2000/09/xmldsig#\">\n          <ds:SignedInfo>\n            <ds:CanonicalizationMethod Algorithm=\"http://www.w3.org/2001/10/xml-exc-c14n#\"/>\n            <ds:SignatureMethod Algorithm=\"urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr34102012-gostr34112012-256\"/>\n            <ds:Reference URI=\"#MainContent\">\n              <ds:Transforms>\n                <ds:Transform Algorithm=\"http://www.w3.org/2001/10/xml-exc-c14n#\"/>\n                <ds:Transform Algorithm=\"urn://smev-gov-ru/xmldsig/transform\"/>\n              </ds:Transforms>\n              <ds:DigestMethod Algorithm=\"urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr34112012-256\"/>\n              <ds:DigestValue/>\n            </ds:Reference>\n          </ds:SignedInfo>\n          <ds:SignatureValue/>\n          <ds:KeyInfo>\n            <ds:X509Data>\n              <ds:X509Certificate/>\n            </ds:X509Data>\n          </ds:KeyInfo>\n        </ds:Signature>\n      </ns1:CallerInformationSystemSignature>\n    </ns1:SendResponseRequest>\n  </soap:Body>\n</soap:Envelope>";
        string SendRequestRequest = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">\n  <soap:Header/>\n  <soap:Body>\n    <ns1:SendRequestRequest xmlns:ns1=\"urn://x-artefacts-smev-gov-ru/services/message-exchange/types/1.1\">\n      <ns1:SenderProvidedRequestData Id=\"MainContent\">\n        <ns1:MessageID></ns1:MessageID>\n        <ns2:MessagePrimaryContent xmlns:ns2=\"urn://x-artefacts-smev-gov-ru/services/message-exchange/types/basic/1.1\">\n\t</ns2:MessagePrimaryContent>\n      </ns1:SenderProvidedRequestData>\n      <ns1:CallerInformationSystemSignature>\n        <ds:Signature xmlns:ds=\"http://www.w3.org/2000/09/xmldsig#\">\n          <ds:SignedInfo>\n            <ds:CanonicalizationMethod Algorithm=\"http://www.w3.org/2001/10/xml-exc-c14n#\"/>\n            <ds:SignatureMethod Algorithm=\"urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr34102012-gostr34112012-256\"/>\n            <ds:Reference URI=\"#MainContent\">\n              <ds:Transforms>\n                <ds:Transform Algorithm=\"http://www.w3.org/2001/10/xml-exc-c14n#\"/>\n                <ds:Transform Algorithm=\"urn://smev-gov-ru/xmldsig/transform\"/>\n              </ds:Transforms>\n              <ds:DigestMethod Algorithm=\"urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr34112012-256\"/>\n              <ds:DigestValue/>\n            </ds:Reference>\n          </ds:SignedInfo>\n          <ds:SignatureValue/>\n          <ds:KeyInfo>\n            <ds:X509Data>\n              <ds:X509Certificate/>\n            </ds:X509Data>\n          </ds:KeyInfo>\n        </ds:Signature>\n      </ns1:CallerInformationSystemSignature>\n    </ns1:SendRequestRequest>\n  </soap:Body>\n</soap:Envelope>";
        string GetResponseRequest = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">\n  <soap:Header />\n  <soap:Body>\n    <ns1:GetResponseRequest xmlns:ns1=\"urn://x-artefacts-smev-gov-ru/services/message-exchange/types/1.1\" xmlns:ns2=\"urn://x-artefacts-smev-gov-ru/services/message-exchange/types/basic/1.1\">\n      <ns2:MessageTypeSelector Id=\"MainContent\">\n        <ns2:NamespaceURI></ns2:NamespaceURI>\n        <ns2:RootElementLocalName></ns2:RootElementLocalName>\n        <ns2:Timestamp></ns2:Timestamp>\n      </ns2:MessageTypeSelector>\n      <ns1:CallerInformationSystemSignature>\n        <ds:Signature xmlns:ds=\"http://www.w3.org/2000/09/xmldsig#\">\n          <ds:SignedInfo>\n            <ds:CanonicalizationMethod Algorithm=\"http://www.w3.org/2001/10/xml-exc-c14n#\" />\n            <ds:SignatureMethod Algorithm=\"urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr34102012-gostr34112012-256\" />\n            <ds:Reference URI=\"#MainContent\">\n              <ds:Transforms>\n                <ds:Transform Algorithm=\"http://www.w3.org/2001/10/xml-exc-c14n#\" />\n                <ds:Transform Algorithm=\"urn://smev-gov-ru/xmldsig/transform\" />\n              </ds:Transforms>\n              <ds:DigestMethod Algorithm=\"urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr34112012-256\" />\n              <ds:DigestValue>\n              </ds:DigestValue>\n            </ds:Reference>\n          </ds:SignedInfo>\n          <ds:SignatureValue>\n          </ds:SignatureValue>\n          <ds:KeyInfo>\n            <ds:X509Data>\n              <ds:X509Certificate/>\n            </ds:X509Data>\n          </ds:KeyInfo>\n        </ds:Signature>\n      </ns1:CallerInformationSystemSignature>\n    </ns1:GetResponseRequest>\n  </soap:Body>\n</soap:Envelope>";
        string curver = Assembly.GetExecutingAssembly().GetName().Version.ToString(2);
        string exename = AppDomain.CurrentDomain.FriendlyName;
        string exepath = Assembly.GetEntryAssembly().Location;
        string currentPath = Directory.GetCurrentDirectory();
        string pathToEtalons = string.Empty;
        bool _save = false;
        bool _checkMessage = true;
        Action callback = null;
        PhDGetter phdGetter = new PhDGetter();//Получение подписи из JCP
        Signer signer = new Signer();//Получение подписи из JCP
        public MainWindow()
        {
            InitializeComponent();
            Directory.SetCurrentDirectory(AppContext.BaseDirectory);
            pathToEtalons = Path.Combine($@"{currentPath}\Эталонные сообщения\");
            textBox5.Text = GetRequestRequest;
            textBox7.Text = AckRequest;
            textBox9.Text = SendResponseRequest;
            textBox11.Text = SendRequestRequest;
            textBox13.Text = GetResponseRequest;
            textBox15.Text = AckRequest;
        }
        private void MetroWindow_Initialized(object sender, EventArgs e)
        {
            List<string> phdNames = new List<string>(); Task.Run(() => { phdGetter.GetPHDs().ForEach(Action => phdNames.Add(Action)); }).ContinueWith(Action => { phdNames.ForEach(a => SignsCb.Items.Add(a)); phdNames.ForEach(a => SignsCb1.Items.Add(a)); }, TaskScheduler.FromCurrentSynchronizationContext());
            var (java86, java64) = UpdateJava.GetPageContent();
            MTBBB2.Text = java86;
            MTBBB3.Text = java64;
        }

        private void MetroWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(((char)e.Key) == (char)Key.F1)
            {

            }
            if (((char)e.Key) == (char)Key.F5)
            {
                    SignsCb1.Items.Clear();
                    SignsCb1.SelectedIndex = 0;
                    SignsCb.Items.Clear();
                    SignsCb.SelectedIndex = 0;
                    List<string> phdNames = new List<string>(); Task.Run(() => { phdGetter.GetPHDs().ForEach(Action => phdNames.Add(Action)); }).ContinueWith(Action => { phdNames.ForEach(a => SignsCb.Items.Add(a)); phdNames.ForEach(a => SignsCb1.Items.Add(a)); }, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }
        void ProcessXml(TextBox input, TextBox output, ISMEV.SOAPOperation operation)
        {
            string signedXml = input.Text;
            string phd = SignsCb.SelectionBoxItem.ToString();
            Task.Run(() =>
            {
                signedXml = signer.SignedXml(signedXml, phd);
                input.Dispatcher.Invoke(() =>
                {
                    input.Text = signedXml;
                });
            }).ContinueWith(_ =>
            {
                string response = new TestSMEV().Request(operation, signedXml);
                output.Dispatcher.Invoke(() =>
                {
                    output.Text = ExtractXMLWithEnd(response);
                });
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
        #region "Update-Copy-Paste"
        public void MetroTitleMenuItem_Click(object s, RoutedEventArgs e)
        {
            Update versionUpdater = new Update();
            versionUpdater.CheckAndUpdateVersionWHRXML(exename, exepath, curver);
        }
        private void MMI1_Click(object sender, RoutedEventArgs e)
        {
            UUID windowA = new UUID();
            windowA.Show();
        }
        void MetroMenuCopy_Click(object s, RoutedEventArgs e)
        {
            if (s is MetroMenuItem menuItem && menuItem.Parent is ContextMenu contextMenu)
            {
                if (contextMenu.PlacementTarget is MetroTextBox textBox)
                {
                    textBox.SelectAll();
                    textBox.Copy();
                }
            }
        }
        void MetroMenuPaste_Click(object s, RoutedEventArgs e)
        {
            if (s is MetroMenuItem menuItem && menuItem.Parent is ContextMenu contextMenu)
            {
                if (contextMenu.PlacementTarget is MetroTextBox textBox)
                {
                    textBox.Paste();
                }
            }
        }
        #endregion
        #region "Parsing"
        public void parsing_Click(object s, RoutedEventArgs e)
        {
            if (etalonsDownload.IsChecked != true || string.IsNullOrEmpty(textBox1.Text))
            {
                MetroMessageBox.Show("Следует внести данные пред парсингом", "Парсинг данных", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            string line = textBox1.Text;
            Match matches = Regex.Match(line, "(.*?)/inquiries/(.*?)/versions/(.*?)\\?area=(.*)");
            string json = $"{{\"content\":{{\"inquiryVersionId\":\"{matches.Groups[3].Value}\",\"area\":\"{matches.Groups[4].Value}\"}}}}";

            WebRequest request = WebRequest.Create("https://lkuv.gosuslugi.ru/api/inquiry/public/v1/inquiry/card/version");
            request.Method = "POST";
            byte[] byteArray = Encoding.UTF8.GetBytes(json);
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }

            WebResponse response = request.GetResponse();
            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                Match match = Regex.Match(responseFromServer, "(.*?)\"smevTestId\":(.*?),\"(.*?)");
                if (string.IsNullOrEmpty(match.Groups[2].Value))
                {
                    MetroMessageBox.Show("Данный ВС не имеет эталонов", "Парсинг данных", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                using (WebClient Client = new WebClient())
                {
                    string currentPath = Directory.GetCurrentDirectory();
                    DownloadFile($"https://lkuv.gosuslugi.ru/api/inquiry/public/v1/inquiries/versions/{matches.Groups[3].Value}/file/ETALONS", match, Client, currentPath);
                    SpecialparsingeEtalon(match, currentPath);
                }
            }
        }
        #endregion
        #region "SaveFile"
        void btnSaveFile_Click(object s, RoutedEventArgs e)
        {
            string nameFolder, directory, zipPath;
            string[] filenames, tbshka;
            Match match;

            if (testPotrebitel.IsChecked == false)
            {
                nameFolder = textBox6.Text;
                filenames = new string[] { "1)GetRequestRequest.xml", "1)GetResponseRequest.xml", "2)AсkRequest.xml", "2)AсkResponse.xml", "3)SendResponseRequest.xml", "3)SendResponseResponse.xml" };
                tbshka = new string[] { textBox5.Text, textBox6.Text, textBox7.Text, textBox8.Text, textBox9.Text, textBox10.Text };
            }
            else
            {
                nameFolder = textBox11.Text;
                filenames = new string[] { "1)SendRequestRequest.xml", "1)SendRequestResponse.xml", "2)GetResponseRequest.xml", "2)GetResponseResponse.xml", "3)AckRequest.xml", "3)AckResponse.xml" };
                tbshka = new string[] { textBox11.Text, textBox12.Text, textBox13.Text, textBox14.Text, textBox15.Text, textBox16.Text };
            }

            match = Regex.Match(nameFolder, "<(.*?):MessageID>(.*?)</(.*?):MessageID>");
            directory = $@"{currentPath}\Сохраненные запросы\{match.Groups[2].Value}";
            Directory.CreateDirectory(directory);

            for (int F = 0, T = 0; F < filenames.Length && T < tbshka.Length; F++, T++)
            {
                string filePath = $@"{directory}\{filenames[F]}";
                using (StreamWriter stream = new StreamWriter(filePath))
                    stream.Write(tbshka[T]);
            }

            zipPath = string.IsNullOrEmpty(match.Groups[2].Value) ? @"Result.zip" : @$"{match.Groups[2].Value}.zip";
            if (File.Exists(zipPath))
            {
                File.Delete(zipPath);
            }
            ZipFile.CreateFromDirectory(directory, zipPath);

            ResetTextBoxes(testPotrebitel.IsChecked == false);
        }

        void ResetTextBoxes(bool testPotrebitelChecked)
        {
            exist_request.Text = "";

            if (testPotrebitelChecked)
            {
                textBox5.Text = GetRequestRequest;
                textBox6.Text = "";
                textBox7.Text = AckRequest;
                textBox8.Text = "";
                textBox9.Text = SendResponseRequest;
                textBox10.Text = "";
                _save = false;
            }
            else
            {
                textBox11.Text = SendRequestRequest;
                textBox12.Text = "";
                textBox13.Text = GetResponseRequest;
                textBox14.Text = "";
                textBox15.Text = AckRequest;
                textBox16.Text = "";
                _save = true;
            }
        }
        #endregion
        void btnPodpis_Click(object s, RoutedEventArgs e)
        {
            textBox6.Text = "";
            if (string.IsNullOrEmpty(textBox2.Text) && string.IsNullOrEmpty(textBox3.Text))
            {
                MetroMessageBox.Show("Следует внести данные перед подписанием", "Упсс..", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            textBox4.Text = GetTimestamp(DateTime.Now);
            if (testPotrebitel.IsChecked == false)
            {
                textBox5.Text = Regex.Replace(textBox5.Text, @"(?<=<ns2:NamespaceURI>)(.*)(?=</ns2:NamespaceURI>)", textBox2.Text);
                textBox5.Text = Regex.Replace(textBox5.Text, @"(?<=<ns2:RootElementLocalName>)(.*)(?=</ns2:RootElementLocalName>)", textBox3.Text);
                textBox5.Text = Regex.Replace(textBox5.Text, @"(?<=<ns2:Timestamp>)(.*)(?=</ns2:Timestamp>)", GetTimestamp(DateTime.Now));

                if (testPotrebitel.IsChecked == false)
                {
                    try
                    {
                        _checkMessage = true;
                        ProcessXml(textBox5, textBox6, ISMEV.SOAPOperation.GET_REQUEST);

                        // _checkMessage = Replace(textBox6.Text).Contains("messageprimarycontent") && !textBox6.Text.Contains("<faultcode>");

                        callback = () =>
                        {
                            try
                            {
                                if (_checkMessage)
                                {
                                    ProcessXml(textBox7, textBox8, ISMEV.SOAPOperation.ACK);
                                    ProcessXml(textBox9, textBox10, ISMEV.SOAPOperation.SEND_RESPONSE);
                                }
                            }
                            catch (Exception ex)
                            {
                                HandleException(ex);
                            }
                        };
                    }
                    catch (Exception ex)
                    {
                        HandleException(ex);
                    }
                }
            }
            if (testPotrebitel.IsChecked == true)
            {
                textBox11.Text = Regex.Replace(textBox11.Text, @"(?<=<ns1:MessageID>)(.*)(?=</ns1:MessageID>)", GenerateTimeBasedGuid().ToString());
                textBox13.Text = Regex.Replace(textBox13.Text, @"(?<=<ns2:NamespaceURI>)(.*)(?=</ns2:NamespaceURI>)", textBox2.Text);
                textBox13.Text = Regex.Replace(textBox13.Text, @"(?<=<ns2:RootElementLocalName>)(.*)(?=</ns2:RootElementLocalName>)", textBox3.Text);
                textBox13.Text = Regex.Replace(textBox13.Text, @"(?<=<ns2:Timestamp>)(.*)(?=</ns2:Timestamp>)", GetTimestamp(DateTime.Now));

                if (Directory.Exists(pathToEtalons))
                {
                    var files = Directory.GetFiles(pathToEtalons, "request*.xml");

                    foreach (var item in files)
                    {
                        var resp = item.ToLower().Replace(pathToEtalons.ToLower(), "").Replace("request", "");
                        string linesss = File.ReadAllText($"{pathToEtalons}\\request{resp}");
                        textBox11.Text = Regex.Replace(textBox11.Text, @"(?<=<.*>).*\n\t(?=</ns2:MessagePrimaryContent>)", Regex.Replace(linesss, @"(?i)<\?xml version=""1\.0"" encoding=""UTF-8""\?>", "").Replace("  x", " x"));
                    }
                    ProcessXml(textBox11, textBox12, ISMEV.SOAPOperation.SEND_REQUEST);
                    ProcessXml(textBox13, textBox14, ISMEV.SOAPOperation.GET_RESPONSE);
                }
                else
                {
                    MetroMessageBox.Show("Нет скачанных эталонов", "Упсс..", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

            }
        }
        void HandleException(Exception ex)
        {
            MetroMessageBox.Show(ex.Message, "Автотестирование", MessageBoxButton.OK, MessageBoxImage.Error);
            File.AppendAllText("error.log", $"{DateTime.Now:dd.MM.yyyy HH:mm:ss} - {WorkWithException.GetAllMessages(ex)}{Environment.NewLine}{WorkWithException.GetFullStack(ex)}{Environment.NewLine}");
        }

        void textBox14Change(object s, TextChangedEventArgs e)
        {
            Match matchT = Regex.Match(textBox14.Text, "<(.*?)MessageID>(.*?)</(.*?)MessageID>");
            string mess = matchT.Groups[2].Value;
            textBox15.Text = Regex.Replace(textBox15.Text, @"(?<=<ns2:AckTargetMessage Id=""MainContent"" accepted=""true"">)(.*)(?=</ns2:AckTargetMessage>)", mess);
            ProcessXml(textBox15, textBox16, ISMEV.SOAPOperation.ACK);
        }
        void textBox6Change(object s, TextChangedEventArgs e)
        {
            if (_save || !_checkMessage)
            {
                tab.IsEnabled = true;
                return;
            }
            //Прочитаем вставленный фрагмент, как xml
            XmlDocument xmlFromTb = new XmlDocument();
            try
            {
                //В методе textBox6_PreviewKeyDown идет обработка буфера обмена
                xmlFromTb.LoadXml(ExtractXMLWithEnd(textBox6.Text));
            }
            //Возникает после нажатия на кнопку сохранить так как тут забито срабатывание на изменение
            catch (Exception ex)
            {
                MetroMessageBox.Show($"При разборе xml для поиска возникла ошибка: {ex.Message}", "Ошибка разбора", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //Найдем узел MessagePrimaryContent для сравнения
            string[] nodes = { "MessagePrimaryContent", "basic:MessagePrimaryContent", "ns1:MessagePrimaryContent", "ns2:MessagePrimaryContent" };
            var searchNode = nodes.Select(node => SearchMessagePrimaryContent(xmlFromTb, node)).FirstOrDefault(node => node != null);
            Match matchT = Regex.Match(textBox6.Text, "<(.*?)MessageID>(.*?)</(.*?)MessageID>");
            string mess = matchT.Groups[2].Value;
            Match matchT1 = Regex.Match(textBox6.Text, "<(.*?)Reply(.*?)>(.*?)</(.*?)Reply(.*?)>");
            string reply = matchT1.Groups[3].Value;
            textBox7.Text = Regex.Replace(textBox7.Text, @"(?<=<ns2:AckTargetMessage Id=""MainContent"" accepted=""true"">)(.*)(?=</ns2:AckTargetMessage>)", mess);
            textBox9.Text = Regex.Replace(textBox9.Text, @"(?<=<ns1:To>)(.*)(?=</ns1:To>)", reply);
            textBox9.Text = Regex.Replace(textBox9.Text, @"(?<=<ns1:MessageID>)(.*)(?=</ns1:MessageID>)", GenerateTimeBasedGuid().ToString());
            if (Directory.Exists(pathToEtalons))
            {
                if (textBox6.Text.Trim().Length < 10)
                    return;
                var files = Directory.GetFiles(pathToEtalons, "request*.xml");
                var exist = false;
                foreach (var item in files)
                {
                    try
                    {
                        XmlDocument itemXml = new XmlDocument();
                        //Читаем файл, как xml
                        itemXml.Load(item);
                        //Сравниваем его
                        if (CompareXml(searchNode, itemXml))
                        {
                            exist_request.Text = $"Совпадает c эталоном {item}";
                            var resp = item.ToLower().Replace(pathToEtalons.ToLower(), "").Replace("request", "");
                            string linesss = File.ReadAllText($"{pathToEtalons}\\Response{resp}");
                            textBox9.Text = Regex.Replace(textBox9.Text, @"(?<=<.*>).*\n\t(?=</ns2:MessagePrimaryContent>)", Regex.Replace(linesss, @"(?i)<\?xml version=""1\.0"" encoding=""UTF-8""\?>", "").Replace("  x", " x"));
                            //Дергаем экшен, если он инициализирован(это происходит при автотесте)
                            callback?.Invoke();
                            //Очищаем экшен
                            callback = null;
                            exist = true;
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        MetroMessageBox.Show(ex.Message, "Упсс..", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                if (!exist)
                {
                    exist_request.Text = "";
                    MetroMessageBox.Show($"Совпадения не найдены", "Упсс..", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        XmlNode SearchMessagePrimaryContent(XmlNode root, string name)
        {
            //Бежим по всем дочерним узлам
            foreach (XmlNode child in root.ChildNodes)
            {
                //Проверяем, если имя совпало, то возвращаем значение назад
                if (child.Name.ToLower().Trim().Equals(name.ToLower().Trim()))
                    return child;
                else
                {
                    //Если имя не совпало, то ищем в дочерних элементах текущего элемента
                    var deepNode = SearchMessagePrimaryContent(child, name);
                    if (deepNode != null)//Если значение не null, то значит где-то на нижних уровнях нашлось, возвращаем его
                        return deepNode;
                }
            }
            return null;//Если ни в одном узле не нашлось значение, то возвращаем null
        }

        bool CompareXml(XmlNode searchDoc, XmlNode etalon)
        {
            if (searchDoc == null)
                return false;
            bool result = true;
            int index = 0;
            //Бежим по всем дочерним узлам текущего узла из xml для поиска
            foreach (XmlNode node in searchDoc.ChildNodes)
            {
                //получаем дочерний элемент с совпадающим индексом из xml эталона
                var etalonChild = etalon.ChildNodes[index];
                //Если это декларативный узел <?xml version="1.0" encoding="utf-8"?>, то берем узел с следующим индексом
                if (etalon.ChildNodes[index] is XmlDeclaration)
                    etalonChild = etalon.ChildNodes[index + 1];
                //Сравниваем имена узлов, если совпали, то идем дальше
                if (node.Name.Equals(etalonChild.Name))
                {
                    if (string.IsNullOrEmpty(node.Value))//Если у данного узла нет значения, то попытаемся пройтись по дочерним узлам
                        result = CompareXml(node, etalonChild);
                    else//Если значение у узла есть, то сравниваем его с эталонным
                        result = Replace(node.Value).Equals(Replace(etalonChild.Value));
                }//Если имена узлов не совпали, то считаем, что файл эталона не подходит
                else return false;
                if (!result)//Проверяем мало-ли где-то выше вернулся false
                    return result;
                index++;//Увеличиваем индекс анализируемого узла
            }
            return result;//Если все ок, то результат будет true
        }

        string Replace(string value)
        {
            var newValue = value
                        .Replace(" ", "")//Убираем пробелы
                        .ToLower()//Приводим к нижнему регистру
                        .Replace("\r", "")//Убираем спец символ (часть переноса текста)
                        .Replace("\n", "")//Убираем спец символ (часть переноса текста)
                        .Replace("\t", "");//Убираем спец символ (табуляция)
            return newValue;
        }
        string ExtractXMLWithEnd(string text)
        {
            string startXml = "<soap:Envelope";
            string endXml = "</soap:Envelope>";
            int startIndex = text.IndexOf(startXml);
            //Находим конец xml, на всякий случай ищу с 3го символа
            int endIndex = text.IndexOf(endXml, 4) + endXml.Length;
            //Вычисляем размер получившегося текста
            int len = text.Length - startIndex - (text.Length - endIndex);
            if (len > 0)//Перезаписываем буфер обмена
                return text.Substring(startIndex, len);
            return text;
        }
        void DownloadFile(string url, Match match, WebClient Client, string currentPath)
        {
            Directory.CreateDirectory(pathToEtalons);
            Directory.GetFiles(pathToEtalons, "*.xml", SearchOption.AllDirectories).ToList().ForEach(x => File.Delete(x));

            Directory.CreateDirectory(Path.Combine(currentPath, match.Groups[2].Value));
            Client.DownloadFile(url, $@"{currentPath}\{match.Groups[2].Value}\{match.Groups[2].Value}.zip");
        }

        bool SpecialparsingeEtalon(Match match, string currentPath)
        {
            using (ZipArchive archive = ZipFile.OpenRead($@"{currentPath}\{match.Groups[2].Value}\{match.Groups[2].Value}.zip"))
            {
                foreach (ZipArchiveEntry entry in archive.Entries.Where(entry => entry.FullName.Contains(".xml")))
                {
                    string destinationPath = Path.GetFullPath(Path.Combine(pathToEtalons, ConvertZipEntryFullNameToFileName(entry)));
                    if (destinationPath.StartsWith(pathToEtalons, StringComparison.Ordinal))
                        entry.ExtractToFile(destinationPath);
                }
            }
            Directory.Delete($@"{currentPath}\{match.Groups[2].Value}", true);
            var files = new DirectoryInfo(pathToEtalons).GetFiles("Request*.xml");
            foreach (var file in files)
            {
                XDocument text = XDocument.Load(file.FullName);
                XElement el2 = text.Root;
                textBox2.Text = el2.Name.NamespaceName;
                textBox3.Text = el2.Name.LocalName;
            }
            return files.Length > 0;
        }
        static string ConvertZipEntryFullNameToFileName(ZipArchiveEntry entry)
        {
            //Разберем файл на кусочки - 0/request.xml на 0 request и xml
            var nameElements = entry.FullName.Split('/', '.');
            //Проверим, что у нас получилось 3 элемента
            if (nameElements.Length == 3) // Соберем в правильном порядке
                return $"{nameElements[1]}{nameElements[0]}.{nameElements[2]}";
            //Если элементов меньше 3х, то просто уберем /, чтобы создать хоть какой-то файл
            return entry.FullName.Replace("/", "");
        }
        #region "Events"
        void etalonsDownload_Click(object s, RoutedEventArgs e)
        {
            if (etalonsDownload.IsChecked == true)
            {
                textBox1.IsReadOnly = false;
                Rectangle1.Visibility = Visibility.Collapsed;
                TextBlock1.Visibility = Visibility.Collapsed;
            }
            else
            {
                textBox1.IsReadOnly = true;
                Rectangle1.Visibility = Visibility.Visible;
                TextBlock1.Visibility = Visibility.Visible;
            }
        }
        #region "EtalonDragDrop"
        void Rectangle1_DragEnter(object s, DragEventArgs e)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(pathToEtalons);
            foreach (var file in dirInfo.GetFiles())
            {
                file.Delete();
            }
            textBox2.Clear();
            textBox3.Clear();
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                TextBlock1.Text = "Отпустите файлы";
                e.Effects = DragDropEffects.All;
            }
        }

        void Rectangle1_DragLeave(object s, DragEventArgs e)
        {
            TextBlock1.Text = "Закинуть эталоны";
        }

        void RenameAndMoveFiles(string sourceDir)
        {
            Directory.CreateDirectory(pathToEtalons);

            // Копируем файлы Request.xml и Response.xml из всех поддиректорий в новую директорию
            var requestResponsePairs = Directory.GetDirectories(sourceDir)
                .Select(dirPath => (
                    requestFilePath: Path.Combine(dirPath, "Request.xml"),
                    responseFilePath: Path.Combine(dirPath, "Response.xml")
                )).Where(pair => File.Exists(pair.requestFilePath) && File.Exists(pair.responseFilePath));

            foreach (var (requestFilePath, responseFilePath) in requestResponsePairs)
            {
                // Получаем название папки и формируем новые имена файлов
                string dirName = new DirectoryInfo(Path.GetDirectoryName(requestFilePath)).Name;
                string destFileName = $"Request{dirName}.xml";
                string destFilePath = Path.Combine(pathToEtalons, destFileName);
                File.Copy(requestFilePath, destFilePath);

                destFileName = $"Response{dirName}.xml";
                destFilePath = Path.Combine(pathToEtalons, destFileName);
                File.Copy(responseFilePath, destFilePath);
            }
        }

        void Rectangle1_DragDrop(object s, DragEventArgs e)
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            TextBlock1.Text = files.FirstOrDefault() ?? "";
            try
            {
                string dircopyFrom = TextBlock1.Text;

                // Перемещаем и переименовываем файлы Request.xml и Response.xml в главную папку
                RenameAndMoveFiles(dircopyFrom);

                // Выводим атрибуты Name.NamespaceName и Name.LocalName корневого элемента файла Request*.xml в соответствующие текстовые поля
                foreach (var requestFilePath in Directory.GetFiles(pathToEtalons, "Request*.xml"))
                {
                    XDocument text = XDocument.Load(requestFilePath);
                    XElement el2 = text.Root;
                    textBox2.Text = el2.Name.NamespaceName;
                    textBox3.Text = el2.Name.LocalName;
                    break; // выводим информацию только о первом файле
                }
                if (textBox3.Text == "")
                {
                    MetroMessageBox.Show("Упсс... Нужны эталоны!", "Упсс..", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MetroMessageBox.Show("Загрузил эталоны", "Загрузка эталонов", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                TextBlock1.Text = "Закинуть эталоны";
            }
            catch (DirectoryNotFoundException ex)
            {
                MetroMessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                TextBlock1.Text = "Закинуть эталоны";
            }
        }
        #endregion
        void testPotrebitel_Click(object s, RoutedEventArgs e)
        {
            SendRequestRequest_.Visibility = Visibility.Visible;
            GetResponseRequest_.Visibility = Visibility.Visible;
            AckRequestO_.Visibility = Visibility.Visible;
            if (testPotrebitel.IsChecked == true)
            {
                SendRequestRequest_.IsSelected = true;
                GetRequestRequest_.Visibility = Visibility.Collapsed;
                AckRequest_.Visibility = Visibility.Collapsed;
                SendResponseRequest_.Visibility = Visibility.Collapsed;
            }
            else
            {
                GetRequestRequest_.IsSelected = true;
                SendRequestRequest_.Visibility = Visibility.Collapsed;
                GetResponseRequest_.Visibility = Visibility.Collapsed;
                AckRequestO_.Visibility = Visibility.Collapsed;
                GetRequestRequest_.Visibility = Visibility.Visible;
                AckRequest_.Visibility = Visibility.Visible;
                SendResponseRequest_.Visibility = Visibility.Visible;
            }
        }
        #endregion
        #region "Settings - JCP"
        #region "DownloadJava"
        async void StartDownload(string url, string fileName)
        {
            DownloadJCP.IsEnabled = false;
            DownloadlJava86.IsEnabled = false;
            DownloadlJava64.IsEnabled = false;
            MProgressBar.Visibility = Visibility.Visible;

            using (WebClient Client = new WebClient())
            {
                using (var stream = Client.OpenRead(url))
                {
                    string size = (Convert.ToDouble(Client.ResponseHeaders["content-length"]) / 1048576).ToString("#.# МБ");
                    stream.Dispose();
                    Client.DownloadProgressChanged += (s, e) =>
                    {
                        MTBBB1.Text = $"Размер файла: {size}\nЗагружено: {e.ProgressPercentage}% ({((double)e.BytesReceived / 1048576).ToString("#.#")}МБ)";
                        MProgressBar.Value = e.ProgressPercentage;
                    };
                    Client.DownloadFileAsync(new Uri(url), $@"{Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop))}\{fileName}");
                    Client.DownloadFileCompleted += async (s, e) =>
                    {
                        MProgressBar.Visibility = Visibility.Collapsed;
                        MProgressBar.Value = 0;
                        MTBBB1.Text = "";
                        DownloadJCP.IsEnabled = true;
                        DownloadlJava86.IsEnabled = true;
                        DownloadlJava64.IsEnabled = true;
                        textBox17.FontSize = 20;
                        textBox17.Text = $"Идет установка {fileName}";
                        Process.Start(new ProcessStartInfo { FileName = "cmd", WorkingDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)), Arguments = @$"/c {fileName} /s INSTALL_SILENT=1", WindowStyle = ProcessWindowStyle.Hidden }).WaitForExit();
                        textBox17.Text = $"Установка {fileName} завершена";
                        await Task.Delay(3000);
                        textBox17.FontSize = 11;
                        textBox17.Text = "";
                    };
                }
            }
        }

        void DownloadlJava86_Click(object s, RoutedEventArgs e)
        {
            StartDownload(MTBBB2.Text, "Java86.exe");
        }

        void DownloadJava64_Click(object s, RoutedEventArgs e)
        {
            StartDownload(MTBBB3.Text, "Java64.exe");
        }
        #endregion
        #region "DownloadJCP"
        void DownloadJCP_Click(object s, RoutedEventArgs e)
        {
            if (Directory.Exists($@"C:\jcpv20"))
            {
                MetroMessageBox.Show("У вас уже есть JCP v2.0", "Упсс..", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                DownloadJCP.IsEnabled = false;
                DownloadlJava86.IsEnabled = false;
                DownloadlJava64.IsEnabled = false;
                MProgressBar.Visibility = Visibility.Visible;
                string UrlJCP = "https://github.com/WildHuntRider/WHR/raw/master/Files/jcpv20.zip";
                MProgressBar.Value = 0;
                MTBBB1.Text = "";
                using (WebClient Client = new WebClient())
                {
                    using (var stream = Client.OpenRead(UrlJCP))
                    {
                        string size = (Convert.ToDouble(Client.ResponseHeaders["content-length"]) / 1048576).ToString("#.# МБ");
                        Client.DownloadProgressChanged += (s, e) =>
                        {
                            MTBBB1.Text = $"Размер файла: {size}\nЗагружено: {e.ProgressPercentage}% ({((double)e.BytesReceived / 1048576).ToString("#.#")}МБ)";
                            MProgressBar.Value = e.ProgressPercentage;
                        };
                        Client.DownloadFileAsync(new Uri(UrlJCP), $@"{Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments))}\jcpv20.zip");
                        Client.DownloadFileCompleted += (s, args) =>
                        {
                            ZipFile.ExtractToDirectory($@"{Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments))}\jcpv20.zip", @"C:\");
                            MProgressBar.Visibility = Visibility.Collapsed;
                            MTBBB1.Text = "";
                            DownloadJCP.IsEnabled = true;
                            DownloadlJava86.IsEnabled = true;
                            DownloadlJava64.IsEnabled = true;
                        };
                    }
                }
            }
        }
        #endregion
        void InstallJCP_Click(object s, RoutedEventArgs e)
        {
            if (!Directory.Exists(@"C:\jcpv20"))
            {
                MetroMessageBox.Show("Сначала загрузите JCP v2.0", "Упсс..", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            Process JCP_Install = Process.Start(new ProcessStartInfo
            {
                FileName = "cmd",
                WorkingDirectory = @"c:\jcpv20",
                Arguments = $@"/c chcp 1252 & setup_console.bat ""{JavaVersion.Text}"" -force -ru -install -jre ""{JavaVersion.Text}"" -jcp -jcryptop -cpssl -cades -rutoken",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
            });
            textBox17.Text = JCP_Install.StandardOutput.ReadToEnd();

        }
        void ControlPanel_Click(object s, RoutedEventArgs e)
        {
            if (!Directory.Exists(@"C:\jcpv20"))
            {
                MetroMessageBox.Show("Сначала загрузите JCP v2.0", "Упсс..", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            Process JCP_ControlPanel = Process.Start(new ProcessStartInfo
            {
                FileName = "cmd",
                WorkingDirectory = @"c:\jcpv20",
                Arguments = $@"/c chcp 1252 & ControlPane.bat ""{JavaVersion.Text}""",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
            });
            textBox17.Text = JCP_ControlPanel.StandardOutput.ReadToEnd();

        }
        void Uninstall_Click(object s, RoutedEventArgs e)
        {
            if (!Directory.Exists(@"C:\jcpv20"))
            {
                MetroMessageBox.Show("У вас нет установленного JCP v2.0", "Упсс..", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            Process JCP_Uninstall = Process.Start(new ProcessStartInfo
            {
                FileName = "cmd",
                WorkingDirectory = @"c:\jcpv20",
                Arguments = $@"/c chcp 1252 &  setup_console.bat ""{JavaVersion.Text}"" -force -ru -uninstall -jre ""{JavaVersion.Text}"" -jcp -rmsetting",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
            });
            textBox17.Text = JCP_Uninstall.StandardOutput.ReadToEnd();

            string subKey = @"SOFTWARE\JavaSoft\Prefs";
            using (RegistryKey KeyLM = Registry.LocalMachine.OpenSubKey(subKey, true))
            {
                if (KeyLM != null)
                {
                    Registry.LocalMachine.DeleteSubKeyTree(subKey, true);
                }
            }

            using (RegistryKey KeyLM = Registry.CurrentUser.OpenSubKey(subKey, true))
            {
                if (KeyLM != null)
                {
                    Registry.CurrentUser.DeleteSubKeyTree(subKey, true);
                }
            }
        }
        void ResetTrial_Click(object s, RoutedEventArgs e)
        {
            string value = Registry.LocalMachine.OpenSubKey("SOFTWARE\\JavaSoft\\Prefs\\ru\\/Crypto/Pro")?.GetValue("/J/C/P/Check/Sum_2_0")?.ToString();
            if (value != null)
            {
                using (RegistryKey subKey = Registry.LocalMachine.CreateSubKey("SOFTWARE\\JavaSoft\\Prefs\\ru\\/Crypto/Pro")) subKey?.DeleteValue("/J/C/P/Check/Sum_2_0");
                MetroMessageBox.Show($"Нашел значение отвечающее за сброс триальной версии{Environment.NewLine}{value}{Environment.NewLine}Удалил данное значение", "Информация", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            else
            {
                MetroMessageBox.Show("Данные для обнуление триальной версии не найдены", "Упсс..", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        private void RefreshJavaVersions()
        {
            JavaVersion.Items.Clear();
            string[] javaPaths = new string[] { @"C:\Program Files\Java\", @"C:\Program Files (x86)\Java\" };

            foreach (string javaPath in javaPaths)
            {
                if (Directory.Exists(javaPath))
                {
                    string[] allFolders = Directory.GetDirectories(javaPath);
                    foreach (string folder in allFolders)
                    {
                        JavaVersion.Items.Remove(folder);
                        JavaVersion.Items.Add(folder);
                        JavaVersion.SelectedIndex = 0;
                    }
                }
            }
        }

        void JavaVersion_Initialized(object s, EventArgs e)
        {
            RefreshJavaVersions();
        }

        void SwitchJava_Click(object s, RoutedEventArgs e)
        {
            if (SwitchJava.IsChecked == true)
            {
                RefreshJavaVersions();
            }
        }
        #endregion


        void textBox5_ButtonClick(object s, EventArgs e)
        {
            ProcessXml(textBox5, textBox6, ISMEV.SOAPOperation.GET_REQUEST);
        }

        void textBox7_ButtonClick(object s, EventArgs e)
        {
            ProcessXml(textBox7, textBox8, ISMEV.SOAPOperation.ACK);
        }

        void textBox9_ButtonClick(object s, EventArgs e)
        {
            ProcessXml(textBox9, textBox10, ISMEV.SOAPOperation.SEND_RESPONSE);
        }

        void textBox11_ButtonClick(object s, EventArgs e)
        {
            ProcessXml(textBox11, textBox12, ISMEV.SOAPOperation.SEND_REQUEST);
        }

        void textBox13_ButtonClick(object s, EventArgs e)
        {
            ProcessXml(textBox13, textBox14, ISMEV.SOAPOperation.GET_RESPONSE);
        }

        void textBox15_ButtonClick(object s, EventArgs e)
        {
            ProcessXml(textBox15, textBox16, ISMEV.SOAPOperation.ACK);
        }

        void Signs_Click(object s, RoutedEventArgs e)
        {
            string signedXml = textBox20.Text;
            string phd = SignsCb1.SelectionBoxItem.ToString();
            Task.Run(() =>
            {
                signedXml = signer.SignedXml(signedXml, phd);
                textBox20.Dispatcher.Invoke(() =>
                {
                    textBox20.Text = signedXml;
                });
            });
        }

        void RequestButton_Click(object s, EventArgs e)
        {
            if (GetRequestRequest1.IsChecked == true)
                ProcessXml(textBox20, textBox21, ISMEV.SOAPOperation.GET_REQUEST);
            else if (SendRequestRequest1.IsChecked == true)
                ProcessXml(textBox20, textBox21, ISMEV.SOAPOperation.SEND_REQUEST);
            else if (GetResponseRequest1.IsChecked == true)
                ProcessXml(textBox20, textBox21, ISMEV.SOAPOperation.GET_RESPONSE);
            else if (SendResponseRequest1.IsChecked == true)
                ProcessXml(textBox20, textBox21, ISMEV.SOAPOperation.SEND_RESPONSE);
            else if (AckRequest1.IsChecked == true)
                ProcessXml(textBox20, textBox21, ISMEV.SOAPOperation.ACK);
        }

        void RequestOption_Click(object s, RoutedEventArgs e)
        {
            CheckBox selectedOption = (CheckBox)s;

            if (selectedOption.IsChecked == false)
            {
                // Если пользователь снова кликнул на уже отмеченный CheckBox,
                // то сбрасываем его состояние и очищаем поле textBox20.Text.
                selectedOption.IsChecked = false;
                textBox20.Clear();
                return;
            }

            GetRequestRequest1.IsChecked = (selectedOption.Name == "GetRequestRequest1");
            SendRequestRequest1.IsChecked = (selectedOption.Name == "SendRequestRequest1");
            GetResponseRequest1.IsChecked = (selectedOption.Name == "GetResponseRequest1");
            SendResponseRequest1.IsChecked = (selectedOption.Name == "SendResponseRequest1");
            AckRequest1.IsChecked = (selectedOption.Name == "AckRequest1");

            switch (selectedOption.Name)
            {
                case "GetRequestRequest1":
                    textBox20.Text = GetRequestRequest;
                    textBox20.Text = Regex.Replace(textBox20.Text, @"(?<=<ns2:Timestamp>)(.*)(?=</ns2:Timestamp>)", GetTimestamp(new DateTime()));
                    break;
                case "SendRequestRequest1":
                    textBox20.Text = SendRequestRequest;
                    textBox20.Text = Regex.Replace(textBox20.Text, @"(?<=<ns1:MessageID>)(.*)(?=</ns1:MessageID>)", GenerateTimeBasedGuid().ToString());
                    break;
                case "GetResponseRequest1":
                    textBox20.Text = GetResponseRequest;
                    textBox20.Text = Regex.Replace(textBox20.Text, @"(?<=<ns2:Timestamp>)(.*)(?=</ns2:Timestamp>)", GetTimestamp(new DateTime()));
                    break;
                case "SendResponseRequest1":
                    textBox20.Text = SendResponseRequest;
                    textBox20.Text = Regex.Replace(textBox20.Text, @"(?<=<ns1:MessageID>)(.*)(?=</ns1:MessageID>)", GenerateTimeBasedGuid().ToString());
                    break;
                case "AckRequest1":
                    textBox20.Text = AckRequest;
                    break;
                default:
                    break;
            }
        }
    }
}