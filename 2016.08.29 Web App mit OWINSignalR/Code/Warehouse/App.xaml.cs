using System.Windows;
using Warehouse.Model;

namespace Warehouse
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var wnd = new MainWindow();
            var model = new MainModel();
            wnd.DataContext = model;
            wnd.Show();
        }
    }

}
