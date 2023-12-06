using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interop;

namespace HorizontalScroll.Internal
{
    /// <summary>
    /// Provides a window message hook that adds support for horizontal scrolling with tiltable mouse wheels.
    /// </summary>
    internal static class HorizontalScrollWindowHook
    {
        #region Properties
        private static readonly HashSet<IntPtr> _hookedHwnds = new();
        #endregion Properties

        #region Methods

        #region (Private) WndProcHook
        private const int WM_MOUSEHWHEEL = 0x020E;
        private static IntPtr WndProcHook(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
            case WM_MOUSEHWHEEL:
                HandleMouseWheelHorizontal(Environment.TickCount, GetHorizontalDelta(wParam));
                break;
            }
            return IntPtr.Zero;
        }
        #endregion (Private) WndProcHook

        #region (Private) GetHorizontalDelta
        private static int GetHorizontalDelta(IntPtr value)
        {
            return unchecked(-(short)((uint)(IntPtr.Size == 8 ? (int)value.ToInt64() : value.ToInt32()) >> 16));
        }
        #endregion (Private) GetHorizontalDelta

        #region (Private) HandleMouseWheelHorizontal
        private static void HandleMouseWheelHorizontal(int tickCount, int delta)
        {
            if (Mouse.DirectlyOver is not IInputElement element)
                return; //< the mouse isn't over an element

            if (element is not UIElement)
            { // get first UIElement ancestor
                if (VisualTreeHelpers.FindAncestorWithType<UIElement>((DependencyObject)element) is not UIElement ancestorElement)
                    return; //< no ancestors of type UIElement
                element = ancestorElement;
            }

            // invoke the event(s)
            InvokeEventWithPreview(
                element,
                HorizontalScroll.PreviewMouseWheelTiltEvent,
                HorizontalScroll.MouseWheelTiltEvent,
                new MouseWheelEventArgs(Mouse.PrimaryDevice, tickCount, delta));
        }
        #endregion (Private) HandleMouseWheelHorizontal

        #region (Private) InvokeEventWithPreview
        private static void InvokeEventWithPreview(IInputElement sender, RoutedEvent previewEvent, RoutedEvent mainEvent, RoutedEventArgs eventArgs)
        {
            eventArgs.RoutedEvent = previewEvent;
            sender.RaiseEvent(eventArgs);
            if (eventArgs.Handled) return;

            eventArgs.RoutedEvent = mainEvent;
            sender.RaiseEvent(eventArgs);
        }
        #endregion (Private) InvokeEventWithPreview

        #region (Private) GetParentWindowHandle
        private static IntPtr GetWindowHandle(Window window) => new WindowInteropHelper(window).Handle;
        private static IntPtr? GetWindowHandle(DependencyObject d)
            => (PresentationSource.FromDependencyObject(d) as HwndSource)?.Handle;
        #endregion (Private) GetParentWindowHandle

        #region (Private) EnableTiltWheelSupport
        private static bool EnableTiltWheelSupport(IntPtr handle)
        {
            if (_hookedHwnds.Contains(handle) || handle == IntPtr.Zero)
                return true;

            var source = HwndSource.FromHwnd(handle);
            if (source == null)
                return false;

            _hookedHwnds.Add(handle);
            source.AddHook(WndProcHook);
            return true;
        }
        private static bool EnableTiltWheelSupport(IntPtr? handle) => handle.HasValue && EnableTiltWheelSupport(handle.Value);
        #endregion (Private) EnableSupport

        #region (Internal) EnableTiltWheelSupportFor
        internal static bool EnableTiltWheelSupportFor(UIElement uiElement)
        {
            if (uiElement is Window window)
            {
                if (window.IsLoaded)
                    return EnableTiltWheelSupport(GetWindowHandle(window));
                else
                { // enable support when loaded
                    window.Loaded += (s, e) => EnableTiltWheelSupport(GetWindowHandle((Window)s));
                    return true;
                }
            }
            else if (uiElement is Popup popup)
            {
                // enable support when a new popup window opens
                popup.Opened += (s, e) => EnableTiltWheelSupport(GetWindowHandle((Popup)s!));

                // enable support now if it's already open
                return !popup.IsOpen || EnableTiltWheelSupport(GetWindowHandle(popup));
            }
            else if (uiElement is ContextMenu contextMenu)
            {
                // enable support when a new contextmenu window opens
                contextMenu.Opened += (s, e) => EnableTiltWheelSupport(GetWindowHandle((ContextMenu)s));

                // enable support now if it's already open
                return !contextMenu.IsOpen || EnableTiltWheelSupport(GetWindowHandle(contextMenu));
            }
            else if (GetWindowHandle(uiElement) is IntPtr hWnd)
            {
                // add handler in case the parent window changes
                PresentationSource.AddSourceChangedHandler(uiElement, new((s, e) => EnableTiltWheelSupport((e.NewSource as HwndSource)?.Handle)));

                // enable support now
                return EnableTiltWheelSupport(hWnd);
            }
            else return false;
        }
        #endregion (Private) EnableTiltWheelSupportFor

        #endregion Methods
    }
}
