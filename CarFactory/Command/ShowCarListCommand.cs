using CarFactory.Domain.Model;
using CarFactory.Domain.Repository;
using Menu.Commands;
using Menu.Core;
using Menu.Infrastructure;
using Menu.Infrastructure.Menu;
using Menu.UI;

namespace CarFactory.Command
{
    public class ShowCarListCommand : ICommand
    {
        private const string MenuId = "car-list-menu";
        private readonly IUserInterface _ui;
        private readonly IMenuRegistry _registry;
        private readonly ICarRepository _carRepository;
        public string Title => "Список машин";

        public ShowCarListCommand( IUserInterface ui, IMenuRegistry registry, ICarRepository carRepository )
        {
            _ui = ui;
            _registry = registry;
            _carRepository = carRepository;
        }

        public CommandResult Execute()
        {
            try
            {
                IMenu? menu = _registry.TryGet( MenuId, out menu ) ? menu : null;
                if ( menu != null )
                {
                    menu.Title = Title;
                    return CommandResults.Navigate( menu.MenuId );
                }

                CommandMenu carsCommandMenu = new( _ui, MenuId );

                IReadOnlyList<ICar> cars = _carRepository.GetAllCars();
                if ( cars.Count <= 0 )
                {
                    return CommandResults.Continue();
                }

                for ( int i = 0; i < cars.Count; i++ )
                {
                    carsCommandMenu.InsertOption( $"{i + 1}", new ShowCarInfoCommand( _ui, cars[ i ] ) );
                }

                carsCommandMenu.InsertOption( "0", new BackCommand() );
                _registry.Add( carsCommandMenu );

                return CommandResults.Navigate( carsCommandMenu.MenuId );
            }
            catch ( Exception ex )
            {
                _ui.WriteLine( $"Ошибка: {ex.Message}" );
                return CommandResults.Continue();
            }
        }
    }
}