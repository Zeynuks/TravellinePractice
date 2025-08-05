using FighterGame.Domain;
using FighterGame.Domain.Model;
using Menu.Commands;
using Menu.Core;
using Menu.UI;

namespace FighterGame.Command
{
    public class SelectFighterCommand : ICommand
    {
        private readonly IUserInterface _ui;
        private readonly BattleEngine _battleEngine;
        private readonly IFighter _fighter;
        
        public string Title { get; private set; }

        public SelectFighterCommand( IUserInterface ui, BattleEngine battleEngine, IFighter fighter )
        {
            _ui = ui;
            _battleEngine = battleEngine;
            _fighter = fighter;
            Title = IsSelected( _fighter.Id ) ? _fighter.Name + " (selected)" : _fighter.Name;
        }

        public CommandResult Execute()
        {
            try
            {
                if ( IsSelected( _fighter.Id ) )
                {
                    _battleEngine.DeleteParticipant( _fighter );
                    Title = _fighter.Name;
                }
                else
                {
                    _battleEngine.AddParticipant( _fighter );
                    Title = _fighter.Name + " (selected)";
                }

                return CommandResults.Continue();
            }
            catch ( Exception ex )
            {
                _ui.WriteLine( $"Ошибка: {ex.Message}" );
                return CommandResults.Continue();
            }
        }

        private bool IsSelected( Guid id )
        {
            return _battleEngine.ContainsParticipant( id );
        }
    }
}