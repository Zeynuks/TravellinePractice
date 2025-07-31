using Menu.Core;

namespace Menu.Commands
{
    /// <summary>Команда выхода из приложения.</summary>
    public class ExitCommand : ICommand
    {
        private const string _title = "Выход";

        /// <inheritdoc/>
        public string Title => _title;

        /// <summary>Выполняет действие завершения работы приложения.</summary>
        /// <returns>Результат действия выхода.</returns>
        public CommandResult Execute()
        {
            return Results.Exit();
        }
    }
}