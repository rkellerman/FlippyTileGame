using FlippyTileGame.DataServiceInterfaces;
using FlippyTileGame.ExtensionMethods;
using FlippyTileGame.Model;
using FlippyTileGame.Settings;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace FlippyTileGame.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class TileGameViewModel : ViewModelBase
    {

        private readonly ITileGameDataService _dataService;
        private DispatcherTimer CountdownTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        private Stopwatch GameStopWatch = new Stopwatch();

        private static Random rng = new Random();

        public static void Shuffle<T>(List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public TileGameViewModel(ITileGameDataService dataService)
        {
            _dataService = dataService;
            CountdownTimer.Tick += CountdownTimerOnTick;

        }

        private void CountdownTimerOnTick(object sender, EventArgs eventArgs)
        {
            var totalSeconds = FlippyTileGameSettings.MaxGameTime.TotalSeconds;
            var elapsedSeconds = GameStopWatch.Elapsed.TotalSeconds;
            var timeLeft = Math.Round(totalSeconds - elapsedSeconds);

            if ((int)timeLeft == 0)
            {
                MessageBox.Show("Dude.");
                GameStopWatch.Stop();
                CountdownTimer.Stop();
                HasLost = true;
            }

            CountdownTimerText = timeLeft.ToString(CultureInfo.InvariantCulture);
        }

        private string _countdownTimerText = ((int)FlippyTileGameSettings.MaxGameTime.TotalSeconds).ToString();

        public string CountdownTimerText
        {
            get { return _countdownTimerText; }
            set
            {
                if (Equals(_countdownTimerText, value))
                {
                    return;
                }
                _countdownTimerText = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<FlippyTileViewModel> _tilesCollection;

        public ObservableCollection<FlippyTileViewModel> TilesCollection
        {
            get { return _tilesCollection; }
            set
            {
                if (Equals(_tilesCollection, value))
                {
                    return;
                }
                _tilesCollection = value;
                RaisePropertyChanged();
            }
        }

        private FlippyTileViewModel _lastFlippedFlippyTile = null;

        public FlippyTileViewModel LastFlippedFlippyTile
        {
            get { return _lastFlippedFlippyTile; }
            set
            {
                if (Equals(_lastFlippedFlippyTile, value))
                {
                    return;
                }
                _lastFlippedFlippyTile = value;
                RaisePropertyChanged();
            }
        }

        private bool _isPlaying = false;

        public bool IsPlaying
        {
            get { return _isPlaying; }
            set
            {
                if (Equals(_isPlaying, value))
                {
                    return;
                }
                _isPlaying = value;
                RaisePropertyChanged();
            }
        }

        private bool _hasWon = false;

        public bool HasWon
        {
            get { return _hasWon; }
            set
            {
                if (Equals(_hasWon, value))
                {
                    return;
                }
                _hasWon = value;
                RaisePropertyChanged();
            }
        }

        private bool _hasLost = false;

        public bool HasLost
        {
            get { return _hasLost; }
            set
            {
                if (Equals(_hasLost, value))
                {
                    return;
                }
                _hasLost = value;
                RaisePropertyChanged();
            }
        }




        private RelayCommand _newGameCommand;
        public ICommand NewGameCommand => _newGameCommand ?? (_newGameCommand = new RelayCommand(NewGame));

        protected void NewGame()
        {

            IsPlaying = true;
            HasLost = false;
            HasWon = false;

            LastFlippedFlippyTile = null;

            CountdownTimerText = ((int)FlippyTileGameSettings.MaxGameTime.TotalSeconds).ToString();
            GameStopWatch.Restart();
            CountdownTimer.Stop();
            CountdownTimer.Start();

            // get new tiles and set up game
            var tileModels = _dataService.GeTileModels();
            var tileViewModels = new List<FlippyTileViewModel>();

            foreach (var model in tileModels)
            {
                tileViewModels.Add(new FlippyTileViewModel(model));
                tileViewModels.Add(new FlippyTileViewModel(model));
            }

            tileViewModels.Shuffle();
            TilesCollection = new ObservableCollection<FlippyTileViewModel>(tileViewModels);
        }

        private RelayCommand _giveUpCommand;
        public ICommand GiveUpCommand => _giveUpCommand ?? (_giveUpCommand = new RelayCommand(GiveUp, CanGiveUp));

        protected void GiveUp()
        {

            foreach (var flippyTile in TilesCollection)
            {
                flippyTile.IsFlipped = true;
            }
            CountdownTimer.Stop();
            CountdownTimerText = "00";
            IsPlaying = false;
            HasLost = true;
        }

        private bool CanGiveUp()
        {
            return IsPlaying;
        }

        private RelayCommand<FlippyTileViewModel> _flipTileCommand;
        public ICommand FlipTileCommand => _flipTileCommand ?? (_flipTileCommand = new RelayCommand<FlippyTileViewModel>(FlipTile, CanFlipTile));

        protected void FlipTile(FlippyTileViewModel viewModel)
        {
            viewModel.IsFlipped = !viewModel.IsFlipped;

            if (LastFlippedFlippyTile == null)
            {
                LastFlippedFlippyTile = viewModel;
            }
            else
            {
                if (viewModel.PairId != LastFlippedFlippyTile.PairId)
                {
                    viewModel.IsFlipped = false;
                    LastFlippedFlippyTile.IsFlipped = false;


                }
                LastFlippedFlippyTile = null;
            }

            if (TilesCollection.Count(t => t.IsFlipped) == TilesCollection.Count)
            {
                CountdownTimer.Stop();
                GameStopWatch.Stop();
                IsPlaying = false;
                HasWon = true;
                CheckLeaderboard();
            }
        }

        private bool CanFlipTile(FlippyTileViewModel viewModel)
        {
            if (viewModel == null)
            {
                return false;
            }
            return !viewModel.IsFlipped;
        }

        private void CheckLeaderboard()
        {
            List<LeaderboardEntry> leaderboardEntries = new List<LeaderboardEntry>();

            if (File.Exists(FlippyTileGameSettings.LeaderBoardPath))
            {
                var lines = File.ReadAllLines(FlippyTileGameSettings.LeaderBoardPath);
                foreach (var line in lines)
                {
                    var entry = line.Split(' ');
                    LeaderboardEntry leaderboardEntry = new LeaderboardEntry
                    {
                        FullName = entry[0] + " " + entry[1],
                        CompletionTime = Int32.Parse(entry[2])

                    };

                    leaderboardEntries.Add(leaderboardEntry);
                }
            }

            var newLeaderBoardEntries = new List<LeaderboardEntry>();
            int CompletionTime = 60 - Int32.Parse(CountdownTimerText);
            var DidTheThing = false;
            for (int i = 0; i < leaderboardEntries.Count; i++)
            {
                if (leaderboardEntries[i].CompletionTime < CompletionTime || DidTheThing)
                {
                    if (DidTheThing)
                    {
                        newLeaderBoardEntries.Add(leaderboardEntries[i - 1]);
                    }
                    else
                    {
                        newLeaderBoardEntries.Add(leaderboardEntries[i]);
                    }

                }
                else
                {
                    var textNoSpaces = "";
                    var test = "";
                    do
                    {
                        test = Interaction.InputBox("You've made the leaderboard!  Please enter your full name:").Trim();
                        textNoSpaces = test.Replace(" ", "").Replace("'", "");

                    } while (test.Split(' ').Length != 2 || !textNoSpaces.All(char.IsLetterOrDigit));

                    LeaderboardEntry newEntry = new LeaderboardEntry
                    {
                        FullName = test,
                        CompletionTime = CompletionTime
                    };

                    newLeaderBoardEntries.Add(newEntry);
                    DidTheThing = true;
                }
            }

            string message = "";
            for (int i = 1; i < 6; i++)
            {
                message += i + ".  " + newLeaderBoardEntries[i - 1].FullName + ":  " +
                           newLeaderBoardEntries[i - 1].CompletionTime + " seconds\n";
            }

            MessageBox.Show("Leaderboard:\n\n" + message);

            var newLeaderboard = "";

            foreach (var entry in newLeaderBoardEntries)
            {
                newLeaderboard += entry.FullName + " " + entry.CompletionTime + "\n";
            }

            System.IO.File.WriteAllText(FlippyTileGameSettings.LeaderBoardPath, newLeaderboard);


        }





    }
}