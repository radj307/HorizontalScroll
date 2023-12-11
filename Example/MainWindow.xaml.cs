using HScroll;
using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;

namespace Example
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ScrollViewer_Loaded(object sender, RoutedEventArgs e)
        {
            // add HorizontalScrollBehavior directly to the ScrollViewer
            Interaction
                .GetBehaviors((ScrollViewer)sender)
                .Add(new HorizontalScrollBehavior());
        }
        private void ListView_Loaded(object sender, RoutedEventArgs e)
        {
            var listView = (ListView)sender;

            // add AttachHorizontalScrollBehavior
            Interaction
                .GetBehaviors((ListView)sender)
                .Add(new AttachHorizontalScrollBehavior());
        }
    }
}
