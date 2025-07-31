using FighterGame.Domain;
using Menu.Commands;
using Menu.Core;

namespace FighterGame.Command
{
    public class SelectFighterCommand : ICommand
    {
        private readonly BattleEngine _battleEngine;
        private readonly IFighter _fighter;
        public string Title { get; set; }

        public SelectFighterCommand( BattleEngine battleEngine, IFighter fighter )
        {
            _battleEngine = battleEngine;
            _fighter = fighter;
            Title = IsSelected( _fighter.Id ) ? (_fighter.Name + " (selected)") : _fighter.Name;
        }

        public CommandResult Execute()
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

            return Results.Continue();
        }

        private bool IsSelected( Guid id )
        {
            return _battleEngine.ContainsParticipant( id );
        }
    }
}