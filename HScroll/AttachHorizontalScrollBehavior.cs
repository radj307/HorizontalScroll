using HScroll.Internal;
using Microsoft.Xaml.Behaviors;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace HScroll
{
    /// <summary>
    /// <see cref="Control"/> behavior that searches the ancestors and/or descendents of the attached control for a <see cref="ScrollViewer"/> after it has loaded, then attaches a <see cref="HorizontalScrollBehavior"/> to it.
    /// </summary>
    /// <remarks>
    /// This is for special cases where you can't attach to a <see cref="ScrollViewer"/> in the usual manner, such as when using a View in a <see cref="ListView"/> control.
    /// </remarks>
    public class AttachHorizontalScrollBehavior : Behavior<Control>
    {
        #region Constructors

        #region HorizontalScrollBehavior
        /// <inheritdoc cref="HorizontalScrollBehavior()"/>
        public AttachHorizontalScrollBehavior()
        {
            Behavior = new();
        }
        /// <inheritdoc cref="HorizontalScrollBehavior(double)"/>
        public AttachHorizontalScrollBehavior(double magnitude)
        {
            Behavior = new(magnitude);
        }
        /// <inheritdoc cref="HorizontalScrollBehavior(BindingBase)"/>
        public AttachHorizontalScrollBehavior(BindingBase magnitudeBinding)
        {
            Behavior = new(magnitudeBinding);
        }
        /// <inheritdoc cref="HorizontalScrollBehavior(double, bool)"/>
        public AttachHorizontalScrollBehavior(double magnitude = 0.33, bool enableShiftScroll = true)
        {
            Behavior = new(magnitude, enableShiftScroll);
        }
        /// <inheritdoc cref="HorizontalScrollBehavior(BindingBase, bool)"/>
        public AttachHorizontalScrollBehavior(BindingBase magnitudeBinding, bool enableShiftScroll)
        {
            Behavior = new(magnitudeBinding, enableShiftScroll);
        }
        /// <inheritdoc cref="HorizontalScrollBehavior(double, BindingBase)"/>
        public AttachHorizontalScrollBehavior(double magnitude, BindingBase enableShiftScrollBinding)
        {
            Behavior = new(magnitude, enableShiftScrollBinding);
        }
        /// <inheritdoc cref="HorizontalScrollBehavior(BindingBase, BindingBase)"/>
        public AttachHorizontalScrollBehavior(BindingBase magnitudeBinding, BindingBase enableShiftScrollBinding)
        {
            Behavior = new(magnitudeBinding, enableShiftScrollBinding);
        }
        /// <inheritdoc cref="HorizontalScrollBehavior()"/>
        public AttachHorizontalScrollBehavior(HorizontalScrollBehavior behavior)
        {
            Behavior = behavior;
        }
        #endregion HorizontalScrollBehavior

        #region HorizontalScrollBehavior w/ SearchMode
        /// <inheritdoc cref="AttachHorizontalScrollBehavior"/>
        /// <param name="searchMode">Specifies whether to search the associated control's ancestors and/or descendants for a <see cref="ScrollViewer"/> to attach to.</param>
        public AttachHorizontalScrollBehavior(ESearchMode searchMode) : this()
            => SearchMode = searchMode;
        /// <inheritdoc cref="AttachHorizontalScrollBehavior(double)"/>
        /// <param name="searchMode">Specifies whether to search the associated control's ancestors and/or descendants for a <see cref="ScrollViewer"/> to attach to.</param>
        public AttachHorizontalScrollBehavior(double magnitude, ESearchMode searchMode) : this(magnitude)
            => SearchMode = searchMode;
        /// <inheritdoc cref="AttachHorizontalScrollBehavior(BindingBase)"/>
        /// <param name="searchMode">Specifies whether to search the associated control's ancestors and/or descendants for a <see cref="ScrollViewer"/> to attach to.</param>
        public AttachHorizontalScrollBehavior(BindingBase magnitudeBinding, ESearchMode searchMode) : this(magnitudeBinding)
            => SearchMode = searchMode;
        /// <inheritdoc cref="AttachHorizontalScrollBehavior(double, bool)"/>
        /// <param name="searchMode">Specifies whether to search the associated control's ancestors and/or descendants for a <see cref="ScrollViewer"/> to attach to.</param>
        public AttachHorizontalScrollBehavior(double magnitude = 0.33, bool enableShiftScroll = true, ESearchMode searchMode = ESearchMode.DescendantsFirst) : this(magnitude, enableShiftScroll)
            => SearchMode = searchMode;
        /// <inheritdoc cref="AttachHorizontalScrollBehavior(BindingBase, bool)"/>
        /// <param name="searchMode">Specifies whether to search the associated control's ancestors and/or descendants for a <see cref="ScrollViewer"/> to attach to.</param>
        public AttachHorizontalScrollBehavior(BindingBase magnitudeBinding, bool enableShiftScroll, ESearchMode searchMode) : this(magnitudeBinding, enableShiftScroll)
            => SearchMode = searchMode;
        /// <inheritdoc cref="AttachHorizontalScrollBehavior(double, BindingBase)"/>
        /// <param name="searchMode">Specifies whether to search the associated control's ancestors and/or descendants for a <see cref="ScrollViewer"/> to attach to.</param>
        public AttachHorizontalScrollBehavior(double magnitude, BindingBase enableShiftScrollBinding, ESearchMode searchMode) : this(magnitude, enableShiftScrollBinding)
            => SearchMode = searchMode;
        /// <inheritdoc cref="AttachHorizontalScrollBehavior(BindingBase, BindingBase)"/>
        /// <param name="searchMode">Specifies whether to search the associated control's ancestors and/or descendants for a <see cref="ScrollViewer"/> to attach to.</param>
        public AttachHorizontalScrollBehavior(BindingBase magnitudeBinding, BindingBase enableShiftScrollBinding, ESearchMode searchMode) : this(magnitudeBinding, enableShiftScrollBinding)
            => SearchMode = searchMode;
        /// <inheritdoc cref="AttachHorizontalScrollBehavior(HorizontalScrollBehavior)"/>
        /// <param name="searchMode">Specifies whether to search the associated control's ancestors and/or descendants for a <see cref="ScrollViewer"/> to attach to.</param>
        public AttachHorizontalScrollBehavior(HorizontalScrollBehavior behavior, ESearchMode searchMode) : this(behavior)
            => SearchMode = searchMode;
        #endregion HorizontalScrollBehavior w/ SearchMode

        #region HorizontalScrollBehavior w/ SearchMode & SearchDepth
        /// <inheritdoc cref="AttachHorizontalScrollBehavior(ESearchMode)"/>
        /// <param name="searchDepth">Specifies the maximum number of ancestor or descendant controls to check before returning <see langword="null"/>.</param>
        public AttachHorizontalScrollBehavior(ESearchMode searchMode, int searchDepth) : this(searchMode)
            => SearchDepth = searchDepth;
        /// <inheritdoc cref="AttachHorizontalScrollBehavior(double, ESearchMode)"/>
        public AttachHorizontalScrollBehavior(double magnitude, ESearchMode searchMode, int searchDepth) : this(magnitude, searchMode)
            => SearchDepth = searchDepth;
        /// <inheritdoc cref="AttachHorizontalScrollBehavior(BindingBase, ESearchMode)"/>
        public AttachHorizontalScrollBehavior(BindingBase magnitudeBinding, ESearchMode searchMode, int searchDepth) : this(magnitudeBinding, searchMode)
            => SearchDepth = searchDepth;
        /// <inheritdoc cref="AttachHorizontalScrollBehavior(double, bool, ESearchMode)"/>
        public AttachHorizontalScrollBehavior(double magnitude = 0.33, bool enableShiftScroll = true, ESearchMode searchMode = ESearchMode.DescendantsFirst, int searchDepth = -1) : this(magnitude, enableShiftScroll, searchMode)
            => SearchDepth = searchDepth;
        /// <inheritdoc cref="AttachHorizontalScrollBehavior(BindingBase, bool, ESearchMode)"/>
        public AttachHorizontalScrollBehavior(BindingBase magnitudeBinding, bool enableShiftScroll, ESearchMode searchMode, int searchDepth) : this(magnitudeBinding, enableShiftScroll, searchMode)
            => SearchDepth = searchDepth;
        /// <inheritdoc cref="AttachHorizontalScrollBehavior(double, BindingBase, ESearchMode)"/>
        public AttachHorizontalScrollBehavior(double magnitude, BindingBase enableShiftScrollBinding, ESearchMode searchMode, int searchDepth) : this(magnitude, enableShiftScrollBinding, searchMode)
            => SearchDepth = searchDepth;
        /// <inheritdoc cref="AttachHorizontalScrollBehavior(BindingBase, BindingBase, ESearchMode)"/>
        public AttachHorizontalScrollBehavior(BindingBase magnitudeBinding, BindingBase enableShiftScrollBinding, ESearchMode searchMode, int searchDepth) : this(magnitudeBinding, enableShiftScrollBinding, searchMode)
            => SearchDepth = searchDepth;
        /// <inheritdoc cref="AttachHorizontalScrollBehavior(HorizontalScrollBehavior, ESearchMode)"/>
        /// <param name="searchMode">Specifies whether to search the associated control's ancestors and/or descendants for a <see cref="ScrollViewer"/> to attach to.</param>
        public AttachHorizontalScrollBehavior(HorizontalScrollBehavior behavior, ESearchMode searchMode, int searchDepth) : this(behavior, searchMode)
            => SearchDepth = searchDepth;
        #endregion HorizontalScrollBehavior w/ SearchMode & SearchDepth

        #endregion Constructors

        #region Fields
        /// <summary>
        /// The underlying <see cref="HorizontalScrollBehavior"/> instance to attach to the target <see cref="ScrollViewer"/> control.
        /// </summary>
        public readonly HorizontalScrollBehavior Behavior;
        private bool _isAttached = false;
        #endregion Fields

        #region Properties
        /// <summary>
        /// Gets or sets the search mode to use when searching for the target <see cref="ScrollViewer"/> control to attach the behavior to. The default mode is <see cref="ESearchMode.DescendantsFirst"/>.
        /// </summary>
        /// <remarks>
        /// This can't be changed when the behavior is attached.
        /// </remarks>
        /// <exception cref="InvalidEnumArgumentException">The specified value was not valid for the <see cref="ESearchMode"/> enum type.</exception>
        public ESearchMode SearchMode
        {
            get => _searchMode;
            set
            {
                if (!value.IsValidValue())
                    throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(ESearchMode));

                _searchMode = value;
            }
        }
        private ESearchMode _searchMode = ESearchMode.DescendantsFirst;
        /// <summary>
        /// Gets or sets the maximum search depth when searching for the target <see cref="ScrollViewer"/> control to attach the behavior to. The default value is -1.
        /// </summary>
        /// <remarks>
        /// This can't be changed when the behavior is attached.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">The specified value was zero, or less than -1.</exception>
        /// <returns>An <see cref="int"/> value greater than zero when a search depth was specified; otherwise, -1.</returns>
        public int SearchDepth
        {
            get => _searchDepth;
            set
            {
                if (_isAttached)
                    throw new InvalidOperationException($"The value of {nameof(AttachHorizontalScrollBehavior)}.{nameof(SearchDepth)} cannot be changed when the behavior is attached!");

                if (value < -1 || value == 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, $"{nameof(AttachHorizontalScrollBehavior)}.{nameof(SearchDepth)} expects a value greater than zero, or -1; Actual value was {value}!");

                _searchDepth = value;
            }
        }
        private int _searchDepth = -1;
        /// <summary>
        /// Gets or sets a value that determines if a <see cref="ScrollViewerNotFoundException"/> is thrown when a <see cref="ScrollViewer"/> wasn't found. The default value is <see langword="true"/>.
        /// </summary>
        /// <returns><see langword="true"/> when search failures throw a <see cref="ScrollViewerNotFoundException"/>; otherwise, <see langword="false"/> when searches fail silently.</returns>
        public bool ThrowOnSearchFailed { get; set; } = true;
        #endregion Properties

        #region Behavior Method Overrides
        protected override void OnAttached()
        {
            if (AssociatedObject.GetType().Equals(typeof(ScrollViewer)))
                throw new InvalidOperationException($"{typeof(AttachHorizontalScrollBehavior)} cannot be directly attached to a {nameof(ScrollViewer)}; use {nameof(HorizontalScrollBehavior)} instead!");

            if (AssociatedObject.IsLoaded)
                AttachToAssociatedObject();
            else // attach once the control has loaded
                AssociatedObject.Loaded += AssociatedObject_Loaded;
        }
        protected override void OnDetaching()
        {
            AssociatedObject.Loaded -= AssociatedObject_Loaded;
            Behavior.Detach();

            _isAttached = false;
        }
        #endregion Behavior Method Overrides

        #region Methods
        private ScrollViewer? FindTargetScrollViewerControl()
            => SearchMode switch
            {
                ESearchMode.DescendantsFirst => VisualTreeHelpers.FindDescendantWithType<ScrollViewer>(AssociatedObject, SearchDepth) ?? VisualTreeHelpers.FindAncestorWithType<ScrollViewer>(AssociatedObject, SearchDepth),
                ESearchMode.AncestorsFirst => VisualTreeHelpers.FindAncestorWithType<ScrollViewer>(AssociatedObject, SearchDepth) ?? VisualTreeHelpers.FindDescendantWithType<ScrollViewer>(AssociatedObject, SearchDepth),
                ESearchMode.DescendantsOnly => VisualTreeHelpers.FindDescendantWithType<ScrollViewer>(AssociatedObject, SearchDepth),
                ESearchMode.AncestorsOnly => VisualTreeHelpers.FindAncestorWithType<ScrollViewer>(AssociatedObject, SearchDepth),
                _ => null,
            };
        private void AttachToAssociatedObject()
        {
            var scrollViewer = FindTargetScrollViewerControl() ?? throw new InvalidOperationException($"{nameof(AttachHorizontalScrollBehavior)} couldn't find any {nameof(ScrollViewer)} controls off {AssociatedObject} using search mode \"{SearchMode:G}\"!");
            Behavior.Attach(scrollViewer);

            _isAttached = true;
        }
        #endregion Methods

        #region EventHandlers
        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e) => AttachToAssociatedObject();
        #endregion EventHandlers
    }
}
