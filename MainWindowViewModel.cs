using MP3_V2;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System;
using MP3_V2.Services;
using System.Windows.Input;
using Microsoft.Win32;
using System.IO;
using TagLib;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private readonly IMusicLibrary _musicLibrary;
    private readonly IMusicPlayer _musicPlayer;
    private readonly PlayerState _playerState;
    private readonly MusicPlayerController _playerController;

    public ICommand AddSongsCommand { get; }
    public ICommand PlayCommand { get; }
    public ICommand PauseCommand { get; }
    public ICommand NextCommand { get; }
    public ICommand PreviousCommand { get; }

    public ObservableCollection<Song> Songs => _musicLibrary.Songs;
    public ObservableCollection<Song> FavoriteSongs { get; set; }

    private Song _selectedSong;
    public Song SelectedSong
    {
        get => _selectedSong;
        set
        {
            if (_selectedSong != value)
            {
                _selectedSong = value;
                OnPropertyChanged(nameof(SelectedSong));
                CommandManager.InvalidateRequerySuggested(); // Оновлюємо команди
            }
        }
    }

    public MainWindowViewModel(IMusicLibrary musicLibrary, IMusicPlayer musicPlayer)
    {
        _musicLibrary = musicLibrary ?? throw new ArgumentNullException(nameof(musicLibrary));
        _musicPlayer = musicPlayer ?? throw new ArgumentNullException(nameof(musicPlayer));

        _playerState = new PlayerState();
        _playerController = new MusicPlayerController(_playerState, _musicLibrary, _musicPlayer);

        FavoriteSongs = new ObservableCollection<Song>();

        AddSongsCommand = new RelayCommand(_ => AddSongs());
        PlayCommand = new RelayCommand(Play, () => SelectedSong != null);
        PauseCommand = new RelayCommand(
            _ => _playerController.Pause(),
            () => _playerState.CurrentState == PlaybackState.Playing
        );
        NextCommand = new RelayCommand(
            _ => _playerController.Next(),
            () => _playerState.CurrentSong != null &&
                 _musicLibrary.Songs.IndexOf(_playerState.CurrentSong) < _musicLibrary.Songs.Count - 1
        );
        PreviousCommand = new RelayCommand(
            _ => _playerController.Previous(),
            () => _playerState.CurrentSong != null &&
                 _musicLibrary.Songs.IndexOf(_playerState.CurrentSong) > 0
        );
    }

    private void AddSongs()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            Filter = "MP3 Files (*.mp3)|*.mp3|All Files (*.*)|*.*",
            Multiselect = true
        };

        if (openFileDialog.ShowDialog() == true)
        {
            foreach (var filePath in openFileDialog.FileNames)
            {
                var file = TagLib.File.Create(filePath);
                var newSong = new Song(
                    file.Tag.Title ?? Path.GetFileNameWithoutExtension(filePath),
                    file.Tag.Performers.Length > 0 ? file.Tag.Performers[0] : null,
                    file.Tag.Pictures.Length > 0 ? "path/to/album/art" : null,
                    filePath,
                    false
                );

                _musicLibrary.AddSong(newSong);
            }
        }
    }

    private void Play()
    {
        if (SelectedSong != null)
        {
            _playerController.Play(SelectedSong);
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
