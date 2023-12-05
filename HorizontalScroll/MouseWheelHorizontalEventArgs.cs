using System;
using System.Windows.Input;

namespace HorizontalScroll
{
    /// <summary>
    /// Event arguments for horizontal mouse wheel events.
    /// </summary>
    public class MouseWheelHorizontalEventArgs : MouseEventArgs
    {
        #region Constructors
        /// <summary>
        /// Creates a new <see cref="MouseWheelHorizontalEventArgs"/> instance with the specified parameters.
        /// </summary>
        /// <param name="mouse">The mouse device associated with this event.</param>
        /// <param name="timestamp">The time when the input occurred.</param>
        /// <param name="horizontalDelta">The amount the wheel has changed.</param>
        public MouseWheelHorizontalEventArgs(MouseDevice mouse, int timestamp, int horizontalDelta) : base(mouse, timestamp)
        {
            HorizontalDelta = horizontalDelta;
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets a value that indicates the amount that the mouse wheel has changed.
        /// </summary>
        /// <returns>
        /// The amount the wheel has changed. This value is positive if the mouse wheel is 
        /// tilted to the left or negative if the mouse wheel is tilted to the right.
        /// </returns>
        public int HorizontalDelta { get; }
        #endregion Properties

        #region Methods
        /// <inheritdoc/>
        protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
            => ((MouseWheelHorizontalEventHandler)genericHandler)(genericTarget, this);
        #endregion Methods
    }
    /// <summary>
    /// Event handler type for the MouseWheelHorizontal &amp; PreviewMouseWheelHorizontal events.
    /// </summary>
    /// <param name="sender">The control that the user horizontally scrolled over top of.</param>
    /// <param name="e">Event arguments containing the distance scrolled, where </param>
    public delegate void MouseWheelHorizontalEventHandler(object sender, MouseWheelHorizontalEventArgs e);
}
