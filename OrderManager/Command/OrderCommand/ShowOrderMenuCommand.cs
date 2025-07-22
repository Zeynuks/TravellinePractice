using OrderManager.Service;
using OrderManager.UI;
using OrderManager.UI.Menu;

namespace OrderManager.Command.OrderCommand
{
    public class ShowOrderMenuCommand : ICommand
    {
        private readonly IUserInterface _ui;
        private readonly OrderService _orderService;
        private readonly Guid _orderId;

        public ShowOrderMenuCommand( IUserInterface ui, OrderService orderService, Guid orderId )
        {
            _ui = ui;
            _orderService = orderService;
            _orderId = orderId;
        }

        public void Execute()
        {
            try
            {
                Menu menu = new( _ui );
                menu.Add( "1", new MenuAction( "Информация о заказе",
                    new ShowOrderCommand( _ui, _orderService, _orderId ) ) );
                menu.Add( "2", new MenuAction( "Изменить дату или адрес доставки",
                    new EditOrderCommand( _ui, _orderService, _orderId ) ) );
                menu.Add( "3", new MenuAction( "Отменить заказ",
                    new CancelOrderCommand( _ui, _orderService, _orderId ) ), true );
                menu.Add( "0", new MenuAction( "Выход" ), true );

                menu.Execute();
            }
            catch ( Exception ex )
            {
                _ui.WriteLine( ex.Message );
            }
        }
    }
}