using System.Windows;
using ArchiverApp.ViewModel;

namespace ArchiverApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindow window = new MainWindow();
            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();
            window.DataContext = mainWindowViewModel;
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Show();
        }
    }
}
