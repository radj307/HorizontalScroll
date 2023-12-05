using HorizontalScroll;
using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;

namespace Example
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void ScrollViewer_Loaded(object sender, RoutedEventArgs e)
        {
            Interaction.GetBehaviors((ScrollViewer)sender).Add(new ScrollViewerHorizontalScrollBehavior());
        }
    }
}
