using Menu.Commands;
using Menu.Core;
using Menu.Infrastructure;
using Menu.UI;
using OrderManager.Core;
using OrderManager.Core.Model;
using OrderManager.Core.Service;

namespace OrderManager.Command.CustomerCommand
{
    public class ShowCustomerListCommand : ICommand
    {
        public string Title => "Список пользователей";
        private readonly IUserInterface _ui;
        private readonly CustomerService _customerService;
        private readonly OrderService _orderService;
        private readonly ICommandRegistry _registry;

        public ShowCustomerListCommand(
            IUserInterface ui,
            CustomerService customerService,
            OrderService orderService,
            ICommandRegistry registry )
        {
            _ui = ui;
            _customerService = customerService;
            _orderService = orderService;
            _registry = registry;
        }

        public CommandResult Execute()
        {
            try
            {
                IReadOnlyList<Customer> customers = _customerService.GetAllCustomers();
                if ( !customers.Any() )
                {
                    _ui.WriteLine( "Список пользователей пуст." );
                    return Results.Continue();
                }

                List<(string, ICommand)> items = [ ];
                for ( int i = 0; i < customers.Count; i++ )
                {
                    Customer customer = customers[ i ];
                    MenuCommand customerMenu = CustomerMenu.BuildCustomerMenu(
                        _ui,
                        _customerService,
                        _orderService,
                        customer.Id,
                        _registry );
                    
                    items.Add( ( ( i + 1 ).ToString(),
                        new NavigateCommand( customerMenu.MenuId, customerMenu.Title ) ) );
                }

                items.Add( ( "0", new BackCommand() ) );

                string menuId = $"customers-{Guid.NewGuid()}";
                MenuCommand menu = new( _ui, menuId, "Выберите пользователя:", items );
                _registry.Add( menu );

                return Results.Navigate( menuId );
            }
            catch ( Exception ex )
            {
                _ui.WriteLine( ex.Message );

                return Results.Continue();
            }
        }
    }
}