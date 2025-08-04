using FighterGame.Domain.Model;
using FighterGame.Domain.Repository;
using Menu.Commands;
using Menu.Core;
using Menu.Infrastructure;
using Menu.Infrastructure.Menu;
using Menu.UI;

namespace FighterGame.Command
{
    public sealed class CreateFighterCommand : ICommand
    {
        private const string MenuId = "create-fighter";
        private readonly IUserInterface _ui;
        private readonly IMenuRegistry _registry;
        private readonly IFighterRepository _repository;
        public string Title => "Добавить нового бойца на арену";
        
        public CreateFighterCommand(
            IUserInterface ui,
            IMenuRegistry registry,
            IFighterRepository repository
        )
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
                    return Results.Navigate( menu.MenuId );
                }

                FighterDto fighterDto = new();
                CommandMenu createFighterMenu = new( _ui, MenuId, "Выберите желаемые параметры: " );

                createFighterMenu.InsertOption( "1", new SelectNameCommand( _ui, fighterDto ) );
                createFighterMenu.InsertOption( "2", new SelectClassCommand( _ui, _registry, fighterDto ) );
                createFighterMenu.InsertOption( "3", new SelectRaceCommand( _ui, _registry, fighterDto ) );
                createFighterMenu.InsertOption( "4", new SelectArmorCommand( _ui, _registry, fighterDto ) );
                createFighterMenu.InsertOption( "5", new SelectWeaponCommand( _ui, _registry, fighterDto ) );
                createFighterMenu.InsertOption( "6", new SelectDamageTypeCommand( _ui, _registry, fighterDto ) );
                createFighterMenu.InsertOption( "7",
                    new BuildFighterCommand( _ui, _registry, _repository, fighterDto ) );
                createFighterMenu.InsertOption( "0", new BackCommand() );
                _registry.Add( createFighterMenu );

                return Results.Navigate( createFighterMenu.MenuId );
            }
            catch ( Exception ex )
            {
                _ui.WriteLine( $"Ошибка: {ex.Message}" );
                return Results.Continue();
            }
        }
    }
}