using Casino.Core;
using Casino.UI;

namespace Casino.Commands
{
    public class PlayCommand : ICommand
    {
        private readonly IUserInterface _ui;
        private readonly IGameEngine _engine;
        private readonly Wallet _wallet;

        public PlayCommand( IUserInterface ui, IGameEngine engine, Wallet wallet )
        {
            _ui = ui;
            _engine = engine;
            _wallet = wallet;
        }

        public void Execute()
        {
            Money bet = new( _ui.ReadValue<int>( "Введите сумму ставки:" ) );
            int mult = _ui.ReadValue<int>( "Введите множитель:" );

            if ( bet > _wallet.Balance )
            {
                throw new Exception( "Ставка превышает баланс." );
            }

            _wallet.Debit( bet );
            BetResult result = _engine.PlayRound( bet, mult );

            if ( result.IsWin )
            {
                _wallet.Credit( result.Amount );
                _ui.WriteLine( $"Вы выиграли {result.Amount} монет." );
            }
            else
            {
                _ui.WriteLine( $"Вы проиграли {result.Amount} монет." );
            }
        }
    }
}