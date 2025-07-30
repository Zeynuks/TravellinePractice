using FighterGame.Domain;
using Menu.Commands;
using Menu.Core;
using Menu.Infrastructure;
using Menu.Infrastructure.Menu;
using Menu.UI;

namespace FighterGame.Command
{
    public class SelectFightersCommand : ICommand
    {
        public string Title => "Подготовка к турниру";
        private readonly IUserInterface _ui;
        private readonly IMenuRegistry _registry;
        private readonly BattleEngine _battleEngine;
        private readonly FighterRepository _fighterRepository;

        public SelectFightersCommand( IUserInterface ui, IMenuRegistry registry, BattleEngine battleEngine,
            FighterRepository fighterRepository )
        {
            _ui = ui;
            _registry = registry;
            _battleEngine = battleEngine;
            _fighterRepository = fighterRepository;
        }

        public CommandResult Execute()
        {
            MultiArrowMenu fightersMenu = new(
                _ui,
                $"fightersMenu-{Guid.NewGuid()}",
                new BackCommand(),
                Title,
                onSubmit: fighterIds => _battleEngine.StartBattle( fighterIds )
            );

            Dictionary<Guid, IFighter> fighters = _fighterRepository.GetAllFighters();
            if ( fighters.Count <= 0 )
            {
                return Results.Continue();
            }

            foreach ( var (id, fighter) in fighters )
            {
                fightersMenu.AddOption( id, fighter.Name );
            }

            _registry.Add( fightersMenu );

            return Results.Navigate( fightersMenu.MenuId );
        }
    }
}