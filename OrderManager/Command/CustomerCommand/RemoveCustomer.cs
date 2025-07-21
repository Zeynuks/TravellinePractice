using OrderManager.Service;
using OrderManager.UI;

namespace OrderManager.Command.CustomerCommand
{
    public class RemoveCustomerCommand : ICommand
    {
        private readonly IUserInterface _ui;
        private readonly CustomerService _customerService;
        private readonly OrderService _orderService;
        private readonly Guid _customerId;

        public RemoveCustomerCommand( IUserInterface ui, CustomerService customerService, OrderService orderService,
            Guid customerId )
        {
            _ui = ui;
            _customerService = customerService;
            _orderService = orderService;
            _customerId = customerId;
        }

        public void Execute()
        {
            try
            {
                bool isRemoved = _customerService.RemoveCustomer( _customerId );
                _orderService.DeleteOrdersByCustomerId( _customerId );

                _ui.WriteLine( isRemoved ? "Клиент успешно удален." : "Не удалось удалить клиента." );
            }
            catch ( Exception ex )
            {
                _ui.WriteLine( ex.Message );
            }
        }
    }
}