using MP3_V2;
using System.Collections.ObjectModel;

public interface IMusicLibrary
{
    ObservableCollection<Song> Songs { get; }
    void AddSong(Song song);
    void RemoveSong(Song song);
    Song FindSongByPath(string filePath);
    void LoadLibrary(string filePath);
    void SaveLibrary(string filePath);
}
