using Menu.Core;

namespace Menu.Commands
{
    /// <summary>Команда возврата к предыдущему пункту меню.</summary>
    public class BackCommand : ICommand
    {
        private const string _title = "Назад";

        /// <inheritdoc/>
        public string Title => _title;

        /// <summary>Выполняет действие возврата назад.</summary>
        /// <returns>Результат действия возврата.</returns>
        public CommandResult Execute()
        {
            return Results.Back();
        }
    }
}