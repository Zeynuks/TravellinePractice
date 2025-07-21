using OrderManager.Model;
using OrderManager.Service;
using OrderManager.UI;

namespace OrderManager.Command.OrderCommand
{
    public class NewOrderCommand : ICommand
    {
        private readonly IUserInterface _ui;
        private readonly OrderService _orderService;
        private readonly Guid _customerId;

        public NewOrderCommand( IUserInterface ui, OrderService orderService, Guid customerId )
        {
            _ui = ui;
            _orderService = orderService;
            _customerId = customerId;
        }

        public void Execute()
        {
            try
            {
                string? product = _ui.ReadLine( "Введите название товара:" );
                int quantity = _ui.ReadValue<int>( "Введите количество:" );
                string? address = _ui.ReadLine( "Введите адрес доставки:" );

                Order newOrder = _orderService.CreateOrder( _customerId, product, quantity, address );
                ShowOrderMenuCommand showOrderMenuCommand = new( _ui, _orderService, newOrder.Id );

                showOrderMenuCommand.Execute();
            }
            catch ( Exception ex )
            {
                _ui.WriteLine( ex.Message );
            }
        }
    }
}