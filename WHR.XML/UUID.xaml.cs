using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using WHR.Controls;
using WHR.XML.Utility;

namespace WHR.XML
{
    public partial class UUID : MetroWindow
    {
        public UUID()
        {
            InitializeComponent();
            KeyDown += (s, e) => { if (e.Key == Key.Enter) BT1_Click(BT1, null); };
            this.TB1.PreviewTextInput += new TextCompositionEventHandler(TB1_PreviewTextInput);
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