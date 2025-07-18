using Casino.Core;
using Casino.UI;

namespace Casino.Commands
{
    public class CheckBalanceCommand : ICommand
    {
        private readonly IUserInterface _ui;
        private readonly Wallet _wallet;

        public CheckBalanceCommand( IUserInterface ui, Wallet wallet )
        {
            _ui = ui;
            _wallet = wallet;
        }

        public void Execute()
        {
            _ui.WriteLine( $"Ваш баланс: {_wallet.Balance}" );
        }
    }
}