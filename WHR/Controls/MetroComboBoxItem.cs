using WHR.Utility.Element;
using System.Windows.Controls;

namespace WHR.Controls
{
    public class MetroComboBoxItem : ComboBoxItem
    {
        public MetroComboBoxItem()
        {
            Utility.Refresh(this);
        }
        static MetroComboBoxItem()
        {
            ElementBase.DefaultStyle<MetroComboBoxItem>(DefaultStyleKeyProperty);
        }
    }
}