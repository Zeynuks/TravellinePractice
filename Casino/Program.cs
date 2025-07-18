using Casino.Commands;
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
            CommandFactory commands = new( ui, engine, wallet );
            App app = new( ui, commands );
            app.Run();
        }
    }
}