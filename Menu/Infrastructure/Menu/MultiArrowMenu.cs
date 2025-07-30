using System;
using System.Collections.Generic;
using Menu.Commands;
using Menu.Core;
using Menu.UI;

namespace Menu.Infrastructure.Menu
{
    public sealed class MultiArrowMenu : IMenu
    {
        private record MenuOption( Guid Id, string Title );

        private readonly IUserInterface _ui;
        private readonly List<MenuOption> _options = new();
        private readonly ICommand _command;
        private readonly Action<IEnumerable<Guid>>? _onSubmit;

        public string MenuId { get; }
        public string Title { get; }

        public MultiArrowMenu(
            IUserInterface ui,
            string menuId,
            ICommand command,
            string? title = null,
            Action<IEnumerable<Guid>>? onSubmit = null
        )
        {
            if ( string.IsNullOrWhiteSpace( menuId ) )
            {
                throw new ArgumentNullException( nameof( menuId ) );
            }

            ArgumentNullException.ThrowIfNull( command );

            _ui = ui;
            MenuId = menuId;
            _command = command;
            _onSubmit = onSubmit;
            Title = string.IsNullOrWhiteSpace( title ) ? "Выберите пункты:" : title;
        }

        public void AddOption( Guid id, string title )
        {
            if ( id == Guid.Empty )
            {
                throw new ArgumentException( "Пустой GUID", nameof( id ) );
            }

            if ( string.IsNullOrWhiteSpace( title ) )
            {
                throw new ArgumentNullException( nameof( title ) );
            }

            _options.Add( new MenuOption( id, title ) );
        }

        public CommandResult Execute()
        {
            if ( _options.Count == 0 )
            {
                return Results.Continue();
            }

            int selectedIndex = 0;
            bool[] flags = new bool[ _options.Count ];

            while ( true )
            {
                RenderMenu( selectedIndex, flags );

                ConsoleKey key = _ui.ReadKey( true );

                switch ( key )
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = ( selectedIndex - 1 + _options.Count ) % _options.Count;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = ( selectedIndex + 1 ) % _options.Count;
                        break;
                    case ConsoleKey.Spacebar:
                        flags[ selectedIndex ] = !flags[ selectedIndex ];
                        break;
                    case ConsoleKey.Backspace:
                        return Results.Back();
                    case ConsoleKey.Enter:
                        List<Guid> selectedIds = GetSelectedIds( flags, selectedIndex );
                        _onSubmit?.Invoke( selectedIds );
                        return _command.Execute();
                    default:
                        _ui.WriteLine( "Неверный выбор, попробуйте снова." );
                        return Results.Continue();
                }
            }
        }

        private void RenderMenu( int selectedIndex, bool[] flags )
        {
            _ui.Clear();
            _ui.WriteLine( Title );
            _ui.WriteLine( "↑↓ — навигация, Space — отметить, Enter — выполнить, Backspace — назад" );

            for ( int i = 0; i < _options.Count; i++ )
            {
                string pointer = i == selectedIndex ? "►" : " ";
                string selector = flags[ i ] ? "[x] " : "[ ] ";
                _ui.WriteLine( pointer + " " + selector + _options[ i ].Title );
            }
        }

        private List<Guid> GetSelectedIds( bool[] flags, int fallbackIndex )
        {
            List<Guid> result = new();

            for ( int i = 0; i < flags.Length; i++ )
            {
                if ( flags[ i ] )
                {
                    result.Add( _options[ i ].Id );
                }
            }

            if ( result.Count == 0 )
            {
                result.Add( _options[ fallbackIndex ].Id );
            }

            return result;
        }
    }
}