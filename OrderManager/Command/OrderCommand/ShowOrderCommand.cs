using Menu.Commands;
using Menu.Core;
using Menu.UI;
using OrderManager.Core.Model;
using OrderManager.Core.Service;

namespace OrderManager.Command.OrderCommand
{
    public class ShowOrderCommand : ICommand
    {
        public string Title => "Информация о заказе";
        private readonly IUserInterface _ui;
        private readonly OrderService _orderService;
        private readonly Guid _orderId;

        public ShowOrderCommand(
            IUserInterface ui,
            OrderService orderService,
            Guid orderId )
        {
            _ui = ui;
            _orderService = orderService;
            _orderId = orderId;
        }

        public CommandResult Execute()
        {
            try
            {
                Order order = _orderService.GetOrderById( _orderId );
                _ui.WriteLine( $"Номер заказа: {order.Id}" );
                _ui.WriteLine( $"Товар: {order.Product}" );
                _ui.WriteLine( $"Количество: {order.Quantity}" );
                _ui.WriteLine( $"Адрес доставки: {order.Address}" );
                _ui.WriteLine( $"Дата заказа: {order.CreatedAt}" );
                _ui.WriteLine( $"Ожидаемая дата доставки: {order.ExpectedDelivery:dd.MM.yy}" );
                _ui.WriteLine( $"Статус: {order.Status}" );
            }
            catch ( Exception ex )
            {
                _ui.WriteLine( ex.Message );
            }

            return Results.Continue();
        }
    }
}