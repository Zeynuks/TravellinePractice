using Casino.Command;
using Casino.Core;

namespace Casino.UI.Menu
{
    public class MenuFactory
    {
        private readonly IUserInterface _ui;
        private readonly IGameEngine _engine;
        private readonly Wallet _wallet; //Стоит ли сделать интерфейс IWallet?

        public MenuFactory( IUserInterface ui, IGameEngine engine, Wallet wallet )
        {
            _ui = ui;
            _engine = engine;
            _wallet = wallet;
        }

        public MenuAction CreatePlayAction()
        {
            return new MenuAction( _ui, "Играть", new PlayCommand( _ui, _engine, _wallet ) );
        }

        public MenuAction CreateCheckBalanceAction()
        {
            return new MenuAction( _ui, "Проверить баланс", new CheckBalanceCommand( _ui, _wallet ) );
        }

        public MenuAction CreateDepositAction()
        {
            return new MenuAction( _ui, "Пополнить баланс", new DepositCommand( _ui, _wallet ) );
        }
    }
}