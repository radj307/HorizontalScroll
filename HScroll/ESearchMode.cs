using System.Windows.Controls;

namespace HScroll
{
    /// <summary>
    /// Defines the modes of searching the visual tree for a <see cref="ScrollViewer"/> control.
    /// </summary>
    /// <remarks>
    /// Used by <see cref="AttachHorizontalScrollBehavior"/>.
    /// </remarks>
    public enum ESearchMode : byte
    {
        /// <summary>
        /// Searches the attached control's descendants before searching through its ancestors.
        /// </summary>
        DescendantsFirst = 0,
        /// <summary>
        /// Searches the attached control's ancestors before searching through its descendants.
        /// </summary>
        AncestorsFirst = 1,
        /// <summary>
        /// Searches the attached control's descendants only.
        /// </summary>
        DescendantsOnly = 2,
        /// <summary>
        /// Searches the attached control's ancestors only.
        /// </summary>
        AncestorsOnly = 3,
    }
}
