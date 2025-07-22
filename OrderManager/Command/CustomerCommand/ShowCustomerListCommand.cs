using OrderManager.Model;
using OrderManager.Service;
using OrderManager.UI;
using OrderManager.UI.Menu;

namespace OrderManager.Command.CustomerCommand
{
    public class ShowCustomerListCommand : ICommand
    {
        private readonly IUserInterface _ui;
        private readonly CustomerService _customerService;
        private readonly OrderService _orderService;

        public ShowCustomerListCommand( IUserInterface ui, CustomerService customerService, OrderService orderService )
        {
            _ui = ui;
            _customerService = customerService;
            _orderService = orderService;
        }

        public void Execute()
        {
            try
            {
                IReadOnlyList<Customer> customers = _customerService.GetAllCustomers();
                if ( customers.Count == 0 )
                {
                    _ui.WriteLine( "Список пользователей пуст." );

                    return;
                }

                Menu customerMenu = new( _ui );

                for ( int i = 0; i < customers.Count; i++ )
                {
                    Customer customer = customers[ i ];
                    customerMenu.Add( $"{++i}", new MenuAction( $"{customer.Name}",
                        new ShowCustomerMenuCommand( _ui, _customerService, _orderService, customer.Id ) ), true );
                }

                customerMenu.Add( "0", new MenuAction( "Выход" ), true );

                customerMenu.Execute();
            }
            catch ( Exception ex )
            {
                _ui.WriteLine( ex.Message );
            }
        }
    }
}