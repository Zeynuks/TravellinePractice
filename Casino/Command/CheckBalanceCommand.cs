using Casino.Core;
using Casino.UI;

namespace Casino.Command
{
    public class CheckBalanceCommand : ICommand
    {
        private readonly IUserInterface _ui;
        private readonly IWallet _wallet;

        public CheckBalanceCommand( IUserInterface ui, IWallet wallet )
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