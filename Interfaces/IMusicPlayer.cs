namespace MP3_V2.Services
{
    public interface IMusicPlayer
    {
        void Play(Song song);
        void Pause();
        void Stop();
        void Next();
        void Previous();
    }
}
