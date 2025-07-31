using Menu.Commands;
using Menu.Infrastructure.Menu;

namespace Menu.Infrastructure
{
    public class MenuRegistry : IMenuRegistry
    {
        private readonly Dictionary<string, IMenu> _map = new();

        public void Add( IMenu cmd )
        {
            _map[ cmd.MenuId ] = cmd;
        }

        public bool TryGet( string id, out IMenu? cmd )
        {
            return _map.TryGetValue( id, out cmd );
        }
    }
}