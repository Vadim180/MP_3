using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace MP3_V2.Services
{
    public class MusicLibrary : IMusicLibrary, INotifyPropertyChanged
    {
        private ObservableCollection<Song> _songs;

        public ObservableCollection<Song> Songs
        {
            get => _songs;
            private set
            {
                _songs = value;
                OnPropertyChanged(nameof(Songs));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MusicLibrary()
        {
            Songs = new ObservableCollection<Song>();
        }

        public void AddSong(Song song)
        {
            if (song == null) throw new ArgumentNullException(nameof(song));

            if (!Songs.Any(s => s.FilePath == song.FilePath))
            {
                Songs.Add(song);
            }
        }

        public void RemoveSong(Song song)
        {
            if (Songs.Contains(song))
            {
                Songs.Remove(song);
            }
        }

        public Song FindSongByPath(string filePath)
        {
            return Songs.FirstOrDefault(s => s.FilePath == filePath);
        }

        public void LoadLibrary(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    if (new FileInfo(filePath).Length == 0)
                    {
                        Songs.Clear();
                        return;
                    }

                    string json = File.ReadAllText(filePath);
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
                    };

                    var loadedSongs = JsonSerializer.Deserialize<ObservableCollection<Song>>(json, options);
                    Songs.Clear();
                    if (loadedSongs != null)
                    {
                        foreach (var song in loadedSongs)
                        {
                            Songs.Add(song);
                        }
                    }
                }
                catch
                {
                    Songs.Clear();
                }
            }
            else
            {
                Songs.Clear();
            }
        }

        public void SaveLibrary(string filePath)
        {
            if (Songs == null || Songs.Count == 0)
            {
                File.WriteAllText(filePath, "[]"); // Записуємо порожній масив JSON
                return;
            }

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
            };

            string json = JsonSerializer.Serialize(Songs, options);
            File.WriteAllText(filePath, json);
        }
    }
}
