namespace Casino.UI
{
    public class ConsoleUi : IUserInterface
    {
        private const string _banner =
            "\n\u2588\u2588\u2588\u2588\u2588\u2588\u2557\u2591\u2591\u2588\u2588\u2588\u2588\u2588\u2557\u2591\u2003" +
            "\u2003\u2588\u2588\u2588\u2588\u2588\u2588\u2557\u2591\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2557" +
            "\u2588\u2588\u2588\u2588\u2588\u2588\u2557\u2591\n\u2588\u2588\u2554\u2550\u2550\u2588\u2588\u2557\u2588" +
            "\u2588\u2554\u2550\u2550\u2588\u2588\u2557\u2003\u2003\u2588\u2588\u2554\u2550\u2550\u2588\u2588\u2557\u2588" +
            "\u2588\u2554\u2550\u2550\u2550\u2550\u255d\u2588\u2588\u2554\u2550\u2550\u2588\u2588\u2557\n\u2588\u2588" +
            "\u2551\u2591\u2591\u2588\u2588\u2551\u2588\u2588\u2551\u2591\u2591\u2588\u2588\u2551\u2003\u2003\u2588\u2588\u2551" +
            "\u2591\u2591\u2588\u2588\u2551\u2588\u2588\u2588\u2588\u2588\u2557\u2591\u2591\u2588\u2588\u2588\u2588\u2588\u2588" +
            "\u2554\u255d\n\u2588\u2588\u2551\u2591\u2591\u2588\u2588\u2551\u2588\u2588\u2551\u2591\u2591\u2588\u2588\u2551\u2003" +
            "\u2003\u2588\u2588\u2551\u2591\u2591\u2588\u2588\u2551\u2588\u2588\u2554\u2550\u2550\u255d\u2591\u2591\u2588\u2588" +
            "\u2554\u2550\u2550\u2550\u255d\u2591\n\u2588\u2588\u2588\u2588\u2588\u2588\u2554\u255d\u255a\u2588\u2588\u2588\u2588" +
            "\u2588\u2554\u255d\u2003\u2003\u2588\u2588\u2588\u2588\u2588\u2588\u2554\u255d\u2588\u2588\u2588\u2588\u2588\u2588" +
            "\u2588\u2557\u2588\u2588\u2551\u2591\u2591\u2591\u2591\u2591\n\u255a\u2550\u2550\u2550\u2550\u2550\u255d\u2591\u2591" +
            "\u255a\u2550\u2550\u2550\u2550\u255d\u2591\u2003\u2003\u255a\u2550\u2550\u2550\u2550\u2550\u255d\u2591\u255a\u2550\u2550" +
            "\u2550\u2550\u2550\u2550\u255d\u255a\u2550\u255d\u2591\u2591\u2591\u2591\u2591";

        public void ShowBanner()
        {
            Console.WriteLine( _banner );
        }

        public void WriteLine( string text ) => Console.WriteLine( text );

        public string ReadLine( string prompt )
        {
            Console.WriteLine( prompt );
            return Console.ReadLine() ?? "";
        }

        public int ReadInt( string prompt )
        {
            while ( true )
            {
                Console.WriteLine( prompt );
                if ( int.TryParse( Console.ReadLine(), out int val ) )
                    return val;
                Console.WriteLine( "Неверный ввод. Введите целое число." );
            }
        }

        public void Clear() => Console.Clear();
    }
}