using FlippyTileGame.ViewModel;
using System.Diagnostics;
using System.Windows;
using FlippyTileGame.DataServiceInterfaces;
using FlippyTileGame.DataServices;

namespace FlippyTileGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly IRegistrationDataService _dataService;
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {

            InitializeComponent();
            SizeChanged += OnLoaded;
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            Debug.WriteLine(ActualWidth + " " + ActualHeight);
        }
    }
}