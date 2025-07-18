using Casino.Core;
using Casino.UI;

namespace Casino.Commands
{
    public class CommandFactory
    {
        private readonly Dictionary<string, Func<ICommand>> _registry;

        public CommandFactory( IUserInterface ui, IGameEngine engine, Wallet wallet )
        {
            IUserInterface ui1 = ui;
            IGameEngine engine1 = engine;
            Wallet wallet1 = wallet;

            _registry = new Dictionary<string, Func<ICommand>>
            {
                [ "1" ] = () => new PlayCommand( ui1, engine1, wallet1 ),
                [ "2" ] = () => new CheckBalanceCommand( ui1, wallet1 ),
                [ "3" ] = () => new DepositCommand( ui1, wallet1 ),
                [ "0" ] = () => new ExitCommand( ui1 ),
            };
        }

        public bool TryGetCommand( string key, out ICommand? command )
        {
            if ( _registry.TryGetValue( key, out Func<ICommand>? ctor ) )
            {
                command = ctor();

                return true;
            }

            command = null;

            return false;
        }
    }
}