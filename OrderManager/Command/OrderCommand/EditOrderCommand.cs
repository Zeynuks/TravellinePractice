using Menu.Commands;
using Menu.Core;
using Menu.UI;
using OrderManager.Core.Model;
using OrderManager.Core.Service;

namespace OrderManager.Command.OrderCommand
{
    public class EditOrderCommand : ICommand
    {
        public string Title => "Изменить дату или адрес доставки";
        private readonly IUserInterface _ui;
        private readonly OrderService _orderService;
        private readonly Guid _orderId;

        public EditOrderCommand(
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
                string expectedDelivery = $"{order.ExpectedDelivery:dd.MM.yy}";

                DateTime newDate = _ui.ReadValue<DateTime>( $"Введите дату доставки ({expectedDelivery}): " );
                string? newAddr = _ui.ReadLine( $"Введите адрес доставки ({order.Address}): " );

                _orderService.UpdateOrder( _orderId, newDate, newAddr );
                _ui.WriteLine( "Заказ успешно обновлен." );
            }
            catch ( Exception ex )
            {
                _ui.WriteLine( ex.Message );
            }

            return Results.Continue();
        }
    }
}