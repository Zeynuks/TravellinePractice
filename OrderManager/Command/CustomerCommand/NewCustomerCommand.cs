using OrderManager.Model;
using OrderManager.Service;
using OrderManager.UI;

namespace OrderManager.Command.CustomerCommand
{
    public class NewCustomerCommand : ICommand
    {
        private readonly IUserInterface _ui;
        private readonly CustomerService _customerService;
        private readonly OrderService _orderService;

        public NewCustomerCommand( IUserInterface ui, CustomerService customerService, OrderService orderService )
        {
            _ui = ui;
            _customerService = customerService;
            _orderService = orderService;
        }

        public void Execute()
        {
            try
            {
                string? customerName = _ui.ReadLine( "Введите имя клиента:" );

                Guid customerId = _customerService.CreateCustomer( customerName );

                ShowCustomerMenuCommand customerMenuCommand =
                    new( _ui, _customerService, _orderService, customerId );

                customerMenuCommand.Execute();
            }
            catch ( Exception ex )
            {
                _ui.WriteLine( ex.Message );
            }
        }
    }
}