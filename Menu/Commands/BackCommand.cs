using Menu.Core;

namespace Menu.Commands
{
    /// <summary>Команда возврата к предыдущему пункту меню.</summary>
    public class BackCommand : ICommand
    {
        /// <inheritdoc/>
        public string Title => "Назад";

        /// <summary>Выполняет действие возврата назад.</summary>
        /// <returns>Результат действия возврата.</returns>
        public CommandResult Execute()
        {
            return Results.Back();
        }
    }
}