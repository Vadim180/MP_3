using MP3_V2.Services;
using System;
using System.Windows;
using System.Windows.Controls;


namespace MP3_V2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var musicLibrary = new MusicLibrary();
            DataContext = new MainWindowViewModel(musicLibrary);
        }

        private void AddToFavorites_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as MainWindowViewModel;
            if (viewModel != null && viewModel.SelectedSong != null)
            {
                viewModel.SelectedSong.IsFavorite = true;
                viewModel.FavoriteSongs.Add(viewModel.SelectedSong);
            }
        }



        private void OpenEqualizerSettings_Click(object sender, RoutedEventArgs e)
        {
            // Логіка відкриття налаштувань еквалайзера
        }

        private void PreviousSong_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as MainWindowViewModel;
            int currentIndex = viewModel.Songs.IndexOf(viewModel.SelectedSong);
            if (currentIndex > 0)
            {
                viewModel.SelectedSong = viewModel.Songs[currentIndex - 1];
            }
        }

        private void PlayPause_Click(object sender, RoutedEventArgs e)
        {
            // Логіка паузи/продовження відтворення
        }

        private void NextSong_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as MainWindowViewModel;
            int currentIndex = viewModel.Songs.IndexOf(viewModel.SelectedSong);
            if (currentIndex < viewModel.Songs.Count - 1)
            {
                viewModel.SelectedSong = viewModel.Songs[currentIndex + 1];
            }
        }

        private void TogglePlayMode_Click(object sender, RoutedEventArgs e)
        {
            // Логіка перемикання режиму відтворення (по колу, списком, рандом)
        }
    }
}
