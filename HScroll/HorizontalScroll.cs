using HScroll.Internal;
using System;
using System.Windows;
using System.Windows.Input;

namespace HScroll
{
    /// <summary>
    /// Defines attached <see cref="RoutedEvent"/>s for receiving horizontal scroll inputs.
    /// </summary>
    public static class HorizontalScroll
    {
        #region PreviewMouseWheelTiltEvent
        /// <summary>
        /// Occurs when the user tilts the mouse wheel while the mouse pointer is over an element.
        /// </summary>
        public static readonly RoutedEvent PreviewMouseWheelTiltEvent = EventManager.RegisterRoutedEvent(
            PreviewMouseWheelTiltEventName,
            RoutingStrategy.Tunnel,
            typeof(MouseWheelEventHandler),
            typeof(HorizontalScrollWindowHook));
        public const string PreviewMouseWheelTiltEventName = "PreviewMouseWheelTilt";

        /// <summary>
        /// Adds the specified <paramref name="handler"/> to the <see cref="PreviewMouseWheelTiltEvent"/>.
        /// </summary>
        public static void AddPreviewMouseWheelTiltHandler(DependencyObject dependencyObject, MouseWheelEventHandler handler)
        {
            if (dependencyObject is not UIElement uiElement)
                throw new ArgumentException($"{nameof(HorizontalScroll)}.{PreviewMouseWheelTiltEventName} can only be set on subclasses of type \"{typeof(UIElement)}\"!", nameof(dependencyObject));

            uiElement.AddHandler(PreviewMouseWheelTiltEvent, handler);
            HorizontalScrollWindowHook.EnableTiltWheelSupportFor(uiElement);
        }
        /// <summary>
        /// Removes the specified <paramref name="handler"/> from the <see cref="PreviewMouseWheelTiltEvent"/>.
        /// </summary>
        public static void RemovePreviewMouseWheelTiltHandler(DependencyObject dependencyObject, MouseWheelEventHandler handler)
            => ((UIElement)dependencyObject).RemoveHandler(PreviewMouseWheelTiltEvent, handler);
        #endregion PreviewMouseWheelTiltEvent

        #region MouseWheelTiltEvent
        /// <summary>
        /// Occurs when the user tilts the mouse wheel while the mouse pointer is over an element.
        /// </summary>
        public static readonly RoutedEvent MouseWheelTiltEvent = EventManager.RegisterRoutedEvent(
            MouseWheelTiltEventName,
            RoutingStrategy.Bubble,
            typeof(MouseWheelEventHandler),
            typeof(HorizontalScrollWindowHook));
        public const string MouseWheelTiltEventName = "MouseWheelTilt";

        /// <summary>
        /// Adds the specified <paramref name="handler"/> to the <see cref="MouseWheelTiltEvent"/>.
        /// </summary>
        public static void AddMouseWheelTiltHandler(DependencyObject dependencyObject, MouseWheelEventHandler handler)
        {
            if (dependencyObject is not UIElement uiElement)
                throw new ArgumentException($"{nameof(HorizontalScroll)}.{MouseWheelTiltEventName} can only be set on subclasses of type \"{typeof(UIElement)}\"!", nameof(dependencyObject));

            var inst = (UIElement)dependencyObject;
            inst.AddHandler(MouseWheelTiltEvent, handler);
            HorizontalScrollWindowHook.EnableTiltWheelSupportFor(inst);
        }
        /// <summary>
        /// Removes the specified <paramref name="handler"/> from the <see cref="MouseWheelTiltEvent"/>.
        /// </summary>
        public static void RemoveMouseWheelTiltHandler(DependencyObject d, MouseWheelEventHandler handler)
            => ((UIElement)d).RemoveHandler(MouseWheelTiltEvent, handler);
        #endregion MouseWheelTiltEvent
    }
}
