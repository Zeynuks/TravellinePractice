using Menu.Commands;
using Menu.Core;
using Menu.UI;

namespace Menu.Infrastructure.Menu
{
    /// <summary>
    /// Команда меню: отображает список опций, считывает выбор и выполняет связанную команду.
    /// </summary>
    public class CommandMenu : IMenu
    {
        private readonly IUserInterface _ui;
        private readonly Dictionary<string, ICommand> _options = new();

        /// <summary>Уникальный идентификатор меню.</summary>
        public string MenuId { get; }

        /// <summary>Заголовок меню.</summary>
        public string Title { get; }

        /// <summary>
        /// Создаёт новую команду меню.
        /// </summary>
        /// <param name="ui">Абстракция для ввода-вывода.</param>
        /// <param name="menuId">Уникальный идентификатор меню.</param>
        /// <param name="title">Заголовок меню (или «Выберите команду:» по умолчанию).</param>
        /// <param name="items">
        /// Коллекция элементов меню: кортеж (Key, Cmd, IsExit), где
        /// <list type="bullet">
        /// <item><term>Key</term><description>строка ввода пользователем;</description></item>
        /// <item><term>Cmd</term><description>команда для выполнения;</description></item>
        /// <item><term>IsExit</term><description>признак возврата назад после выполнения.</description></item>
        /// </list>
        /// </param>
        public CommandMenu(
            IUserInterface ui,
            string menuId,
            string? title = null
        )
        {
            _ui = ui;
            MenuId = menuId;
            Title = string.IsNullOrWhiteSpace( title ) ? "Выберите команду:" : title!;
        }

        /// <summary>
        /// Отображает меню, запрашивает ввод и выполняет соответствующую команду.
        /// </summary>
        /// <returns>
        /// Результат выполнения:
        /// переход к другому меню, возврат назад, выход или повтор меню.
        /// </returns>
        public CommandResult Execute()
        {
            _ui.WriteLine( Title );
            foreach ( KeyValuePair<string, ICommand> kv in _options )
            {
                _ui.WriteLine( $"{kv.Key} - {kv.Value.Title}" );
            }

            string? choice = _ui.ReadLine( "> " );
            _ui.Clear();

            if ( choice is not null && _options.TryGetValue( choice, out ICommand? opt ) )
            {
                return opt.Execute();
            }

            _ui.WriteLine( "Неверный выбор, попробуйте снова." );

            return Results.Continue();
        }

        public void InsertOption( string key, ICommand command )
        {
            if ( !_options.ContainsKey( key ) )
            {
                _options[ key ] = command;
            }
        }
    }
}