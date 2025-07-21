using OrderManager.Command.OrderCommand;
using OrderManager.Model;
using OrderManager.Service;
using OrderManager.UI;
using OrderManager.UI.Menu;

namespace OrderManager.Command.CustomerCommand
{
    public class ShowCustomerMenuCommand : ICommand
    {
        private readonly IUserInterface _ui;
        private readonly CustomerService _customerService;
        private readonly OrderService _orderService;
        private readonly Guid _customerId;

        public ShowCustomerMenuCommand( IUserInterface ui, CustomerService customerService, OrderService orderService,
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
                Customer? customer = _customerService.GetCustomerById( _customerId );
                if ( customer == null )
                {
                    _ui.WriteLine( "Клиент не найден." );
                    
                    return;
                }

                Menu customerMenu = new( _ui );
                customerMenu.Add( "1", new MenuAction( _ui, "Просмотр заказов",
                    new ShowOrderListCommand( _ui, _orderService, _customerId ) ) );
                customerMenu.Add( "2", new MenuAction( _ui, "Новый заказ заказа",
                    new NewOrderCommand( _ui, _orderService, _customerId ) ) );
                customerMenu.Add( "3", new MenuAction( _ui, "Удалить клиента",
                    new RemoveCustomerCommand( _ui, _customerService,_orderService,  _customerId ) ), true );
                customerMenu.Add( "0", new MenuAction( _ui, "Выход" ), true );
                
                customerMenu.Execute();
            }
            catch ( Exception ex )
            {
                _ui.WriteLine( ex.Message );
            }
        }
    }
}