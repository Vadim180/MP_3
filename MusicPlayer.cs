using System;
using System.Diagnostics;
using System.Windows.Media;

namespace MP3_V2.Services
{
    public class MusicPlayer : IMusicPlayer
    {
        private readonly MediaPlayer _mediaPlayer = new MediaPlayer();
        private Song _currentSong;

        public void Play(Song song)
        {
            if (song == null)
                throw new ArgumentNullException(nameof(song));

            try
            {
                if (_currentSong != song)
                {
                    _currentSong = song;
                    _mediaPlayer.Open(new Uri(song.FilePath));
                }

                _mediaPlayer.Play();
                Debug.WriteLine($"Playing: {song.Title}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error playing song: {ex.Message}");
            }
        }

        public void Pause()
        {
            _mediaPlayer.Pause();
            Debug.WriteLine("Playback paused.");
        }

        public void Stop()
        {
            _mediaPlayer.Stop();
            _currentSong = null;
            Debug.WriteLine("Playback stopped.");
        }

        public void Next()
        {
            Debug.WriteLine("Skipping to next song.");
            // Реалізація перемикання на наступну пісню буде у контролері
        }

        public void Previous()
        {
            Debug.WriteLine("Going back to previous song.");
            // Реалізація перемикання на попередню пісню буде у контролері
        }
    }
}
