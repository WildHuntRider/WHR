using WHR.Utility.Element;
using System.Windows.Controls;

namespace WHR.Controls
{
    public class MetroGroupBox : GroupBox
    {
        static MetroGroupBox()
        {
            ElementBase.DefaultStyle<MetroGroupBox>(DefaultStyleKeyProperty);
        }
    }
}