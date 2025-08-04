using FighterGame.Domain;
using Menu.Commands;
using Menu.Core;
using Menu.UI;

namespace FighterGame.Command
{
    public class StartBattleCommand : ICommand
    {
        private readonly IUserInterface _ui;
        private readonly BattleEngine _battleEngine;
        public string Title => "Продолжить";

        public StartBattleCommand( IUserInterface ui, BattleEngine battleEngine )
        {
            _ui = ui;
            _battleEngine = battleEngine;
        }

        public CommandResult Execute()
        {
            try
            {
                _battleEngine.StartBattle();

                return CommandResults.Back();
            }
            catch ( Exception ex )
            {
                _ui.WriteLine( $"Ошибка: {ex.Message}" );
                return CommandResults.Continue();
            }
        }
    }
}