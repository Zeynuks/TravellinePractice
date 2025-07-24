using Menu.Commands;
using Menu.Core;
using Menu.UI;
using OrderManager.Core.Service;

namespace OrderManager.Command.OrderCommand
{
    public sealed class CancelOrderCommand : ICommand
    {
        public string Title => "Отменить заказ";
        private readonly IUserInterface _ui;
        private readonly OrderService _orderService;
        private readonly Guid _orderId;

        public CancelOrderCommand(
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
                _orderService.DeleteOrder( _orderId );
                _ui.WriteLine( "Заказ отменен." );
            }
            catch ( Exception ex )
            {
                _ui.WriteLine( ex.Message );
            }

            return Results.Back();
        }
    }
}