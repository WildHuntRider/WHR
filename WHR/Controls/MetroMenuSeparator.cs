using WHR.Utility.Element;
using System.Windows.Controls;

namespace WHR.Controls
{
    public class MetroMenuSeparator : Separator
    {
        static MetroMenuSeparator()
        {
            ElementBase.DefaultStyle<MetroMenuSeparator>(DefaultStyleKeyProperty);
        }
    }
}