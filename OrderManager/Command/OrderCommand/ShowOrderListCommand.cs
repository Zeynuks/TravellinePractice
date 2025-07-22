using OrderManager.Model;
using OrderManager.Service;
using OrderManager.UI;
using OrderManager.UI.Menu;

namespace OrderManager.Command.OrderCommand
{
    public class ShowOrderListCommand : ICommand
    {
        private readonly IUserInterface _ui;
        private readonly OrderService _orderService;
        private readonly Guid _customerId;

        public ShowOrderListCommand( IUserInterface ui, OrderService orderService, Guid customerId )
        {
            _ui = ui;
            _orderService = orderService;
            _customerId = customerId;
        }

        public void Execute()
        {
            try
            {
                IReadOnlyList<Order> orders = _orderService.GetOrdersByCustomerId( _customerId );
                if ( orders.Count == 0 )
                {
                    _ui.WriteLine( "У клиента нет заказов." );

                    return;
                }

                Menu orderMenu = new( _ui );
                for ( int i = 0; i < orders.Count; i++ )
                {
                    Order order = orders[ i ];
                    orderMenu.Add( $"{++i}", new MenuAction(
                        $"Номер заказа: {order.Product}, Статус: {order.OrderStatus}, " +
                        $"Дата доставки: {order.ExpectedDelivery:d}",
                        new ShowOrderMenuCommand( _ui, _orderService, order.Id ) ), true );
                }

                orderMenu.Add( "0", new MenuAction( "Выход" ), true );

                orderMenu.Execute();
            }
            catch ( Exception ex )
            {
                _ui.WriteLine( ex.Message );
            }
        }
    }
}