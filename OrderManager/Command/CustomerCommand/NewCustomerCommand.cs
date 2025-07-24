using Menu.Commands;
using Menu.Core;
using Menu.Infrastructure;
using Menu.UI;
using OrderManager.Core.Service;
using OrderManager.Infrastructure;

namespace OrderManager.Command.CustomerCommand
{
    public sealed class NewCustomerCommand : ICommand
    {
        public string Title => "Новый пользователь";
        private readonly IUserInterface _ui;
        private readonly CustomerService _customerService;
        private readonly OrderService _orderService;
        private readonly ICommandRegistry _registry;

        public NewCustomerCommand(
            IUserInterface ui,
            CustomerService customerService,
            OrderService orderService,
            ICommandRegistry registry )
        {
            _ui = ui;
            _customerService = customerService;
            _orderService = orderService;
            _registry = registry;
        }

        public CommandResult Execute()
        {
            try
            {
                string? name = _ui.ReadLine( "Введите имя клиента: " );
                Guid id = _customerService.CreateCustomer( name );
                MenuCommand menu = CustomerMenu.BuildCustomerMenu(
                    _ui,
                    _customerService,
                    _orderService,
                    id,
                    _registry
                );

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