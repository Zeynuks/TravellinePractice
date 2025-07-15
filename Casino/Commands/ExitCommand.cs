using Casino.Core;
using Casino.UI;

namespace Casino.Commands
{
    public class ExitCommand : ICommand
    {
        public bool ShouldExit => true;

        public void Execute( IUserInterface ui, IGameEngine engine, Wallet wallet )
        { 
            ui.WriteLine( "Удачи в следующий раз!" );
        }
    }
}