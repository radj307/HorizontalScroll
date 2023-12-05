using System.Windows;
using System.Windows.Markup;

[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "HorizontalScroll", AssemblyName = "Localization.WPF")]

namespace HorizontalScroll
{
    /// <summary>
    /// WPF attached event for receiving horizontal scroll events sent by tiltable mouse wheels.
    /// </summary>
    public static class HorizontalScrollWheel
    {
        #region Events

        #region PreviewMouseWheelHorizontalEvent
        /// <summary>
        /// Occurs when the user tilts the mouse wheel while the mouse pointer is over this element.
        /// </summary>
        public static readonly RoutedEvent PreviewMouseWheelHorizontalEvent = EventManager.RegisterRoutedEvent(
            "PreviewMouseWheelHorizontal",
            RoutingStrategy.Tunnel,
            typeof(MouseWheelHorizontalEventHandler),
            typeof(HorizontalScrollSupportHook));
        /// <summary>
        /// Adds the specified <paramref name="handler"/> to the <see cref="PreviewMouseWheelHorizontalEvent"/>.
        /// </summary>
        public static void AddPreviewMouseWheelHorizontalHandler(DependencyObject d, MouseWheelHorizontalEventHandler handler)
        {
            var inst = (UIElement)d;
            inst.AddHandler(PreviewMouseWheelHorizontalEvent, handler);
            HorizontalScrollSupportHook.EnableSupportFor(inst);
        }
        /// <summary>
        /// Removes the specified <paramref name="handler"/> from the <see cref="PreviewMouseWheelHorizontalEvent"/>.
        /// </summary>
        public static void RemovePreviewMouseWheelHorizontalHandler(DependencyObject d, MouseWheelHorizontalEventHandler handler)
            => ((UIElement)d).RemoveHandler(PreviewMouseWheelHorizontalEvent, handler);
        #endregion PreviewMouseWheelHorizontalEvent

        #region MouseWheelHorizontalEvent
        /// <summary>
        /// Occurs when the user tilts the mouse wheel while the mouse pointer is over this element.
        /// </summary>
        public static readonly RoutedEvent MouseWheelHorizontalEvent = EventManager.RegisterRoutedEvent(
            "MouseWheelHorizontal",
            RoutingStrategy.Bubble,
            typeof(MouseWheelHorizontalEventHandler),
            typeof(HorizontalScrollSupportHook));
        /// <summary>
        /// Adds the specified <paramref name="handler"/> to the <see cref="MouseWheelHorizontalEvent"/>.
        /// </summary>
        public static void AddMouseWheelHorizontalHandler(DependencyObject d, MouseWheelHorizontalEventHandler handler)
        {
            var inst = (UIElement)d;
            inst.AddHandler(MouseWheelHorizontalEvent, handler);
            HorizontalScrollSupportHook.EnableSupportFor(inst);
        }
        /// <summary>
        /// Removes the specified <paramref name="handler"/> from the <see cref="MouseWheelHorizontalEvent"/>.
        /// </summary>
        public static void RemoveMouseWheelHorizontalHandler(DependencyObject d, MouseWheelHorizontalEventHandler handler)
            => ((UIElement)d).RemoveHandler(MouseWheelHorizontalEvent, handler);
        #endregion MouseWheelHorizontalEvent

        #endregion Events
    }
}
