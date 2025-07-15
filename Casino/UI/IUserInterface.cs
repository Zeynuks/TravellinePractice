namespace Casino.UI
{
    public interface IUserInterface
    {
        void ShowBanner();
        void WriteLine( string text );
        string ReadLine( string prompt );
        int ReadInt( string prompt );
        void Clear();
    }
}