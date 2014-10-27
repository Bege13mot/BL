using System.Windows;
using BookLibrary.ViewModel;
using Microsoft.Practices.Prism.MefExtensions;

namespace BookLibrary
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MefBootstrapper _bootstrapper;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _bootstrapper = new Boots();
            _bootstrapper.Run();
        }
    }

    public class Boots : MefBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return new MainWindow();
        }

        protected override void InitializeShell()
        {
            var window = Application.Current.MainWindow = Shell as Window;
            var bookViewModel = new BookViewModel();
            bookViewModel.ProcessXml();
            window.DataContext = bookViewModel;
            window.Show();
        }
    }
}
