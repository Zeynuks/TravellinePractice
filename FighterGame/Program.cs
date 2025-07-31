using FighterGame.Command;
using FighterGame.Domain;
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

            FighterRepository fighterRepository = new();
            BattleEngine battleEngine = new( ui );
            FighterBuilder fighterBuilder = new();

            Menu.Infrastructure.Menu.CommandMenu mainCommandMenu = new( ui, "main", "Введите команду:" );
            mainCommandMenu.InsertOption( "1",
                new CreateFighterCommand( ui, fighterRepository, fighterBuilder ) );
            mainCommandMenu.InsertOption( "2",
                new PrepareToBattleCommand( ui, registry, battleEngine, fighterRepository ) );
            mainCommandMenu.InsertOption( "exit", new ExitCommand() );

            registry.Add( mainCommandMenu );

            new FlowRunner( mainCommandMenu, registry ).Run();

            ui.WriteLine( "Удачи!" );
        }
    }
}