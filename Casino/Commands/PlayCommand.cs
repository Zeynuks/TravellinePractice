using Casino.Core;
using Casino.UI;

namespace Casino.Commands
{
    public class PlayCommand : ICommand
    {
        public bool ShouldExit => false;

        public void Execute( IUserInterface ui, IGameEngine engine, Wallet wallet )
        {
            Money bet = new( ui.ReadInt( "Введите сумму ставки:" ) );
            int mult = ui.ReadInt( "Введите множитель:" );

            if ( bet.Amount <= 0m || mult <= 0 )
            {
                ui.WriteLine( "Ставка и множитель должны быть положительными." );
                
                return;
            }

            if ( bet > wallet.Balance )
            {
                ui.WriteLine( "Ставка превышает баланс." );
                
                return;
            }

            wallet.Debit( bet );
            BetResult result = engine.PlayRound( bet, mult );

            if ( result.IsWin )
            {
                wallet.Credit( result.Amount );
                ui.WriteLine( $"Вы выиграли {result.Amount} монет." );
            }
            else
            {
                ui.WriteLine( $"Вы проиграли {result.Amount} монет." );
            }
        }
    }
}