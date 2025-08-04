using Menu.Infrastructure.Menu;

namespace Menu.Infrastructure
{
    public class MenuRegistry : IMenuRegistry
    {
        private readonly Dictionary<string, IMenu> _map = new();

        public void Add( IMenu cmd )
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd), "Команда не может быть null.");
            }

            if (string.IsNullOrEmpty(cmd.MenuId))
            {
                throw new ArgumentException("У команды должен быть уникальный идентификатор.", nameof(cmd));
            }

            // Добавляем команду в реестр
            _map[cmd.MenuId] = cmd;
        }

        public bool TryGet( string id, out IMenu? cmd )
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Идентификатор не может быть пустым.", nameof(id));
            }

            return _map.TryGetValue(id, out cmd);
        }

        public bool Remove( string id )
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Идентификатор не может быть пустым.", nameof(id));
            }

            return _map.Remove(id);
        }
    }
}