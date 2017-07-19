using System.IO;
using System.Net.Mime;
using System.Threading;
using System.Windows;
using FlippyTileGame.DataServiceInterfaces;
using FlippyTileGame.Settings;
using GalaSoft.MvvmLight;
using Microsoft.VisualBasic;

namespace FlippyTileGame.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {

        private readonly ITileGameDataService _dataService;
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(ITileGameDataService dataService)
        {
            _dataService = dataService;
            

        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}