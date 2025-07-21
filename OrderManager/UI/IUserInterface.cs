namespace OrderManager.UI
{
    public interface IUserInterface
    {
        void WriteLine( string text );
        public void Write( string text );
        string? ReadLine( string? prompt = null );

        T ReadValue<T>( string? prompt = null )
            where T : struct, IParsable<T>;

        void Clear();
    }
}