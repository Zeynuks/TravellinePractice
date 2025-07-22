namespace OrderManager.UI.Menu
{
    public class Menu : IMenuComponent
    {
        public string Title { get; }
        private readonly IUserInterface _ui;
        private readonly Dictionary<string, (IMenuComponent Component, bool IsExit)> _items = new();

        public Menu( IUserInterface userInterface, string? title = null )
        {
            _ui = userInterface;
            Title = string.IsNullOrWhiteSpace( title ) ? "Выберите команду:" : title!;
        }

        public void Add( string key, IMenuComponent item, bool isExit = false )
        {
            if ( string.IsNullOrWhiteSpace( key ) )
            {
                throw new Exception( "Ключ не может быть нулевым" );
            }

            if ( _items.ContainsKey( key ) )
            {
                throw new Exception( $"Пункт с ключом «{key}» уже существует." );
            }

            _items[ key ] = ( item, isExit );
        }

        public void Execute()
        {
            while ( true )
            {
                if ( !string.IsNullOrEmpty( Title ) )
                    _ui.WriteLine( Title );

                foreach ( KeyValuePair<string, (IMenuComponent Component, bool IsExit)> item in _items )
                {
                    _ui.WriteLine( $"{item.Key}. {item.Value.Component.Title}" );
                }

                string? choice = _ui.ReadLine();
                _ui.Clear();

                if ( choice == null || !_items.TryGetValue( choice, out var entry ) )
                {
                    _ui.WriteLine( "Неверный выбор, попробуйте снова." );
                    continue;
                }

                entry.Component.Execute();
                if ( entry.IsExit )
                {
                    break;
                }
            }
        }
    }
}