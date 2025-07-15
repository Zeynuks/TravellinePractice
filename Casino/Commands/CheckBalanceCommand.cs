using Casino.Core;
using Casino.UI;

namespace Casino.Commands
{
    public class CheckBalanceCommand : ICommand
    {
        public bool ShouldExit => false;

        public void Execute( IUserInterface ui, IGameEngine engine, Wallet wallet )
            => ui.WriteLine( $"Ваш баланс: {wallet.Balance}" );
    }
}