using Casino.Core;
using Casino.UI;

namespace Casino.Command
{
    public class PlayCommand : ICommand
    {
        private readonly IUserInterface _ui;
        private readonly IGameEngine _engine;
        private readonly IWallet _wallet;

        public PlayCommand( IUserInterface ui, IGameEngine engine, IWallet wallet )
        {
            _ui = ui;
            _engine = engine;
            _wallet = wallet;
        }

        public void Execute()
        {
            Money bet = _ui.ReadValue<Money>( "Введите сумму ставки:" );
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