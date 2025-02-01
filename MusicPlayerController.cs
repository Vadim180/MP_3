using MP3_V2;
using MP3_V2.Services;
using System;

public class MusicPlayerController
{
    private readonly PlayerState _playerState;
    private readonly IMusicLibrary _musicLibrary;
    private readonly IMusicPlayer _musicPlayer;

    public MusicPlayerController(PlayerState playerState, IMusicLibrary musicLibrary, IMusicPlayer musicPlayer)
    {
        _playerState = playerState;
        _musicLibrary = musicLibrary;
        _musicPlayer = musicPlayer;
    }

    public void Play(Song song)
    {
        if (song == null)
            throw new ArgumentNullException(nameof(song));

        _playerState.CurrentSong = song;
        _playerState.CurrentState = PlaybackState.Playing;
        _musicPlayer.Play(song);

        // Логіка для запуску відтворення
        Console.WriteLine($"Now playing: {song.Title}");
    }

    public void Pause()
    {
        if (_playerState.CurrentState == PlaybackState.Playing)
        {
            _playerState.CurrentState = PlaybackState.Paused;
            _musicPlayer.Pause();
            // Логіка для паузи
            Console.WriteLine("Playback paused.");
        }
    }

    public void Stop()
    {
        _playerState.CurrentState = PlaybackState.Stopped;
        _playerState.CurrentTime = TimeSpan.Zero;
        _musicPlayer.Stop();

        // Логіка для зупинки
        Console.WriteLine("Playback stopped.");
    }

    public void Next()
    {
        int currentIndex = _musicLibrary.Songs.IndexOf(_playerState.CurrentSong);
        if (currentIndex < _musicLibrary.Songs.Count - 1)
        {
            Play(_musicLibrary.Songs[currentIndex + 1]);
        }
    }

    public void Previous()
    {
        int currentIndex = _musicLibrary.Songs.IndexOf(_playerState.CurrentSong);
        if (currentIndex > 0)
        {
            Play(_musicLibrary.Songs[currentIndex - 1]);
        }
    }
}
