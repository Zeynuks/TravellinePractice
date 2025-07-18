using Casino.UI;

namespace Casino.Commands
{
    public class ExitCommand : ICommand
    {
        private readonly IUserInterface _ui;

        public ExitCommand( IUserInterface ui )
        {
            _ui = ui;
        }

        public void Execute()
        {
            _ui.WriteLine( "Удачи в следующий раз!" );
        }
    }
}