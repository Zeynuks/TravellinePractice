using CarFactory.Domain;
using CarFactory.Domain.Repository;
using Menu.Commands;
using Menu.Core;
using Menu.Infrastructure;
using Menu.Infrastructure.Menu;
using Menu.UI;

namespace CarFactory.Command
{
    public class CreateCarCommand : ICommand
    {
        private const string MenuId = "create-car";
        
        private readonly IUserInterface _ui;
        private readonly IMenuRegistry _registry;
        private readonly ICarRepository _repository;
        
        public string Title => "Создать новую машину";

        public CreateCarCommand( IUserInterface ui, IMenuRegistry registry, ICarRepository repository )
        {
            _ui = ui;
            _registry = registry;
            _repository = repository;
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

                CarDto carDto = new();
                CommandMenu createFighterMenu = new( _ui, MenuId, "Выберите желаемые параметры: " );

                createFighterMenu.InsertOption( "1", new SelectNumberCommand( _ui, carDto ) );
                createFighterMenu.InsertOption( "2", new SelectColorCommand( _ui, _registry, carDto ) );
                createFighterMenu.InsertOption( "3", new SelectBodyCommand( _ui, _registry, carDto ) );
                createFighterMenu.InsertOption( "4", new SelectEngineCommand( _ui, _registry, carDto ) );
                createFighterMenu.InsertOption( "5", new SelectTransmissionCommand( _ui, _registry, carDto ) );
                createFighterMenu.InsertOption( "6", new BuildCarCommand( _ui, _registry, _repository, carDto ) );
                createFighterMenu.InsertOption( "0", new BackCommand() );
                _registry.Add( createFighterMenu );

                return CommandResults.Navigate( createFighterMenu.MenuId );
            }
            catch ( Exception ex )
            {
                _ui.WriteLine( $"Ошибка: {ex.Message}" );
                return CommandResults.Continue();
            }
        }
    }
}