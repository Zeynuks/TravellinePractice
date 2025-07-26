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
        private static void Main()
        {
            CustomerRepository customerRepository = new();
            OrderRepository orderRepository = new();

            CustomerService customerService = new( customerRepository );
            OrderService orderService = new( orderRepository );

            IUserInterface ui = new ConsoleUi();

            CommandRegistry registry = new();

            List<(string, ICommand)> mainMenuItems =
            [
                ( "1", new NewCustomerCommand( ui, customerService, orderService, registry ) ),
                ( "2", new ShowCustomerListCommand( ui, customerService, orderService, registry ) ),
                ( "0", new ExitCommand() )
            ];

            MenuCommand mainMenu = new( ui, "main", "Главное меню:", mainMenuItems );
            registry.Add( mainMenu );

            new FlowRunner( mainMenu, registry ).Run();
            ui.WriteLine( "Спасибо за использование нашего сервиса. До свидания!" );
        }
    }
}