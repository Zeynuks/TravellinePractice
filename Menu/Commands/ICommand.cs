using Menu.Core;

namespace Menu.Commands
{
    /// <summary>
    /// Базовый интерфейс команды меню: заголовок и логика выполнения.
    /// </summary>
    public interface ICommand
    {
        /// <summary>Заголовок команды, отображаемый в меню.</summary>
        string Title { get; }

        /// <summary>Выполняет команду и возвращает результат навигации.</summary>
        /// <returns>Результат выполнения команды.</returns>
        CommandResult Execute();
    }
}