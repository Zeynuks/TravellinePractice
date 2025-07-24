using Menu.Core;

namespace Menu.Commands
{
    /// <summary>Команда выхода из приложения.</summary>
    public class ExitCommand : ICommand
    {
        /// <inheritdoc/>
        public string Title => "Выход";

        /// <summary>Выполняет действие завершения работы приложения.</summary>
        /// <returns>Результат действия выхода.</returns>
        public CommandResult Execute()
        {
            return Results.Back();
        }
    }
}