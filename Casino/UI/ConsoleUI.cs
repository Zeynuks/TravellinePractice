namespace Casino.UI
{
    public class ConsoleUi : IUserInterface
    {
        private const string _banner = "\n#############################\n" +
                                       "##         CASINO          ##\n" +
                                       "#############################\n";

        public void ShowBanner()
        {
            WriteLine( _banner );
        }

        public void WriteLine( string text )
        {
            Console.WriteLine( text );
        }

        public string ReadLine( string prompt )
        {
            WriteLine( prompt );

            return Console.ReadLine() ?? "";
        }

        public int ReadInt( string prompt )
        {
            while ( true )
            {
                WriteLine( prompt );
                if ( int.TryParse( Console.ReadLine(), out int val ) )
                {
                    return val;
                }

                WriteLine( "Неверный ввод. Введите целое число." );
            }
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}