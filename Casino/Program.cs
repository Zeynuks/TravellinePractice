using Casino.Command;
using Casino.Core;
using Casino.UI;
using Casino.UI.Menu;

namespace Casino
{
    internal static class Program
    {
        private static void Main()
        {
            IUserInterface ui = new ConsoleUi();
            IGameEngine engine = new GameEngine();
            Wallet wallet = Bootstrapper.InitializeWallet( ui );

            //TODO Добавить MenuFactory для создания Actions
            Menu casinoMenu = new( ui, "Введите команду:" );
            casinoMenu.Add( new MenuAction( ui, "Играть", new PlayCommand( ui, engine, wallet ) ) );
            casinoMenu.Add( new MenuAction( ui, "Проверить баланс", new CheckBalanceCommand( ui, wallet ) ) );
            casinoMenu.Add( new MenuAction( ui, "Пополнить баланс", new DepositCommand( ui, wallet ) ) );

            App app = new( ui, casinoMenu );
            app.Run();
        }
    }
}