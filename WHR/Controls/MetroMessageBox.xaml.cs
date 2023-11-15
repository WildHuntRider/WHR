using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WHR.Controls
{
    public partial class MetroMessageBox : MetroWindow
    {
        MetroMessageBoxViewModel vm = new MetroMessageBoxViewModel();
        static MessageBoxResult result = MessageBoxResult.None;
        public MetroMessageBox()
        {
            InitializeComponent();
            DataContext = vm;
        }
        private MetroMessageBox(string message, string title, MessageBoxButton btn, MessageBoxImage img)
        {
            InitializeComponent();
            vm.Message = message;
            vm.Title = title;
            IconImg.Visibility = Visibility.Visible;
            vm.MessageIcon = ToIcon(img).ToImageSource();
            VisibleButtons(btn);
            DataContext = vm;
        }
        private MetroMessageBox(string message, string title, MessageBoxButton btn)
        {
            InitializeComponent();
            vm.Message = message;
            vm.Title = title;
            IconImg.Visibility = Visibility.Collapsed;
            VisibleButtons(btn);
            DataContext = vm;
        }
        private MetroMessageBox(string message, string title)
        {
            InitializeComponent();
            vm.Message = message;
            vm.Title = title;
            IconImg.Visibility = Visibility.Collapsed;
            VisibleButtons(MessageBoxButton.OK);
            DataContext = vm;
        }
        private MetroMessageBox(string message)
        {
            InitializeComponent();
            vm.Message = message;
            IconImg.Visibility = Visibility.Collapsed;
            VisibleButtons(MessageBoxButton.OK);
            DataContext = vm;
        }
        public static MessageBoxResult Show(string message, string title, MessageBoxButton btn, MessageBoxImage img)
        {
            MetroMessageBox cmb = new(message, title, btn, img);
            var dc = cmb.DataContext;
            cmb.ShowDialog();
            return result;
        }
        public static MessageBoxResult Show(string message, string title, MessageBoxButton btn)
        {
            MetroMessageBox cmb = new(message, title, btn);
            var dc = cmb.DataContext;
            cmb.ShowDialog();
            return result;
        }

        public static MessageBoxResult Show(string message, string title)
        {
            MetroMessageBox cmb = new(message, title);
            var dc = cmb.DataContext;
            cmb.ShowDialog();
            return result;
        }
        public static MessageBoxResult Show(string message)
        {
            MetroMessageBox cmb = new(message);
            var dc = cmb.DataContext;
            cmb.ShowDialog();
            return result;
        }
        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
        private Icon ToIcon(MessageBoxImage img)
        {
            Dictionary<MessageBoxImage, Icon> SystemImageToIcon = new Dictionary<MessageBoxImage, Icon>
            {
                { MessageBoxImage.Error, SystemIcons.Error },
                { MessageBoxImage.Information, SystemIcons.Information },
                { MessageBoxImage.Question, SystemIcons.Question },
                { MessageBoxImage.Warning, SystemIcons.Warning },
                { MessageBoxImage.None, null }
            };
            return SystemImageToIcon[img];
        }
        private void VisibleButtons(MessageBoxButton btn)
        {
            switch (btn)
            {
                case MessageBoxButton.OK:
                    OkBtn.Visibility = Visibility.Visible;
                    break;
                case MessageBoxButton.OKCancel:
                    OkBtn.Visibility = Visibility.Visible;
                    CancelBtn.Visibility = Visibility.Visible;
                    break;
                case MessageBoxButton.YesNo:
                    YesBtn.Visibility = Visibility.Visible;
                    NoBtn.Visibility = Visibility.Visible;
                    break;
                case MessageBoxButton.YesNoCancel:
                    YesBtn.Visibility = Visibility.Visible;
                    NoBtn.Visibility = Visibility.Visible;
                    CancelBtn.Visibility = Visibility.Visible;
                    break;
            }
        }
        private void YesBtn_Click(object sender, RoutedEventArgs e)
        {
            result = MessageBoxResult.Yes;
            DialogResult = true;
        }
        private void NoBtn_Click(object sender, RoutedEventArgs e)
        {
            result = MessageBoxResult.No;
            DialogResult = true;
        }
        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            result = MessageBoxResult.OK;
            DialogResult = true;
        }
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            result = MessageBoxResult.Cancel;
            DialogResult = true;
        }
    }
    public class MetroMessageBoxViewModel : INotifyPropertyChanged
    {
        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }
        private string _message;
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }
        private string _icon;
        public string Icon
        {
            get => _icon;
            set
            {
                _icon = value;
                OnPropertyChanged();
            }
        }
        private ImageSource _messageIcon;
        public ImageSource MessageIcon
        {
            get => _messageIcon;
            set
            {
                _messageIcon = value;
                OnPropertyChanged();
            }
        }
        private bool _visibleOk;
        public bool VisibleOk
        {
            get => this._visibleOk;
            set
            {
                this._visibleOk = value;
                OnPropertyChanged();
            }
        }
        private bool _visibleCancel;
        public bool VisibleCancel
        {
            get => this._visibleCancel;
            set
            {
                this._visibleCancel = value;
                OnPropertyChanged();
            }
        }
        private bool _visibleNo;
        public bool VisibleNo
        {
            get => this._visibleNo;
            set
            {
                this._visibleNo = value;
                OnPropertyChanged();
            }
        }
        private bool _visibleYes;
        public bool VisibleYes
        {
            get => this._visibleYes;
            set
            {
                this._visibleYes = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    internal static class IconUtilities
    {
        [DllImport("gdi32.dll", SetLastError = true)]
        private static extern bool DeleteObject(IntPtr hObject);
        public static ImageSource ToImageSource(this Icon icon)
        {
            Bitmap bitmap = icon.ToBitmap();
            IntPtr hBitmap = bitmap.GetHbitmap();

            ImageSource wpfBitmap = Imaging.CreateBitmapSourceFromHBitmap(
                hBitmap,
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
            if (!DeleteObject(hBitmap))
            {
                throw new Win32Exception();
            }
            return wpfBitmap;
        }
    }
}
