using Menu.Core;

namespace Menu.Infrastructure.Menu
{
    /// <summary>
    /// Базовый интерфейс для всех меню.
    /// </summary>
    public interface IMenu
    {
        /// <summary>Уникальный идентификатор меню.</summary>
        string MenuId { get; }

        /// <summary>Заголовок, отображаемый при рендеринге.</summary>
        string Title { get; }

        /// <summary>
        /// Отображает меню, обрабатывает ввод и возвращает результат выполнения.
        /// </summary>
        CommandResult Execute();
    }
}