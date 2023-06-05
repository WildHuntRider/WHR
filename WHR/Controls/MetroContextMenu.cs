using WHR.Utility.Element;
using System.Windows.Controls;

namespace WHR.Controls
{
    public class MetroContextMenu : ContextMenu
    {
        public MetroContextMenu()
        {
            Utility.Refresh(this);
        }
        static MetroContextMenu()
        {
            ElementBase.DefaultStyle<MetroContextMenu>(DefaultStyleKeyProperty);
        }
    }

}