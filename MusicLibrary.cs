using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MP3_V2.Services
{
    public class MusicLibrary : IMusicLibrary
    {
        public ObservableCollection<Song> Songs { get; private set; }

        public MusicLibrary()
        {
            Songs = new ObservableCollection<Song>();
        }

        public void AddSong(Song song)  //відповідає за додавання об'єкта Song до колекції Songs
        {
            if (song == null)
                throw new ArgumentNullException(nameof(song));

            //перевіряє, чи немає вже пісні з таким самим шляхом до файлу (FilePath) у колекції Songs
            if (!Songs.Any(s => s.FilePath == song.FilePath))
            {
                Songs.Add(song);
            }
        }


        public void RemoveSong(Song song)
        {
            Songs.Remove(song);
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
                        Songs = new ObservableCollection<Song>();
                        return;
                    }

                    string json = File.ReadAllText(filePath);
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
                    };
                    Songs = JsonSerializer.Deserialize<ObservableCollection<Song>>(json, options);
                }
                catch
                {
                    Songs = new ObservableCollection<Song>();
                }
            }
            else
            {
                Songs = new ObservableCollection<Song>();
            }
        }

        public void SaveLibrary(string filePath)
        {
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