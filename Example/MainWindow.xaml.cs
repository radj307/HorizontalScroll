using HorizontalScroll;
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

        private void ScrollViewer_MouseWheelHorizontal(object sender, MouseWheelHorizontalEventArgs e)
        {
            // do stuff
        }
    }
}
