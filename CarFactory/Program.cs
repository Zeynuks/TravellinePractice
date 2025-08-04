using CarFactory.Command;
using CarFactory.Domain.Repository;
using CarFactory.UI;
using Menu.Commands;
using Menu.Infrastructure;
using Menu.Infrastructure.Menu;
using Menu.UI;

namespace CarFactory
{
    internal static class Program
    {
        private const string MenuId = "main";
        private const string FarewellMessage = "Удачи!";

        static void Main()
        {
            IUserInterface ui = new ConsoleUi();
            IMenuRegistry registry = new MenuRegistry();

            ICarRepository carRepository = new CarRepository();

            CommandMenu mainCommandMenu = new( ui, MenuId, "Введите команду:" );
            mainCommandMenu.InsertOption( "1", new CreateCarCommand( ui, registry, carRepository ) );
            mainCommandMenu.InsertOption( "2", new ShowCarListCommand( ui, registry, carRepository ) );
            mainCommandMenu.InsertOption( "0", new ExitCommand() );
            registry.Add( mainCommandMenu );

            new FlowRunner( mainCommandMenu, registry ).Run();
            ui.WriteLine( FarewellMessage );
        }
    }
}