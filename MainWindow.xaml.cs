using System.Windows;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using BookLibrary.View;

namespace BookLibrary
{
    /// <summary>
    /// Логика взаимодействия с MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;    
            
        }

        private static void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var rm = ServiceLocator.
                Current.GetInstance<IRegionManager>();
            var rgn = rm.Regions["MainRegion"];
            rgn.Add(new List());
        }
        
    }
}
