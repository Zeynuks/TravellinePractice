using Menu.Commands;
using Menu.Core;
using Menu.Infrastructure;
using Menu.UI;
using OrderManager.Core;
using OrderManager.Core.Model;
using OrderManager.Core.Service;

namespace OrderManager.Command.OrderCommand
{
    public class ShowOrderListCommand : ICommand
    {
        public string Title => "Просмотр заказов";
        private readonly IUserInterface _ui;
        private readonly OrderService _orderService;
        private readonly Guid _customerId;
        private readonly ICommandRegistry _registry;

        public ShowOrderListCommand(
            IUserInterface ui,
            OrderService orderService,
            Guid customerId,
            ICommandRegistry registry )
        {
            _ui = ui;
            _orderService = orderService;
            _customerId = customerId;
            _registry = registry;
        }

        public CommandResult Execute()
        {
            try
            {
                IReadOnlyList<Order> orders = _orderService.GetOrdersByCustomerId( _customerId );
                if ( orders.Count == 0 )
                {
                    _ui.WriteLine( "У клиента нет заказов." );
                    return Results.Continue();
                }

                List<(string, ICommand, bool)> items = [ ];
                for ( int i = 0; i < orders.Count; i++ )
                {
                    Order order = orders[ i ];
                    MenuCommand orderMenu = OrderMenu.BuildOrderMenu(
                        _ui,
                        _orderService,
                        order.Id,
                        _registry );
                    
                    items.Add( ( ( i + 1 ).ToString(),
                        new NavigateCommand( orderMenu.MenuId, orderMenu.Title ), false ) );
                }

                items.Add( ( "0", new BackCommand(), true ) );

                string menuId = $"orders-{_customerId}";
                MenuCommand menu = new( _ui, menuId, "Заказы клиента:", items );
                _registry.Add( menu );

                return Results.Navigate( menuId );
            }
            catch ( Exception ex )
            {
                _ui.WriteLine( ex.Message );
                return Results.Continue();
            }
        }
    }
}