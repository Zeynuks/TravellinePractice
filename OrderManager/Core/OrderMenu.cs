using Menu.Commands;
using Menu.Infrastructure;
using Menu.UI;
using OrderManager.Command.OrderCommand;
using OrderManager.Core.Model;
using OrderManager.Core.Service;

namespace OrderManager.Core
{
    public static class OrderMenu
    {
        private const string _backKey = "0";

        public static MenuCommand BuildOrderMenu(
            IUserInterface ui,
            OrderService os,
            Guid orderId,
            ICommandRegistry registry )
        {
            Order order = os.GetOrderById( orderId );
            string title = $"Заказ: {order.Product}, в количестве {order.Quantity}. " +
                           $"Дата доставки: {order.ExpectedDelivery}";

            MenuCommand menu = new( ui, $"order-{orderId}", $"Заказ: {title}" );
            menu.InsertOption( "1", new ShowOrderCommand( ui, os, orderId ) );
            menu.InsertOption( "2", new EditOrderCommand( ui, os, orderId ) );
            menu.InsertOption( "3", new CancelOrderCommand( ui, os, orderId ) );
            menu.InsertOption( _backKey, new BackCommand() );

            registry.Add( menu );
            return menu;
        }
    }
}