using Menu.Commands;
using Menu.Core;
using Menu.UI;

namespace Menu.Infrastructure.Menu
{
    public sealed class ArrowMenu : IMenu
    {
        private record MenuOption( ICommand Command );

        private readonly IUserInterface _ui;
        private readonly List<MenuOption> _options = [ ];
        public string MenuId { get; }
        public string Title { get; set; }

        public ArrowMenu(
            IUserInterface ui,
            string menuId,
            string? title = null
        )
        {
            if ( string.IsNullOrWhiteSpace( menuId ) )
            {
                throw new ArgumentNullException( nameof( menuId ) );
            }

            _ui = ui;
            MenuId = menuId;
            Title = string.IsNullOrWhiteSpace( title ) ? "Выберите пункт:" : title;
        }

        public void AddOption( ICommand command )
        {
            if ( string.IsNullOrWhiteSpace( command.Title ) )
            {
                throw new ArgumentNullException( nameof( command.Title ) );
            }

            _options.Add( new MenuOption( command ) );
        }

        public CommandResult Execute()
        {
            if ( _options.Count == 0 )
            {
                return CommandResults.Continue();
            }

            int selectedIndex = 0;

            while ( true )
            {
                RenderMenu( selectedIndex );

                ConsoleKey key = _ui.ReadKey( true );

                switch ( key )
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = ( selectedIndex - 1 + _options.Count ) % _options.Count;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = ( selectedIndex + 1 ) % _options.Count;
                        break;
                    case ConsoleKey.Backspace:
                        return CommandResults.Back();
                    case ConsoleKey.Enter:
                        MenuOption selected = _options[ selectedIndex ];
                        CommandResult result = selected.Command.Execute();
                        return result;
                    default:
                        _ui.WriteLine( "Неверный выбор, попробуйте снова." );
                        return CommandResults.Continue();
                }
            }
        }

        private void RenderMenu( int selectedIndex )
        {
            _ui.Clear();
            _ui.WriteLine( Title );
            _ui.WriteLine( "↑↓ — навигация, Enter — выбрать, Backspace — назад" );

            for ( int i = 0; i < _options.Count; i++ )
            {
                string pointer = i == selectedIndex ? "►" : " ";
                _ui.WriteLine( pointer + " " + _options[ i ].Command.Title );
            }
        }
    }
}