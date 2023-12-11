using System;
using System.Windows.Controls;

namespace HScroll
{
    /// <summary>
    /// Represents an error that occurs when a <see cref="AttachHorizontalScrollBehavior"/> fails to find a <see cref="ScrollViewer"/> with the specified search parameters.
    /// </summary>
    public sealed class ScrollViewerNotFoundException : Exception
    {
        #region Constructors
        internal ScrollViewerNotFoundException(Control associatedObject, ESearchMode searchMode, int searchDepth, Exception? innerException = null)
            : base($"Failed to find a {nameof(ScrollViewer)} control off of {associatedObject} using search mode \"{searchMode:G}\"{(searchDepth != -1 ? $" to a max depth of {searchDepth}" : string.Empty)}!", innerException)
        {
            AssociatedObject = associatedObject;
            SearchMode = searchMode;
            SearchDepth = searchDepth;
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets the control instance associated with the <see cref="AttachHorizontalScrollBehavior"/> with invalid parameters.
        /// </summary>
        public Control AssociatedObject { get; }
        /// <summary>
        /// Gets the search mode that caused the exception.
        /// </summary>
        public ESearchMode SearchMode { get; }
        /// <summary>
        /// Gets the search depth that caused the exception.
        /// </summary>
        public int SearchDepth { get; }
        #endregion Properties
    }
}
