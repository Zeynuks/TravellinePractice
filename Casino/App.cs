using Casino.Commands;
using Casino.Core;
using Casino.UI;

namespace Casino
{
    public class App
    {
        private readonly IUserInterface _ui;
        private readonly CommandFactory _commands;

        public App( IUserInterface ui, CommandFactory commands )
        {
            _ui = ui;
            _commands = commands;
        }

        public void Run()
        {
            bool exitRequested = false;

            while ( !exitRequested )
            {
                string? input = _ui.ReadLine(
                    "\nВведите команду:\n1. Играть\n2. Проверить баланс\n3. Пополнить баланс\n0. Выход" );
                _ui.Clear();
                _ui.ShowBanner();

                if ( input == null || !_commands.TryGetCommand( input, out ICommand? command ) )
                {
                    _ui.WriteLine( "Неверная команда. Попробуйте ещё раз." );
                    continue;
                }

                if ( command is ExitCommand )
                {
                    exitRequested = true;
                }

                command!.Execute();
            }
        }
    }
}