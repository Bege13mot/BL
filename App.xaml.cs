using System.Windows;
using BookLibrary.ViewModel;
using BookLibrary;

namespace BookLibrary
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var window = new Shell();
            var bookViewModel = new BookViewModel();
            bookViewModel.ProcessXml();
            window.DataContext = bookViewModel;
            window.Show();
        }
    }
}
