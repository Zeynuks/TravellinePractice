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
            IWallet wallet = Bootstrapper.InitializeWallet( ui );

            MenuFactory menuFactory = new( ui, engine, wallet );
            Menu casinoMenu = new( ui, "Введите команду:" );
            casinoMenu.Add( menuFactory.CreatePlayAction() );
            casinoMenu.Add( menuFactory.CreateCheckBalanceAction() );
            casinoMenu.Add( menuFactory.CreateDepositAction() );

            App app = new( ui, casinoMenu );
            app.Run();
        }
    }
}