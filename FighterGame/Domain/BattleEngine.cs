using Menu.UI;

namespace FighterGame.Domain
{
    public class BattleEngine
    {
        private readonly IUserInterface _ui;
        private readonly FighterRepository _fighterRepository;

        public BattleEngine( IUserInterface ui, FighterRepository fighterRepository )
        {
            _ui = ui;
            _fighterRepository = fighterRepository;
        }

        public void StartBattle( IEnumerable<Guid> fighterIds )
        {
            _ui.Clear();
            List<IFighter> fighters = _fighterRepository.GetFighters( fighterIds );
            if ( fighters.Count < 2 )
            {
                _ui.WriteLine( "Недостаточно бойцов для начала боя" );
                return;
            }

            List<IFighter> initiativeOrder = DetermineInitiativeOrder( fighters );
            DisplayInitiativeOrder( initiativeOrder );

            IFighter champion = RunTournament( initiativeOrder );
            AnnounceChampion( champion );
        }

        private static List<IFighter> DetermineInitiativeOrder( IEnumerable<IFighter> fighters )
        {
            return fighters.Select( IFighter => new
                {
                    IFighter = IFighter,
                    Roll = IFighter.RollInitiative()
                } )
                .OrderByDescending( x => x.Roll )
                .Select( x => x.IFighter )
                .ToList();
        }

        private void DisplayInitiativeOrder( IReadOnlyList<IFighter> initiativeOrder )
        {
            _ui.WriteLine( "Турнирная таблица:" );
            for ( int i = 0; i < initiativeOrder.Count; i += 2 )
            {
                _ui.WriteLine( i + 1 < initiativeOrder.Count
                    ? $"{initiativeOrder[ i ].Name} vs {initiativeOrder[ i + 1 ].Name}"
                    : $"{initiativeOrder[ i ].Name} получает пропуск" );
            }
        }

        private IFighter RunTournament( List<IFighter> participants )
        {
            int round = 1;
            while ( participants.Count > 1 )
            {
                _ui.WriteLine( $"\n--- Начало раунда #{round} ---" );
                List<IFighter> winners = [ ];

                for ( int i = 0; i < participants.Count; i += 2 )
                {
                    IFighter first = participants[ i ];
                    IFighter? second = i + 1 < participants.Count ? participants[ i + 1 ] : null;

                    if ( second == null )
                    {
                        _ui.WriteLine( $"{first.Name} проходит далее без боя" );
                        winners.Add( first );
                    }
                    else
                        winners.Add( ConductDuel( first, second ) );
                }

                participants = winners;
                round++;
            }

            return participants[ 0 ];
        }

        private IFighter ConductDuel( IFighter fighterA, IFighter fighterB )
        {
            _ui.WriteLine( $"{fighterA.Name} vs {fighterB.Name}" );
            IFighter attacker = fighterA;
            IFighter defender = fighterB;

            while ( attacker.Health > 0 && defender.Health > 0 )
            {
                int result = attacker.Attack( defender );

                switch ( result )
                {
                    case > 0:
                        _ui.WriteLine(
                            $"{attacker.Name} нанёс {defender.Name} {result}hp урона. (осталось {defender.Health} HP)" );
                        break;
                    case < 0:
                        int selfDmg = -result;
                        _ui.WriteLine(
                            $"Критическая неудача! {attacker.Name} наносит {selfDmg} урона самому себе. (осталось {attacker.Health} HP)" );
                        break;
                    default:
                        _ui.WriteLine( $"{attacker.Name} промахнулся по {defender.Name}." );
                        break;
                }

                ( attacker, defender ) = ( defender, attacker );
            }

            IFighter winner = attacker.Health > 0 ? attacker : defender;
            IFighter loser = winner == attacker ? defender : attacker;

            _ui.WriteLine( $"{loser.Name} выбыл из турнира(" );
            _ui.WriteLine( $"{winner.Name} выигрывает поединок!" );

            int healAmount = winner.RollHealing();
            winner.Heal( healAmount );
            _ui.WriteLine( $"{winner.Name} исцеляется на {healAmount} HP (текущее {winner.Health} HP)" );

            return winner;
        }

        private void AnnounceChampion( IFighter champion ) =>
            _ui.WriteLine( $"\n{champion.Name} — победитель турнира!" );
    }
}