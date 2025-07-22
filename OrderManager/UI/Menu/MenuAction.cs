using OrderManager.Command;

namespace OrderManager.UI.Menu
{
    public class MenuAction : IMenuComponent
    {
        public string Title { get; }

        private readonly ICommand? _command;

        public MenuAction( string title, ICommand? command = null )
        {
            Title = title;
            _command = command;
        }

        public void Execute()
        {
            _command?.Execute();
        }
    }
}