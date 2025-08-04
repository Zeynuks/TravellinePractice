using FighterGame.Command;
using FighterGame.Domain;
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
        public static void Main()
        {
            IUserInterface ui = new ConsoleUi();
            IMenuRegistry registry = new MenuRegistry();

            IFighterRepository fighterRepository = new FighterRepository();

            CommandMenu mainCommandMenu = new( ui, "main", "Введите команду:" );
            mainCommandMenu.InsertOption( "1", new CreateFighterCommand( ui, registry, fighterRepository ) );
            mainCommandMenu.InsertOption( "2", new PrepareToBattleCommand( ui, registry, fighterRepository ) );
            mainCommandMenu.InsertOption( "exit", new ExitCommand() );
            registry.Add( mainCommandMenu );

            new FlowRunner( mainCommandMenu, registry ).Run();
            ui.WriteLine( "Удачи!" );
        }
    }
}