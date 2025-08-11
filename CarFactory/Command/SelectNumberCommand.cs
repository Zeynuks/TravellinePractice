using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using CarFactory.Domain;
using Menu.Commands;
using Menu.Core;
using Menu.Infrastructure;
using Menu.UI;

namespace CarFactory.Command
{
    public class SelectNumberCommand : ICommand
    {
        private readonly IUserInterface _ui;
        private readonly CarDto _carDto;

        public string Title { get; private set; }

        public SelectNumberCommand( IUserInterface ui, CarDto carDto )
        {
            _ui = ui;
            _carDto = carDto;
            Title = $"Введите номер машины ({_carDto.Number})";
        }

        public CommandResult Execute()
        {
            try
            {
                string? number = _ui.ReadLine( $"{Title}: " )?.ToUpper();

                if ( string.IsNullOrWhiteSpace( number ) )
                {
                    throw new ValidationException( "Неверный формат номера. Поле не может быть пустым" );
                }

                if ( !IsValidCarNumber( number ) )
                {
                    throw new ValidationException( "Неверный формат номера. Пожалуйста, введите в формате: H799EM." );
                }

                _carDto.Number = number;
                Title = $"Введите номер машины ({_carDto.Number})";

                return CommandResults.Continue();
            }
            catch ( Exception ex )
            {
                _ui.WriteLine( $"Ошибка: {ex.Message}" );
                return CommandResults.Continue();
            }
        }

        private static bool IsValidCarNumber( string number )
        {
            const string pattern = @"^[A-Z]{1}\d{3}[A-Z]{2}$";
            Regex regex = new( pattern, RegexOptions.IgnoreCase );

            return regex.IsMatch( number );
        }
    }
}