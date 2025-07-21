using OrderManager.Command.CustomerCommand;
using OrderManager.Model;
using OrderManager.Service;
using OrderManager.UI;
using OrderManager.UI.Menu;

namespace OrderManager
{
    public class App
    {
        private readonly IUserInterface _ui;
        private readonly CustomerService _customerService;
        private readonly OrderService _orderService;

        public App( IUserInterface ui, CustomerService customerService, OrderService orderService )
        {
            _ui = ui;
            _customerService = customerService;
            _orderService = orderService;
        }

        public void Run()
        {
            Menu customerListMenu = new( _ui, "Выберите пользователя:" );

            customerListMenu.Add( "1", new MenuAction( _ui, "Новый пользователь",
                new NewCustomerCommand( _ui, _customerService, _orderService ) ) );
            customerListMenu.Add( "2", new MenuAction( _ui, "Список пользователей",
                new ShowCustomerListCommand( _ui, _customerService, _orderService ) ) );
            customerListMenu.Add( "0", new MenuAction( _ui, "Выход" ), true );

            customerListMenu.Execute();
            _ui.WriteLine( "Спасибо за использование нашего сервиса. До свидания!" );
        }
    }
}