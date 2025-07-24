using Menu.Commands;
using Menu.Infrastructure;
using Menu.UI;
using OrderManager.Command.CustomerCommand;
using OrderManager.Command.OrderCommand;
using OrderManager.Core.Model;
using OrderManager.Core.Service;

namespace OrderManager.Core
{
    public static class CustomerMenu
    {
        public static MenuCommand BuildCustomerMenu( 
            IUserInterface ui, 
            CustomerService cs, 
            OrderService os,
            Guid customerId, 
            ICommandRegistry registry )
        {
            Customer? customer = cs.GetCustomerById( customerId );
            string title = customer is null ? "Клиент (не найден)" : $"Клиент: {customer.Name}";

            List<(string, ICommand, bool)> items =
            [
                ( "1", new NewOrderCommand( ui, os, customerId, registry ), false ),
                ( "2", new ShowOrderListCommand( ui, os, customerId, registry ), false ),
                ( "3", new RemoveCustomerCommand( ui, cs, os, customerId ), false ),
                ( "0", new BackCommand(), true )
            ];

            MenuCommand menu = new( ui, $"customer-{customerId}", title, items );
            registry.Add( menu );
            return menu;
        }
    }
}