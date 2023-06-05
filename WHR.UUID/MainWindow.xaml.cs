using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;
using WHR.Controls;
using WHR.Utility;
using WHR.Utility.Update;
using WHR.UUID.Utils;

namespace WHR.UUID
{
    public partial class MainWindow : MetroWindow
    {
        string curver = Assembly.GetExecutingAssembly().GetName().Version.ToString(2);
        string exename = AppDomain.CurrentDomain.FriendlyName;
        string exepath = Assembly.GetEntryAssembly().Location;
        public MainWindow()
        {
            InitializeComponent();
            KeyDown += (s, e) => { if (e.Key == Key.Enter) BT1_Click(BT1, null); };
            this.TB1.PreviewTextInput += new TextCompositionEventHandler(TB1_PreviewTextInput);
        }
        public void MetroTitleMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Update versionUpdater = new Update();
            versionUpdater.CheckAndUpdateVersionWHRUUID(exename, exepath, curver);
        }
        private void BT1_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(TB1.Text, out int countText) && countText > 0)
            {
                List<Guid> uuids = MS1.IsChecked == false ? UUIDGeneratorV4.GenerateUUIDV4(countText) : UUIDGeneratorV1.GenerateUUIDV1(countText);
                TB2.Text = string.Join(Environment.NewLine, uuids);
            }
            else
            {
                MetroMessageBox.Show("Пожалуйста, введите корректное число!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void TB1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
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
        private void MS1_Checked(object sender, RoutedEventArgs e)
        {
            MS1.Content = "Version 1 UUID";
            TB1.InputHint = "Version 4 UUID";
        }

        private void MS1_Unchecked(object sender, RoutedEventArgs e)
        {
            MS1.Content = "Version 4 UUID";
            TB1.InputHint = "Version 1 UUID";
        }
    }
}