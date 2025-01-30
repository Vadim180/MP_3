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
    private readonly PlayerState _playerState;
    private readonly MusicPlayerController _playerController;

    public ObservableCollection<Song> Songs => _musicLibrary.Songs;
    public ObservableCollection<Song> FavoriteSongs { get; set; }
    public MusicLibrary MusicLibrary { get; private set; }
    public ICommand AddSongsCommand { get; }
    public RelayCommand PlayCommand { get; }
    public RelayCommand PauseCommand { get; }
    public RelayCommand NextCommand { get; }
    public RelayCommand PreviousCommand { get; }

    private Song _selectedSong;

    public MainWindowViewModel(IMusicLibrary musicLibrary)
    {
        _musicLibrary = musicLibrary ?? throw new ArgumentNullException(nameof(musicLibrary));
        _playerState = new PlayerState();
        _playerController = new MusicPlayerController(_playerState, _musicLibrary);

        FavoriteSongs = new ObservableCollection<Song>();

        AddSongsCommand = new RelayCommand(_ => AddSongs());
        MusicLibrary = (MusicLibrary)musicLibrary;

        _musicLibrary = musicLibrary ?? throw new ArgumentNullException(nameof(musicLibrary));
        AddSongsCommand = new RelayCommand(_ => AddSongs());

        _playerState = new PlayerState();
        _playerController = new MusicPlayerController(_playerState, _musicLibrary);

        FavoriteSongs = new ObservableCollection<Song>();

        PlayCommand = new RelayCommand(
            _ => _playerController.Play(_playerState.CurrentSong),
            _ => _playerState.CurrentSong != null
        );

        PauseCommand = new RelayCommand(
            _ => _playerController.Pause(),
            _ => _playerState.CurrentState == PlaybackState.Playing
        );

        NextCommand = new RelayCommand(
            _ => _playerController.Next(),
            _ => _playerState.CurrentSong != null &&
                 _musicLibrary.Songs.IndexOf(_playerState.CurrentSong) < _musicLibrary.Songs.Count - 1
        );

        PreviousCommand = new RelayCommand(
            _ => _playerController.Previous(),
            _ => _playerState.CurrentSong != null &&
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
                    false // Можете встановити за замовчуванням або отримати це значення
                );

                MusicLibrary.AddSong(newSong); // Використовуємо існуючий метод
            }
        }
    }

    public Song SelectedSong
    {
        get { return _selectedSong; }
        set
        {
            if (_selectedSong != value)
            {
                _selectedSong = value;
                OnPropertyChanged(nameof(SelectedSong)); // Сповіщаємо про зміни
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
