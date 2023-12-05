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
        /// Creates a new <see cref="HorizontalScrollBehavior"/> instance with the specified <paramref name="magnitude"/>.
        /// </summary>
        /// <param name="magnitude"></param>
        public HorizontalScrollBehavior(double magnitude)
        {
            Magnitude = magnitude;
        }
        /// <summary>
        /// Creates a new <see cref="HorizontalScrollBehavior"/> instance with the specified <paramref name="magnitudeBinding"/>.
        /// </summary>
        /// <param name="magnitudeBinding">A data binding to use for the Magnitude.</param>
        public HorizontalScrollBehavior(BindingBase magnitudeBinding)
        {
            BindingOperations.SetBinding(AssociatedObject, MagnitudeProperty, magnitudeBinding);
        }
        #endregion Constructors

        #region MagnitudeProperty
        /// <summary>
        /// The <see cref="DependencyProperty"/> for <see cref="Magnitude"/>.
        /// </summary>
        public static readonly DependencyProperty MagnitudeProperty = DependencyProperty.Register(
            nameof(Magnitude),
            typeof(double),
            typeof(HorizontalScrollBehavior),
            new PropertyMetadata(0.33, null, MagnitudePropertyCoerceValue));
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
        private static object MagnitudePropertyCoerceValue(DependencyObject d, object value)
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

            AssociatedObject.PreviewMouseWheel += AssociatedObject_PreviewMouseWheel;
            HorizontalScroll.AddPreviewMouseWheelTiltHandler(AssociatedObject, AssociatedObject_PreviewMouseWheelHorizontal);
        }
        /// <inheritdoc/>
        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.PreviewMouseWheel -= AssociatedObject_PreviewMouseWheel;
            HorizontalScroll.RemovePreviewMouseWheelTiltHandler(AssociatedObject, AssociatedObject_PreviewMouseWheelHorizontal);
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
        private void AssociatedObject_PreviewMouseWheelHorizontal(object sender, MouseWheelEventArgs e)
        { // handle horizontal mouse wheel events
            AssociatedObject.ScrollToHorizontalOffset(AssociatedObject.HorizontalOffset - e.Delta * Magnitude);
            e.Handled = true;
        }
        #endregion EventHandlers
    }
}
