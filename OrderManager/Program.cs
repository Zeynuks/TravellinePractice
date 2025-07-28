using Menu.Commands;
using Menu.Infrastructure;
using Menu.UI;
using OrderManager.Command.CustomerCommand;
using OrderManager.Core.Repository;
using OrderManager.Core.Service;
using OrderManager.UI;

namespace OrderManager
{
    internal static class Program
    {
        private const string _farewellMessage = "Спасибо за использование нашего сервиса. До свидания!";

        private static void Main()
        {
            CustomerRepository customerRepository = new();
            OrderRepository orderRepository = new();

            CustomerService customerService = new( customerRepository );
            OrderService orderService = new( orderRepository );

            IUserInterface ui = new ConsoleUi();

            CommandRegistry registry = new();

            MenuCommand mainMenu = new( ui, "main", "Главное меню:" );
            mainMenu.InsertOption( "1", new CreateCustomerCommand( ui, customerService, orderService, registry ) );
            mainMenu.InsertOption( "2", new ShowCustomerListCommand( ui, customerService, orderService, registry ) );
            mainMenu.InsertOption( "0", new ExitCommand() );

            registry.Add( mainMenu );

            new FlowRunner( mainMenu, registry ).Run();
            ui.WriteLine( _farewellMessage );
        }
    }
}