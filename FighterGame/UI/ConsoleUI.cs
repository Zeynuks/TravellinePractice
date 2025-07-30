using System;
using System.Globalization;
using Menu.UI;

namespace FighterGame.UI
{
    public class ConsoleUi : IUserInterface
    {
        public void WriteLine( string? text )
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

            string? input = Console.ReadLine();

            return string.IsNullOrWhiteSpace( input ) ? null : input;
        }

        public T ReadValue<T>( string? prompt = null )
            where T : struct, IParsable<T>
        {
            while ( true )
            {
                if ( prompt != null )
                {
                    Write( prompt );
                }

                string? line = ReadLine();

                if ( line != null && T.TryParse( line, CultureInfo.CurrentCulture, out T value ) )
                {
                    return value;
                }

                WriteLine( "Некорректный ввод. Введите значение типа " + typeof( T ).Name + "." );
            }
        }
        
        public ConsoleKey ReadKey( bool intercept = false )
        {
            return Console.ReadKey( intercept ).Key;
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}