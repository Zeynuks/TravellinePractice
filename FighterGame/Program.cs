using FighterGame.Command;
using FighterGame.Domain.Repository;
using FighterGame.UI;
using Menu.Commands;
using Menu.Infrastructure;
using Menu.Infrastructure.Menu;
using Menu.UI;

namespace FighterGame
{
    internal static class Program
    {
        private const string MenuId = "main";
        private const string FarewellMessage = "Удачи!";

        public static void Main()
        {
            IUserInterface ui = new ConsoleUi();
            IMenuRegistry registry = new MenuRegistry();

            IFighterRepository fighterRepository = new FighterRepository();

            CommandMenu mainCommandMenu = new( ui, MenuId, "Введите команду:" );
            mainCommandMenu.InsertOption( "1", new CreateFighterCommand( ui, registry, fighterRepository ) );
            mainCommandMenu.InsertOption( "play", new PrepareToBattleCommand( ui, registry, fighterRepository ) );
            mainCommandMenu.InsertOption( "exit", new ExitCommand() );
            registry.Add( mainCommandMenu );

            new FlowRunner( mainCommandMenu, registry ).Run();
            ui.WriteLine( FarewellMessage );
        }
    }
}