using System;
using System.Collections.Generic;
using Menu.Core;
using Menu.UI;

namespace Menu.Infrastructure.Menu
{
    /// <summary>
    /// Интерактивное меню: навигация стрелками, выбор Enter, назад — Backspace.
    /// Поддерживает одиночный и множественный выбор.
    /// При подтверждении вызывает onSubmit с IEnumerable<Guid> выбранных опций.
    /// </summary>
    public sealed class ArrowMenu : IMenu
    {
        private record MenuOption( Guid Id, string Title );

        private readonly IUserInterface _ui;
        private readonly List<MenuOption> _options = [ ];
        private readonly bool _multiSelect;
        private readonly Action<IEnumerable<Guid>> _onSubmit;

        public string MenuId { get; }
        public string Title { get; }

        public ArrowMenu(
            IUserInterface ui,
            string menuId,
            Action<IEnumerable<Guid>> onSubmit,
            string? title = null,
            bool multiSelect = false
        )
        {
            if ( string.IsNullOrWhiteSpace( menuId ) )
            {
                throw new ArgumentNullException( nameof( menuId ) );
            }

            ArgumentNullException.ThrowIfNull( onSubmit );

            _ui = ui;
            MenuId = menuId;
            _onSubmit = onSubmit;
            _multiSelect = multiSelect;

            Title = string.IsNullOrWhiteSpace( title ) ? "Выберите пункт:" : title;
        }

        /// <summary>Добавляет опцию меню с заданным Guid и заголовком.</summary>
        public void AddOption( Guid id, string title )
        {
            if ( id == Guid.Empty )
            {
                throw new ArgumentException( "Идентификатор не может быть пустым GUID.", nameof( id ) );
            }

            if ( string.IsNullOrWhiteSpace( title ) )
            {
                throw new ArgumentNullException( nameof( title ) );
            }

            _options.Add( new MenuOption( id, title ) );
        }

        /// <summary>Показывает меню, обрабатывает ввод и вызывает onSubmit.</summary>
        public CommandResult Execute()
        {
            if ( _options.Count == 0 )
            {
                return Results.Continue();
            }

            int selectedIndex = 0;
            bool[] flags = _multiSelect ? new bool[ _options.Count ] : [ ];

            while ( true )
            {
                RenderMenu( selectedIndex, flags );

                ConsoleKey key = _ui.ReadKey( intercept: true );

                switch ( key )
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = ( selectedIndex - 1 + _options.Count ) % _options.Count;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = ( selectedIndex + 1 ) % _options.Count;
                        break;
                    case ConsoleKey.Spacebar when _multiSelect:
                        flags[ selectedIndex ] = !flags[ selectedIndex ];
                        break;
                    case ConsoleKey.Backspace:
                        return Results.Back();
                    case ConsoleKey.Enter:
                        List<Guid> chosenIds = GetChosenIds( selectedIndex, flags );
                        _onSubmit( chosenIds );
                        return Results.Back();
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

            string tips = _multiSelect
                ? "↑↓ — навигация, Space — отметить/снять, Enter — подтвердить, Esc — назад"
                : "↑↓ — навигация, Enter — выбрать, Esc — назад";

            _ui.WriteLine( tips );

            for ( int i = 0; i < _options.Count; i++ )
            {
                string pointer = i == selectedIndex ? "►" : " ";

                string selector = string.Empty;
                if ( _multiSelect )
                {
                    selector = flags[ i ] ? "[x] " : "[ ] ";
                }

                _ui.WriteLine( pointer + " " + selector + _options[ i ].Title );
            }
        }

        private List<Guid> GetChosenIds( int selectedIndex, bool[] flags )
        {
            List<Guid> chosenIds = [ ];

            if ( _multiSelect )
            {
                for ( int i = 0; i < flags.Length; i++ )
                {
                    if ( flags[ i ] )
                    {
                        chosenIds.Add( _options[ i ].Id );
                    }
                }

                if ( chosenIds.Count == 0 )
                {
                    chosenIds.Add( _options[ selectedIndex ].Id );
                }
            }
            else
            {
                chosenIds.Add( _options[ selectedIndex ].Id );
            }

            return chosenIds;
        }
    }
}