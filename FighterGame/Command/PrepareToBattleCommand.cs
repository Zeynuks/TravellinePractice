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
    public class PrepareToBattleCommand : ICommand
    {
        public string Title => "Подготовка к турниру";
        private const string MenuId = "fighter-list-menu";
        private readonly IUserInterface _ui;
        private readonly IMenuRegistry _registry;
        private readonly BattleEngine _battleEngine;
        private readonly IFighterRepository _fighterRepository;

        public PrepareToBattleCommand(
            IUserInterface ui,
            IMenuRegistry registry,
            IFighterRepository fighterRepository
        )
        {
            _ui = ui;
            _registry = registry;
            _battleEngine = new BattleEngine(ui);
            _fighterRepository = fighterRepository;
        }

        public CommandResult Execute()
        {
            try
            {
                if ( _registry.TryGet( MenuId, out IMenu? menu ) )
                {
                    if ( menu is null )
                    {
                        throw new Exception( "Меню не найдено" );
                    }

                    return Results.Navigate( menu.MenuId );
                }

                CommandMenu fightersCommandMenu = new( _ui, MenuId );

                IReadOnlyList<IFighter> fighters = _fighterRepository.GetAllFighters();
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
            catch ( Exception ex)
            {
                _ui.WriteLine( $"Ошибка: {ex.Message}" );
                return Results.Continue();
            }
        }
    }
}