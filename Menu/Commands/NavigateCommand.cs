using Menu.Core;

namespace Menu.Commands
{
    /// <summary>
    /// Команда навигации: переходит к меню с заданным идентификатором.
    /// </summary>
    public class NavigateCommand : ICommand
    {
        /// <summary>Заголовок команды.</summary>
        public string Title { get; }

        private readonly string _targetMenuId;

        /// <summary>
        /// Создаёт новую команду навигации.
        /// </summary>
        /// <param name="targetMenuId">Идентификатор целевого меню.</param>
        /// <param name="title">Текст для отображения в меню.</param>
        public NavigateCommand(string targetMenuId, string title)
        {
            _targetMenuId = targetMenuId;
            Title = title;
        }

        /// <summary>Выполняет переход к целевому меню.</summary>
        /// <returns>Результат действия навигации.</returns>
        public CommandResult Execute() => Results.Navigate(_targetMenuId);
    }
}