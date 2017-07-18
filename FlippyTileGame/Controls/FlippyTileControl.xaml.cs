using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FlippyTileGame.Controls
{
    /// <summary>
    /// Interaction logic for FlippyTileControl.xaml
    /// </summary>
    public partial class FlippyTileControl : UserControl
    {
        public FlippyTileControl()
        {
            InitializeComponent();
            ImageGrid.Visibility = Visibility.Hidden;
            Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            await Task.Delay(1100);
            ImageGrid.Visibility = Visibility.Visible;
        }
    }
}
