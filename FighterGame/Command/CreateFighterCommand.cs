using FighterGame.Domain;
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
        public string Title => "Добавить нового бойца на арену";

        private readonly IUserInterface _ui;
        private readonly IMenuRegistry _registry;
        private readonly IFighterRepository _fighterRepository;

        public CreateFighterCommand(
            IUserInterface ui,
            IMenuRegistry registry,
            IFighterRepository fighterRepository
        )
        {
            _ui = ui;
            _registry = registry;
            _fighterRepository = fighterRepository;
        }

        public CommandResult Execute()
        {
            if ( _registry.TryGet( "create-fighter", out IMenu? menu ) )
            {
                if ( menu is null )
                {
                    throw new Exception( "Меню не найдено" );
                }

                return Results.Navigate( menu.MenuId );
            }

            FighterDto fighterDto = new();
            CommandMenu createFighterMenu = new( _ui, "create-fighter", "Выберите желаемые параметры: " );

            createFighterMenu.InsertOption( "1", new SelectNameCommand( _ui, fighterDto ) );
            createFighterMenu.InsertOption( "2", new SelectClassCommand( _ui, _registry, fighterDto ) );
            createFighterMenu.InsertOption( "3", new SelectRaceCommand( _ui, _registry, fighterDto ) );
            createFighterMenu.InsertOption( "4", new SelectArmorCommand( _ui, _registry, fighterDto ) );
            createFighterMenu.InsertOption( "5", new SelectWeaponCommand( _ui, _registry, fighterDto ) );
            createFighterMenu.InsertOption( "6", new SelectDamageTypeCommand( _ui, _registry, fighterDto ) );
            createFighterMenu.InsertOption( "7", new BuildFighterCommand( _registry, _fighterRepository, fighterDto ) );
            createFighterMenu.InsertOption( "0", new BackCommand() );
            _registry.Add( createFighterMenu );

            return Results.Navigate( createFighterMenu.MenuId );
        }
    }
}