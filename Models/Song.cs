using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP3_V2
{
    public class Song
    {
        public string Title { get; set; } // Назва пісні
        public string Artist { get; set; } // Виконавець
        public string AlbumArtPath { get; set; } // Шлях до обкладинки
        public string FilePath { get; set; } // Шлях до файлу пісні
        public bool IsFavorite { get; set; } // Чи улюблена пісня
    }
}
