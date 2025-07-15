namespace Casino.Core
{
    public class GameEngine : IGameEngine
    {
        private const int _winThreshold = 18;
        private const int _maxRoll = 20;
        private readonly Random _rand = new();

        public BetResult PlayRound( Money bet, int mult )
        {
            int rolled = _rand.Next( 1, _maxRoll + 1 );
            if ( rolled < _winThreshold )
            {
                return new BetResult( false, bet );
            }

            Money win = bet * ( 1 + ( mult * rolled ) % ( _winThreshold - 1 ) );

            return new BetResult( true, win );
        }
    }
}