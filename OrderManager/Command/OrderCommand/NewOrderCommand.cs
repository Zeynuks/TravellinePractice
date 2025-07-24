using Menu.Commands;
using Menu.Core;
using Menu.Infrastructure;
using Menu.UI;
using OrderManager.Core;
using OrderManager.Core.Model;
using OrderManager.Core.Service;

namespace OrderManager.Command.OrderCommand
{
    public class NewOrderCommand : ICommand
    {
        public string Title => "Новый заказ";
        private readonly IUserInterface _ui;
        private readonly OrderService _orderService;
        private readonly Guid _customerId;
        private readonly ICommandRegistry _registry;

        public NewOrderCommand(
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
                string? product = _ui.ReadLine( "Введите название товара: " );
                int quantity = _ui.ReadValue<int>( "Введите количество: " );
                string? addr = _ui.ReadLine( "Введите адрес доставки: " );

                Order order = _orderService.CreateOrder( _customerId, product, quantity, addr );
                MenuCommand menu = OrderMenu.BuildOrderMenu( _ui, _orderService, order.Id, _registry );

                return Results.Navigate( menu.MenuId );
            }
            catch ( Exception ex )
            {
                _ui.WriteLine( ex.Message );

                return Results.Continue();
            }
        }
    }
}