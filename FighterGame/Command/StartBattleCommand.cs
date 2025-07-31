using FighterGame.Domain;
using Menu.Commands;
using Menu.Core;

namespace FighterGame.Command
{
    public class StartBattleCommand : ICommand
    {
        private readonly BattleEngine _battleEngine;
        public string Title => "Продолжить";

        public StartBattleCommand( BattleEngine battleEngine )
        {
            _battleEngine = battleEngine;
        }

        public CommandResult Execute()
        {
            _battleEngine.StartBattle();

            return Results.Back();
        }
    }
}