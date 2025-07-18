using Casino.Command;

namespace Casino.UI.Menu
{
    public class MenuAction : IMenuComponent
    {
        public string Title { get; }
        private readonly IUserInterface _ui;

        private readonly ICommand[] _commands;

        public MenuAction( IUserInterface userInterface, string title, params ICommand[] commands )
        {
            _ui = userInterface;
            Title = title;
            _commands = commands;
        }

        public void Execute()
        {
            _ui.Clear();
            _ui.ShowBanner();
            foreach ( ICommand command in _commands )
            {
                command.Execute();
            }
        }
    }
}