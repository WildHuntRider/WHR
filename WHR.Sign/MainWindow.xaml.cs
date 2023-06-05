using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;
using WHR.Controls;
using WHR.Utility.Update;

namespace WHR.Sign
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        string curver = Assembly.GetExecutingAssembly().GetName().Version.ToString(2);
        string exename = AppDomain.CurrentDomain.FriendlyName;
        string exepath = Assembly.GetEntryAssembly().Location;
        public MainWindow()
        {
            InitializeComponent();
            List<string> phdNames = new List<string>();
            KeyDown += (s, e) => { if (e.Key == Key.Enter) btnPodpis_Click(btnPodpis, null); };
            Task.Run(() => { GetPHDs().ForEach(Action => phdNames.Add(Action)); }).ContinueWith(Action => { phdNames.ForEach(a => SignsCb.Items.Add(a)); }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        public void MetroTitleMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Update versionUpdater = new Update();
            versionUpdater.CheckAndUpdateVersionWHRSIGN(exename, exepath, curver);
        }
        private string SignedXml(string xml, string singName)
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

        private List<string> GetPHDs()
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

        private void btnPodpis_Click(object sender, RoutedEventArgs e)
        {
            string signedXml = textBox1.Text;
            string phd = SignsCb.SelectionBoxItem.ToString();
            Task.Run(() => { signedXml = SignedXml(signedXml, phd); }).ContinueWith(a => { textBox1.Text = signedXml; }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void MetroMenuCopy_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MetroMenuItem menuItem && menuItem.Parent is ContextMenu contextMenu)
            {
                if (contextMenu.PlacementTarget is MetroTextBox textBox)
                {
                    textBox.SelectAll();
                    textBox.Copy();
                }
            }
        }

        private void MetroMenuPaste_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MetroMenuItem menuItem && menuItem.Parent is ContextMenu contextMenu)
            {
                if (contextMenu.PlacementTarget is MetroTextBox textBox)
                {
                    textBox.Paste();
                }
            }
        }
    }
}


