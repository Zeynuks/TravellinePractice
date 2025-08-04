using CarFactory.Domain.Model;
using Menu.Commands;
using Menu.Core;
using Menu.UI;

namespace CarFactory.Command
{
    public class ShowCarInfoCommand : ICommand
    {
        private readonly IUserInterface _ui;
        private readonly ICar _car;
        public string Title { get; }

        public ShowCarInfoCommand( IUserInterface ui, ICar car )
        {
            _ui = ui;
            _car = car;
            Title = $"Машина #{_car.Number}";
        }

        public CommandResult Execute()
        {
            try
            {
                _ui.WriteLine( $"Конфигурация машины:" );
                _ui.WriteLine( $"Номер: {_car.Number}" );
                _ui.WriteLine( $"Цвет: {_car.Color}" );
                _ui.WriteLine( $"Коробка передач: {_car.Transmission.GetType().Name}" );
                _ui.WriteLine( $"Двигатель: {_car.Engine.GetType().Name}" );
                _ui.WriteLine( $"Кузов: {_car.Body.GetType().Name}" );
                _ui.WriteLine( $"Максимальная скорость: {_car.GetMaxSpeed()} км/ч\n" );

                return Results.Continue();
            }
            catch ( Exception ex )
            {
                _ui.WriteLine( $"Ошибка: {ex.Message}" );
                return Results.Back();
            }
        }
    }
}