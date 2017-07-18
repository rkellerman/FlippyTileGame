using FlippyTileGame.Model;
using GalaSoft.MvvmLight;

namespace FlippyTileGame.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class FlippyTileViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the FlippyTileViewModel class.
        /// </summary>
        public FlippyTileViewModel()
        {
        }

        public FlippyTileViewModel(FlippyTileModel model)
        {
            PairId = model.PairId;
            ImagePath = model.ImagePath;
        }

        private int _pairId;

        public int PairId
        {
            get { return _pairId; }
            set
            {
                if (Equals(_pairId, value))
                {
                    return;
                }
                _pairId = value;
                RaisePropertyChanged();
            }
        }

        private string _imagePath;

        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                if (Equals(_imagePath, value))
                {
                    return;
                }
                _imagePath = value;
                RaisePropertyChanged();
            }
        }

        private bool _isFlipped;

        public bool IsFlipped
        {
            get { return _isFlipped; }
            set
            {
                if (Equals(_isFlipped, value))
                {
                    return;
                }
                _isFlipped = value;
                RaisePropertyChanged();
            }
        }



    }
}