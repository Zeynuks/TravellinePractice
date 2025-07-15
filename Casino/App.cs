using Casino.Commands;
using Casino.Core;
using Casino.UI;

namespace Casino
{
    public class App
    {
        private readonly CommandFactory _commands = new();
        private readonly IUserInterface _ui;
        private readonly IGameEngine _engine;
        private readonly Wallet _wallet;

        public App( IUserInterface ui, IGameEngine engine, Wallet wallet )
        {
            _ui = ui;
            _engine = engine;
            _wallet = wallet;
        }

        public void Run()
        {
            bool exitRequested = false;

            while ( !exitRequested )
            {
                string input = _ui.ReadLine(
                    "\nВведите команду:\n1. Играть\n2. Проверить баланс\n3. Пополнить баланс\n0. Выход" );
                _ui.Clear();
                _ui.ShowBanner();

                if ( !_commands.TryGetCommand( input, out ICommand? command ) )
                {
                    _ui.WriteLine( "Неверная команда. Попробуйте ещё раз." );
                    continue;
                }

                command!.Execute( _ui, _engine, _wallet );
                exitRequested = command.ShouldExit;
            }
        }
    }
}