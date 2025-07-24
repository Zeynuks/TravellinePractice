using Menu.Commands;
using Menu.Infrastructure;
using Menu.UI;
using OrderManager.Command.OrderCommand;
using OrderManager.Core.Model;
using OrderManager.Core.Service;

namespace OrderManager.Infrastructure
{
    public static class OrderMenu
    {
        public static MenuCommand BuildOrderMenu( IUserInterface ui, OrderService os, Guid orderId,
            ICommandRegistry registry )
        {
            Order order = os.GetOrderById( orderId );
            string title =
                $"Заказ: {order.Product}, в количестве {order.Quantity}. Дата доставки: {order.ExpectedDelivery}";

            List<(string, ICommand, bool)> items =
            [
                ( "1", new ShowOrderCommand( ui, os, orderId ), false ),
                ( "2", new EditOrderCommand( ui, os, orderId ), false ),
                ( "3", new CancelOrderCommand( ui, os, orderId ), false ),
                ( "0", new BackCommand(), true )
            ];
            MenuCommand menu = new( ui, $"order-{orderId}", $"Заказ: {title}", items );
            registry.Add( menu );
            return menu;
        }
    }
}