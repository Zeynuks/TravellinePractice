using Casino.Core;
using Casino.UI;

namespace Casino
{
    internal static class Program
    {
        private static void Main()
        {
            IUserInterface ui = new ConsoleUi();
            IGameEngine engine = new GameEngine();
            Wallet wallet = Bootstrapper.InitializeWallet( ui );

            App app = new( ui, engine, wallet );
            app.Run();
        }
    }
}