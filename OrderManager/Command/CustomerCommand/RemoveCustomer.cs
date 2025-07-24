using Menu.Commands;
using Menu.Core;
using Menu.UI;
using OrderManager.Core.Service;

namespace OrderManager.Command.CustomerCommand
{
    public class RemoveCustomerCommand : ICommand
    {
        public string Title => "Удалить клиента";
        private readonly IUserInterface _ui;
        private readonly CustomerService _customerService;
        private readonly OrderService _orderService;
        private readonly Guid _customerId;

        public RemoveCustomerCommand(
            IUserInterface ui,
            CustomerService customerService,
            OrderService orderService,
            Guid customerId )
        {
            _ui = ui;
            _customerService = customerService;
            _orderService = orderService;
            _customerId = customerId;
        }

        public CommandResult Execute()
        {
            try
            {
                bool ok = _customerService.RemoveCustomer( _customerId );
                _orderService.DeleteOrdersByCustomerId( _customerId );

                _ui.WriteLine( ok ? "Клиент успешно удален." : "Не удалось удалить клиента." );
            }
            catch ( Exception ex )
            {
                _ui.WriteLine( ex.Message );
            }

            return Results.Back();
        }
    }
}