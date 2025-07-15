using Casino.Core;
using Casino.UI;

namespace Casino.Commands
{
    public class DepositCommand : ICommand
    {
        public bool ShouldExit => false;

        public void Execute( IUserInterface ui, IGameEngine engine, Wallet wallet )
        {
            Money amount = new( ui.ReadInt( "Введите сумму пополнения:" ) );

            if ( amount.Amount <= 0m )
            {
                ui.WriteLine( "Сумма пополнения должна быть положительной." );
                
                return;
            }

            wallet.Credit( amount );
            ui.WriteLine( $"Баланс успешно пополнен на {amount}. Текущий баланс: {wallet.Balance}" );
        }
    }
}