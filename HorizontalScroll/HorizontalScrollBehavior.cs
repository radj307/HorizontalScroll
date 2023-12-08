using Microsoft.Xaml.Behaviors;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace HorizontalScroll
{
    /// <summary>
    /// <see cref="ScrollViewer"/> behavior that enables scrolling horizontally by tilting the mouse wheel left or right, and by holding Shift while scrolling up or down.
    /// </summary>
    /// <example>
    /// # Attach in XAML
    /// <code>
    /// &lt;ScrollViewer xmlns:i="http://schemas.microsoft.com/xaml/behaviors">
    ///     &lt;i:Interaction.Behaviors>
    ///         &lt;HorizontalScrollBehavior />
    ///     &lt;/i:Interaction.Behaviors>
    /// &lt;/ScrollViewer>
    /// </code>
    /// 
    /// You can attach this to a control in C# with:
    /// <code>
    /// using HorizontalScroll;
    /// using Microsoft.Xaml.Behaviors;
    /// 
    /// var myScrollViewer = new ScrollViewer();
    /// 
    /// Interaction.GetBehaviors(myScrollViewer).Add(new HorizontalScrollBehavior());
    /// </code>
    /// 
    /// # Attach in code-behind
    /// <code>
    /// using HorizontalScroll;
    /// using Microsoft.Xaml.Behaviors;
    /// 
    /// var myScrollViewer = new ScrollViewer();
    /// 
    /// Interaction.GetBehaviors(myScrollViewer).Add(new HorizontalScrollBehavior());
    /// </code>
    /// 
    /// # Attach to all <see cref="ScrollViewer"/> controls
    /// <code>
    /// &lt;Style TargetType="{x:Type ScrollViewer}"&gt;
    ///     &lt;EventSetter Event="Loaded" Handler="ScrollViewer_Loaded" /&gt;
    /// &lt;/Style&gt;
    /// </code>
    /// <c>ScrollViewer_Loaded</c> implementation:
    /// <code>
    /// private void ScrollViewer_Loaded(object sender, RoutedEventArgs e)
    /// {
    ///     Interaction.GetBehaviors((ScrollViewer)sender).Add(new HorizontalScrollBehavior());
    /// }
    /// </code>
    /// </example>
    public class HorizontalScrollBehavior : Behavior<ScrollViewer>
    {
        #region Constructors
        /// <summary>
        /// Creates a new <see cref="HorizontalScrollBehavior"/> instance with the default magnitude.
        /// </summary>
        public HorizontalScrollBehavior() { }
        /// <summary>
        /// Creates a new <see cref="HorizontalScrollBehavior"/> instance with the default magnitude.
        /// </summary>
        /// <param name="enableShiftScroll"><see langword="false"/> disables the Shift+ScrollWheel input method.</param>
        public HorizontalScrollBehavior(bool enableShiftScroll)
            => IsShiftScrollEnabled = enableShiftScroll;
        /// <summary>
        /// Creates a new <see cref="HorizontalScrollBehavior"/> instance with the specified <paramref name="magnitude"/>.
        /// </summary>
        /// <param name="magnitude">Value in the range 0.0-1.0 that determines how sensitive horizontal scrolling is. 0.0 is no sensitivity (disables scroll), 1.0 is max sensitivity.</param>
        public HorizontalScrollBehavior(double magnitude)
        {
            Magnitude = magnitude;
        }
        /// <summary>
        /// Creates a new <see cref="HorizontalScrollBehavior"/> instance with the specified <paramref name="magnitude"/>.
        /// </summary>
        /// <param name="magnitude">Value in the range 0.0-1.0 that determines how sensitive horizontal scrolling is. 0.0 is no sensitivity (disables scroll), 1.0 is max sensitivity.</param>
        /// <param name="enableShiftScroll"><see langword="false"/> disables the Shift+ScrollWheel input method.</param>
        public HorizontalScrollBehavior(double magnitude, bool enableShiftScroll) : this(magnitude)
            => IsShiftScrollEnabled = enableShiftScroll;
        /// <summary>
        /// Creates a new <see cref="HorizontalScrollBehavior"/> instance with the specified <paramref name="magnitudeBinding"/>.
        /// </summary>
        /// <param name="magnitudeBinding">A data binding to use for the Magnitude.</param>
        public HorizontalScrollBehavior(BindingBase magnitudeBinding)
        {
            BindingOperations.SetBinding(AssociatedObject, MagnitudeProperty, magnitudeBinding);
        }
        /// <summary>
        /// Creates a new <see cref="HorizontalScrollBehavior"/> instance with the specified <paramref name="magnitudeBinding"/>.
        /// </summary>
        /// <param name="magnitudeBinding">A data binding to use for the Magnitude.</param>
        /// <param name="enableShiftScroll"><see langword="false"/> disables the Shift+ScrollWheel input method.</param>
        public HorizontalScrollBehavior(BindingBase magnitudeBinding, bool enableShiftScroll) : this(magnitudeBinding)
            => IsShiftScrollEnabled = enableShiftScroll;
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets a value indicating whether holding Shift while scrolling up or down will scroll horizontally or vertically.
        /// </summary>
        /// <returns><see langword="true"/> when it scrolls horizontally; otherwise, <see langword="false"/> when it scrolls vertically.</returns>
        public virtual bool IsShiftScrollEnabled { get; } = true;
        #endregion Properties

        #region MagnitudeProperty
        /// <summary>
        /// The <see cref="DependencyProperty"/> for <see cref="Magnitude"/>.
        /// </summary>
        public static readonly DependencyProperty MagnitudeProperty = DependencyProperty.Register(
            nameof(Magnitude),
            typeof(double),
            typeof(HorizontalScrollBehavior),
            new PropertyMetadata(0.33, null, OnMagnitudePropertyCoerceValue));
        /// <summary>
        /// Gets or sets the magnitude that controls how far each user input will move the scrollbar.
        /// </summary>
        /// <returns>Scrolling magnitude as a <see cref="double"/> in the range 0.0-1.0, where 0 doesn't move the scrollbar &amp; 1 moves the scrollbar the full distance.</returns>
        /// <exception cref="ArgumentNullException">The specified value was <see langword="null"/>.</exception>
        public double Magnitude
        {
            get => (double)GetValue(MagnitudeProperty);
            set => SetValue(MagnitudeProperty, value);
        }
        private static object OnMagnitudePropertyCoerceValue(DependencyObject d, object value)
        {
            ArgumentNullException.ThrowIfNull(value);

            // clamp the value within the allowed range
            var magnitude = (double)value;

            if (magnitude < 0.0)
                magnitude = 0.0;
            else if (magnitude > 1.0)
                magnitude = 1.0;

            return magnitude;
        }
        #endregion MagnitudeProperty

        #region Behavior Method Overrides
        /// <inheritdoc/>
        protected override void OnAttached()
        {
            base.OnAttached();

            if (IsShiftScrollEnabled)
                AssociatedObject.PreviewMouseWheel += AssociatedObject_PreviewMouseWheel; //< this has to be PreviewMouseWheel
            HorizontalScroll.AddMouseWheelTiltHandler(AssociatedObject, AssociatedObject_PreviewMouseWheelTilt);
        }
        /// <inheritdoc/>
        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.PreviewMouseWheel -= AssociatedObject_PreviewMouseWheel; //< this has to be PreviewMouseWheel
            HorizontalScroll.RemoveMouseWheelTiltHandler(AssociatedObject, AssociatedObject_PreviewMouseWheelTilt);
        }
        #endregion Behavior Method Overrides

        #region EventHandlers
        private void AssociatedObject_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        { // handle regular mouse wheel events to check for Shift+Scroll
            if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
            {
                AssociatedObject.ScrollToHorizontalOffset(AssociatedObject.HorizontalOffset - e.Delta * Magnitude);
                e.Handled = true;
            }
        }
        private void AssociatedObject_PreviewMouseWheelTilt(object sender, MouseWheelEventArgs e)
        { // handle horizontal mouse wheel events
            AssociatedObject.ScrollToHorizontalOffset(AssociatedObject.HorizontalOffset - e.Delta * Magnitude);
            e.Handled = true;
        }
        #endregion EventHandlers
    }
}
