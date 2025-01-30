using System;

namespace MP3_V2
{
    public class Song
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string AlbumArtPath { get; set; }
        public string FilePath { get; set; }
        public bool IsFavorite { get; set; }

        // Конструктор для встановлення значень за замовчуванням
        public Song(string title, string artist, string albumArtPath, string filePath, bool isFavorite)
        {
            Title = !string.IsNullOrWhiteSpace(title) ? title : "Недоступно";
            Artist = !string.IsNullOrWhiteSpace(artist) ? artist : "Виконавець невідомий";
            AlbumArtPath = !string.IsNullOrWhiteSpace(albumArtPath) ? albumArtPath : "path/to/default/album/art";
            FilePath = filePath;
            IsFavorite = isFavorite;
        }

    }
}