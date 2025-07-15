namespace Casino.Commands
{
    public class CommandFactory
    {
        private readonly Dictionary<string, Func<ICommand>> _registry = new()
        {
            [ "1" ] = () => new PlayCommand(),
            [ "2" ] = () => new CheckBalanceCommand(),
            [ "3" ] = () => new DepositCommand(),
            [ "0" ] = () => new ExitCommand(),
        };

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