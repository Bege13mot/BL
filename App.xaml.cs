using System.Windows;
using BookLibrary.ViewModel;
using Test1.View;

namespace BookLibrary
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            var window = new MainWindow();
            var bookViewModel = new BookViewModel();
            bookViewModel.ProcessXml();
            window.DataContext = bookViewModel;
            window.Show();
        }
    }
}
