using WHR.Utility.Element;
using System.Windows.Controls;

namespace WHR.Controls
{
    public class MetroTitleMenu : Menu
    {
        public MetroTitleMenu()
        {
            Utility.Refresh(this);
        }
        static MetroTitleMenu()
        {
            ElementBase.DefaultStyle<MetroTitleMenu>(DefaultStyleKeyProperty);
        }
    }
}