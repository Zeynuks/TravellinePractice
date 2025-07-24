using Menu.Commands;

namespace Menu.Infrastructure
{
    public class CommandRegistry : ICommandRegistry
    {
        private readonly Dictionary<string, ICommand?> _map = new();

        public void Add( ICommand? cmd )
        {
            if ( cmd is MenuCommand m )
            {
                _map[ m.MenuId ] = cmd;
            }
        }

        public bool TryGet( string id, out ICommand? cmd ) => _map.TryGetValue( id, out cmd );
    }
}