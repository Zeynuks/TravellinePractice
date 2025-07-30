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
            BattleEngine battleEngine = new( ui, fighterRepository );
            FighterBuilder fighterBuilder = new();

            KeyMenu mainKeyMenu = new( ui, "main", "Введите команду:" );
            mainKeyMenu.InsertOption( "add-fighter",
                new CreateFighterCommand( ui, fighterRepository, fighterBuilder ) );
            mainKeyMenu.InsertOption( "play",
                new SelectFightersCommand( ui, registry, battleEngine, fighterRepository ) );
            mainKeyMenu.InsertOption( "exit", new ExitCommand() );

            registry.Add( mainKeyMenu );

            new FlowRunner( mainKeyMenu, registry ).Run();

            ui.WriteLine( "Удачи!" );
        }
    }
}