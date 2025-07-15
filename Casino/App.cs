using Casino.Commands;
using Casino.Core;
using Casino.UI;

namespace Casino
{
    public class App( IUserInterface ui, IGameEngine engine, Wallet wallet )
    {
        private readonly CommandFactory _commands = new();

        public void Run()
        {
            bool exitRequested = false;

            while ( !exitRequested )
            {
                string input = ui.ReadLine(
                    "\nВведите команду:\n1. Играть\n2. Проверить баланс\n3. Пополнить баланс\n0. Выход" );
                ui.Clear();
                ui.ShowBanner();

                if ( !_commands.TryGetCommand( input, out ICommand? command ) )
                {
                    ui.WriteLine( "Неверная команда. Попробуйте ещё раз." );
                    continue;
                }

                if ( command == null )
                {
                    continue;
                }

                command.Execute( ui, engine, wallet );
                exitRequested = command.ShouldExit;
            }
        }
    }
}