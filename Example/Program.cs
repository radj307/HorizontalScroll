using System;
using System.Windows;

namespace Example
{
    static class Program
    {
        [STAThread]
        static int Main() => new Application().Run(new MainWindow());
    }
}
