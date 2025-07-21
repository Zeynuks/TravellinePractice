using OrderManager.Model;
using OrderManager.Service;
using OrderManager.UI;

namespace OrderManager.Command.OrderCommand
{
    public class EditOrderCommand : ICommand
    {
        private readonly IUserInterface _ui;
        private readonly OrderService _orderService;
        private readonly Guid _orderId;

        public EditOrderCommand( IUserInterface ui, OrderService orderService, Guid orderId )
        {
            _ui = ui;
            _orderService = orderService;
            _orderId = orderId;
        }

        public void Execute()
        {
            try
            {
                Order order = _orderService.GetOrderById( _orderId );

                DateTime newDate =
                    _ui.ReadValue<DateTime>( $"Введите дату доставки ({order.ExpectedDelivery:dd.MM.yy}):" );
                string? newAddress = _ui.ReadLine( $"Введите адрес доставки ({order.Address}):" );

                _orderService.UpdateOrder( _orderId, newDate, newAddress );
                _ui.WriteLine( "Заказ успешно обновлен." );
            }
            catch ( Exception ex )
            {
                _ui.WriteLine( ex.Message );
            }
        }
    }
}