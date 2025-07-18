using System.Globalization;

namespace Casino.UI
{
    public class ConsoleUi : IUserInterface
    {
        private const string Banner = "\n#############################\n" +
                                      "##         CASINO          ##\n" +
                                      "#############################\n";

        public void ShowBanner()
        {
            WriteLine( Banner );
        }

        public void WriteLine( string text )
        {
            Console.WriteLine( text );
        }

        public void Write( string text )
        {
            Console.Write( text );
        }

        public string? ReadLine( string? prompt = null )
        {
            if ( prompt != null )
            {
                Write( prompt );
            }

            return Console.ReadLine();
        }

        public T ReadValue<T>( string? prompt = null )
            where T : struct, IParsable<T>
        {
            while ( true )
            {
                if ( prompt is not null )
                {
                    Write( prompt );
                }

                string? line = ReadLine();
                if ( line is not null
                     && T.TryParse( line, CultureInfo.CurrentCulture, out T value ) )
                {
                    return value;
                }

                WriteLine( $"Некорректный ввод. Введите значение типа {typeof( T ).Name}." );
            }
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}