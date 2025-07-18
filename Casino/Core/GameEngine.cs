namespace Casino.Core
{
    public class GameEngine : IGameEngine
    {
        private const int _winThreshold = 18;
        private const int _maxRoll = 20;

        public BetResult PlayRound( Money bet, int mult )
        {
            if ( mult <= 0 )
            {
                throw new Exception( "Множитель должен быть положительными." );
            }

            int rolled = Random.Shared.Next( 1, _maxRoll + 1 );
            if ( rolled < _winThreshold )
            {
                return new BetResult( false, bet );
            }

            Money win = bet * ( 1 + ( mult * rolled ) % ( _winThreshold - 1 ) );

            return new BetResult( true, win );
        }
    }
}