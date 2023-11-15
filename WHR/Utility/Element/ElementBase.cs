using System.Windows;
using System.Windows.Input;

namespace WHR.Utility.Element
{
    public class ElementBase
    {
        public static DependencyProperty Property<thisType, propertyType>(string name, propertyType defaultValue)
        {
            return DependencyProperty.Register(name.Replace("Property", ""), typeof(propertyType), typeof(thisType), new PropertyMetadata(defaultValue));
        }
        public static DependencyProperty Property<thisType, propertyType>(string name)
        {
            return DependencyProperty.Register(name.Replace("Property", ""), typeof(propertyType), typeof(thisType));
        }
        public static RoutedEvent RoutedEvent<thisType, propertyType>(string name)
        {
            return EventManager.RegisterRoutedEvent(name.Replace("Event", ""), RoutingStrategy.Bubble, typeof(propertyType), typeof(thisType));
        }
        public static void DefaultStyle<thisType>(DependencyProperty dp)
        {
            dp.OverrideMetadata(typeof(thisType), new FrameworkPropertyMetadata(typeof(thisType)));
        }
        public static RoutedUICommand Command<thisType>(string name)
        {
            return new RoutedUICommand(name, name, typeof(thisType));
        }
        public static string GoToState(FrameworkElement element, string state)
        {
            VisualStateManager.GoToState(element, state, false);
            return state;
        }
    }
}