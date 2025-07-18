namespace Casino.UI.Menu
{
    public class Menu : IMenuComponent
    {
        public string Title { get; }
        private readonly List<IMenuComponent> _items = [ ];
        private readonly IUserInterface _ui;

        public Menu( IUserInterface userInterface, string title )
        {
            _ui = userInterface;
            Title = title;
        }

        public void Add( IMenuComponent item )
        {
            ArgumentNullException.ThrowIfNull( item );
            _items.Add( item );
        }

        public void Execute()
        {
            while ( true )
            {
                try
                {
                    if ( !string.IsNullOrWhiteSpace( Title ) )
                    {
                        _ui.WriteLine( Title );
                    }

                    for ( int i = 0; i < _items.Count; i++ )
                    {
                        _ui.WriteLine( $"{i + 1}. {_items[ i ].Title}" );
                    }

                    _ui.WriteLine( "0. Выход" );

                    int choice = _ui.ReadValue<int>();

                    if ( choice == 0 )
                    {
                        break;
                    }

                    if ( choice < 1 || choice > _items.Count )
                    {
                        _ui.Clear();
                        _ui.ShowBanner();
                        _ui.WriteLine( "Неверный выбор, попробуйте снова." );
                        continue;
                    }

                    _items[ choice - 1 ].Execute();
                }
                catch ( Exception ex )
                {
                    _ui.WriteLine( ex.Message );
                }
            }
        }
    }
}