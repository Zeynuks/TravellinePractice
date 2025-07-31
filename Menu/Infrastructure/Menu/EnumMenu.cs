using Menu.Core;
using Menu.UI;

namespace Menu.Infrastructure.Menu
{
    /// <summary>
    /// Generic-меню для выбора значения перечисления TEnum.
    /// При выборе вызывает onSubmit и возвращает Results.Back().
    /// </summary>
    public sealed class EnumMenu<TEnum> : IMenu
        where TEnum : Enum
    {
        private readonly IUserInterface _ui;
        private readonly TEnum[] _values;
        private readonly Action<TEnum> _onSubmit;

        /// <summary>Уникальный идентификатор меню.</summary>
        public string MenuId { get; }

        /// <summary>Заголовок меню.</summary>
        public string Title { get; }

        public EnumMenu(
            IUserInterface ui,
            string menuId,
            Action<TEnum> onSubmit,
            string? title = null
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
            _values = Enum.GetValues( typeof( TEnum ) )
                .Cast<TEnum>()
                .ToArray();
            Title = string.IsNullOrWhiteSpace( title ) ? typeof( TEnum ).Name : title;
        }

        /// <summary>Отображает список значений перечисления, читает ввод и вызывает onSubmit.</summary>
        public CommandResult Execute()
        {
            while ( true )
            {
                _ui.WriteLine( Title );

                for ( int i = 0; i < _values.Length; i++ )
                {
                    _ui.WriteLine( ( i + 1 ) + " - " + _values[ i ] );
                }

                string? input = _ui.ReadLine( "> " );
                _ui.Clear();

                if ( int.TryParse( input, out int choice ) )
                {
                    if ( choice >= 1 && choice <= _values.Length )
                    {
                        TEnum selected = _values[ choice - 1 ];
                        _onSubmit( selected );
                        return Results.Back();
                    }
                }

                _ui.WriteLine( "Неверный выбор, попробуйте снова." );
            }
        }
    }
}