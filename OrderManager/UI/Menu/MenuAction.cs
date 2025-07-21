using OrderManager.Command;

namespace OrderManager.UI.Menu
{
    public class MenuAction : IMenuComponent
    {
        public string Title { get; }
        private readonly IUserInterface _ui;

        private readonly ICommand? _command;

        public MenuAction( IUserInterface userInterface, string title, ICommand? command = null )
        {
            _ui = userInterface;
            Title = title;
            _command = command;
        }

        public void Execute()
        {
            _ui.Clear();
            _command?.Execute();
        }
    }
}