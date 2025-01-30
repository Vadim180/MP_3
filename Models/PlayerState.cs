using MP3_V2;
using System;
using System.ComponentModel;
public enum PlaybackState
{
    Stopped,
    Playing,
    Paused
}

public class PlayerState : INotifyPropertyChanged
{
    private PlaybackState _currentState;
    private bool _isMuted;
    private TimeSpan _currentTime;
    private int _volume;
    private Song _currentSong;

    public PlaybackState CurrentState
    {
        get => _currentState;
        set
        {
            if (_currentState != value)
            {
                _currentState = value;
                OnPropertyChanged(nameof(CurrentState));
            }
        }
    }

    public bool IsMuted
    {
        get => _isMuted;
        set
        {
            if (_isMuted != value)
            {
                _isMuted = value;
                OnPropertyChanged(nameof(IsMuted));
            }
        }
    }

    public TimeSpan CurrentTime
    {
        get => _currentTime;
        set
        {
            if (_currentTime != value)
            {
                _currentTime = value;
                OnPropertyChanged(nameof(CurrentTime));
            }
        }
    }

    public int Volume
    {
        get => _volume;
        set
        {
            if (_volume != value)
            {
                _volume = value;
                OnPropertyChanged(nameof(Volume));
            }
        }
    }

    public Song CurrentSong
    {
        get => _currentSong;
        set
        {
            if (_currentSong != value)
            {
                _currentSong = value;
                OnPropertyChanged(nameof(CurrentSong));
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
