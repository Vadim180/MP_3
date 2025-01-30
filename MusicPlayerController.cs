using MP3_V2;
using System;

public class MusicPlayerController
{
    private readonly PlayerState _playerState;
    private readonly IMusicLibrary _musicLibrary;

    public MusicPlayerController(PlayerState playerState, IMusicLibrary musicLibrary)
    {
        _playerState = playerState ?? throw new ArgumentNullException(nameof(playerState));
        _musicLibrary = musicLibrary ?? throw new ArgumentNullException(nameof(musicLibrary));
    }

    public void Play(Song song)
    {
        if (song == null)
            throw new ArgumentNullException(nameof(song));

        _playerState.CurrentSong = song;
        _playerState.CurrentState = PlaybackState.Playing;

        // Логіка для запуску відтворення
        Console.WriteLine($"Now playing: {song.Title}");
    }

    public void Pause()
    {
        if (_playerState.CurrentState == PlaybackState.Playing)
        {
            _playerState.CurrentState = PlaybackState.Paused;
            // Логіка для паузи
            Console.WriteLine("Playback paused.");
        }
    }

    public void Stop()
    {
        _playerState.CurrentState = PlaybackState.Stopped;
        _playerState.CurrentTime = TimeSpan.Zero;

        // Логіка для зупинки
        Console.WriteLine("Playback stopped.");
    }

    public void Next()
    {
        var currentIndex = _musicLibrary.Songs.IndexOf(_playerState.CurrentSong);
        if (currentIndex < _musicLibrary.Songs.Count - 1)
        {
            Play(_musicLibrary.Songs[currentIndex + 1]);
        }
    }

    public void Previous()
    {
        var currentIndex = _musicLibrary.Songs.IndexOf(_playerState.CurrentSong);
        if (currentIndex > 0)
        {
            Play(_musicLibrary.Songs[currentIndex - 1]);
        }
    }
}
