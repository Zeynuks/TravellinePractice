using FighterGame.Domain;
using Menu.Commands;
using Menu.Core;
using Menu.Infrastructure;
using Menu.UI;

namespace FighterGame.Command
{
    public class PrepareToBattleCommand : ICommand
    {
        public string Title => "Подготовка к турниру";
        private readonly IUserInterface _ui;
        private readonly IMenuRegistry _registry;
        private readonly BattleEngine _battleEngine;
        private readonly FighterRepository _fighterRepository;

        public PrepareToBattleCommand( IUserInterface ui, IMenuRegistry registry, BattleEngine battleEngine,
            FighterRepository fighterRepository )
        {
            _ui = ui;
            _registry = registry;
            _battleEngine = battleEngine;
            _fighterRepository = fighterRepository;
        }

        public CommandResult Execute()
        {
            Menu.Infrastructure.Menu.CommandMenu fightersCommandMenu = new( _ui, "fighters-menu" );

            List<IFighter> fighters = _fighterRepository.GetAllFighters();
            if ( fighters.Count <= 0 )
            {
                return Results.Continue();
            }

            for ( int i = 0; i < fighters.Count; i++ )
            {
                fightersCommandMenu.InsertOption( $"{i + 1}",
                    new SelectFighterCommand( _battleEngine, fighters[ i ] ) );
            }

            fightersCommandMenu.InsertOption( $"{fighters.Count + 1}", new StartBattleCommand( _battleEngine ) );
            fightersCommandMenu.InsertOption( "0", new BackCommand() );
            _registry.Add( fightersCommandMenu );

            return Results.Navigate( fightersCommandMenu.MenuId );
        }
    }
}