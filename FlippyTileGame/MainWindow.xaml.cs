using FlippyTileGame.ViewModel;
using System.Diagnostics;
using System.Windows;

namespace FlippyTileGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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