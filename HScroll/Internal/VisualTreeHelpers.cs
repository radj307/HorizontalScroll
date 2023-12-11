using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace HScroll.Internal
{
    static class VisualTreeHelpers
    {
        #region FindDescendantWithType
        /// <summary>
        /// Finds the descendant of the specified <paramref name="dependencyObject"/> with type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the target descendant to get.</typeparam>
        /// <param name="dependencyObject">The <see cref="DependencyObject"/> to find the descendant of.</param>
        /// <returns>The first <paramref name="dependencyObject"/> descendant of type <typeparamref name="T"/>.</returns>
        public static T? FindDescendantWithType<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            Queue<DependencyObject> queue = new();
            queue.Enqueue(dependencyObject);
            while (queue.Count > 0)
            {
                var d = queue.Dequeue();

                for (int i = 0, i_max = VisualTreeHelper.GetChildrenCount(dependencyObject);
                    i < i_max;
                    ++i)
                {
                    var child = VisualTreeHelper.GetChild(d, i);
                    if (child is T target) return target;
                    queue.Enqueue(child);
                }
            }
            return null;
        }
        /// <summary>
        /// Finds the descendant of the specified <paramref name="dependencyObject"/> with type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the target descendant to get.</typeparam>
        /// <param name="dependencyObject">The <see cref="DependencyObject"/> to find the descendant of.</param>
        /// <param name="maxDepth">The maximum number of ancestors to search through before returning.</param>
        /// <returns>The first <paramref name="dependencyObject"/> descendant of type <typeparamref name="T"/>.</returns>
        public static T? FindDescendantWithType<T>(DependencyObject dependencyObject, int maxDepth) where T : DependencyObject
        {
            if (maxDepth == -1) return FindDescendantWithType<T>(dependencyObject);

            Queue<(DependencyObject DependencyObject, int Depth)> queue = new();
            queue.Enqueue((dependencyObject, 0));
            while (queue.Count > 0)
            {
                var (currentObject, currentDepth) = queue.Dequeue();

                if (currentDepth < maxDepth)
                {
                    for (int i = 0, i_max = VisualTreeHelper.GetChildrenCount(dependencyObject);
                        i < i_max;
                        ++i)
                    {
                        var child = VisualTreeHelper.GetChild(currentObject, i);
                        if (child is T target) return target;
                        queue.Enqueue((child, currentDepth + 1));
                    }
                }
            }
            return null;
        }
        #endregion FindDescendantWithType

        #region FindAncestorWithType
        /// <summary>
        /// Finds the ancestor of the specified <paramref name="dependencyObject"/> with type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the ancestor <see cref="DependencyObject"/> to retrieve.</typeparam>
        /// <param name="dependencyObject">The <see cref="DependencyObject"/> to find the ancestor of.</param>
        /// <returns>The first ancestor of type <typeparamref name="T"/> when found; otherwise, <see langword="null"/>.</returns>
        public static T? FindAncestorWithType<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            for (DependencyObject? current = GetParent(dependencyObject); current != null; current = GetParent(current))
            {
                if (current is T ancestor)
                    return ancestor;
            }
            return null;
        }
        /// <summary>
        /// Finds the ancestor of the specified <paramref name="dependencyObject"/> with type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the ancestor <see cref="DependencyObject"/> to retrieve.</typeparam>
        /// <param name="dependencyObject">The <see cref="DependencyObject"/> to find the ancestor of.</param>
        /// <param name="maxDepth">The maximum number of ancestors to search through before returning.</param>
        /// <returns>The first ancestor of type <typeparamref name="T"/> when found; otherwise, <see langword="null"/>.</returns>
        public static T? FindAncestorWithType<T>(DependencyObject dependencyObject, int maxDepth) where T : DependencyObject
        {
            if (maxDepth == -1) return FindAncestorWithType<T>(dependencyObject);

            int i = 0;
            for (DependencyObject? current = GetParent(dependencyObject); current != null && i < maxDepth; current = GetParent(current), ++i)
            {
                if (current is T ancestor)
                    return ancestor;
            }
            return null;
        }
        #endregion FindAncestorWithType

        #region GetParent
        public static DependencyObject GetParent(DependencyObject d)
            => d is Visual || d is Visual3D
            ? VisualTreeHelper.GetParent(d)     //< is visual element
            : LogicalTreeHelper.GetParent(d);   //< is logical element
        #endregion GetParent
    }
}
