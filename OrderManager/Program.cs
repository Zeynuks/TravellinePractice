using OrderManager.Repository;
using OrderManager.Service;
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

            App app = new( ui, customerService, orderService );
            app.Run();
        }
    }
}