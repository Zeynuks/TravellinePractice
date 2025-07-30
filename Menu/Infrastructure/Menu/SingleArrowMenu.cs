using System;
using System.Collections.Generic;
using Menu.Commands;
using Menu.Core;
using Menu.UI;

namespace Menu.Infrastructure.Menu
{
    public sealed class SingleArrowMenu : IMenu
    {
        private record MenuOption( Guid Id, string Title, ICommand Command );

        private readonly IUserInterface _ui;
        private readonly List<MenuOption> _options = [ ];
        private readonly Action<IEnumerable<Guid>>? _onSubmit;

        public string MenuId { get; }
        public string Title { get; }

        public SingleArrowMenu(
            IUserInterface ui,
            string menuId,
            string? title = null,
            Action<IEnumerable<Guid>>? onSubmit = null
        )
        {
            if ( string.IsNullOrWhiteSpace( menuId ) )
            {
                throw new ArgumentNullException( nameof( menuId ) );
            }

            _ui = ui;
            MenuId = menuId;
            Title = string.IsNullOrWhiteSpace( title ) ? "Выберите пункт:" : title;
            _onSubmit = onSubmit;
        }

        public void AddOption( Guid id, string title, ICommand command )
        {
            if ( id == Guid.Empty )
            {
                throw new ArgumentException( "Пустой GUID", nameof( id ) );
            }

            if ( string.IsNullOrWhiteSpace( title ) )
            {
                throw new ArgumentNullException( nameof( title ) );
            }

            _options.Add( new MenuOption( id, title, command ) );
        }

        public CommandResult Execute()
        {
            if ( _options.Count == 0 )
            {
                return Results.Continue();
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
                        return Results.Back();
                    case ConsoleKey.Enter:
                        MenuOption selected = _options[ selectedIndex ];
                        CommandResult result = selected.Command.Execute();
                        _onSubmit?.Invoke( [ selected.Id ] );
                        return result;
                    default:
                        _ui.WriteLine( "Неверный выбор, попробуйте снова." );
                        return Results.Continue();
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
                _ui.WriteLine( pointer + " " + _options[ i ].Title );
            }
        }
    }
}