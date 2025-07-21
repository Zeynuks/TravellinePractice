using Casino.Core;
using Casino.UI;

namespace Casino.Command
{
    public class DepositCommand : ICommand
    {
        private readonly IUserInterface _ui;
        private readonly IWallet _wallet;

        public DepositCommand( IUserInterface ui, IWallet wallet )
        {
            _ui = ui;
            _wallet = wallet;
        }

        public void Execute()
        {
            Money amount = _ui.ReadValue<Money>( "Введите сумму пополнения:" );

            if ( amount.Amount <= 0m )
            {
                _ui.WriteLine( "Сумма пополнения должна быть положительной." );

                return;
            }

            _wallet.Credit( amount );
            _ui.WriteLine( $"Баланс успешно пополнен на {amount}. Текущий баланс: {_wallet.Balance}" );
        }
    }
}