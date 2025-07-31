using Menu.Commands;
using Menu.Core;

namespace Menu.Infrastructure.Menu
{
    /// <summary>
    /// Базовый интерфейс для всех меню.
    /// </summary>
    public interface IMenu: ICommand
    {
        /// <summary>Уникальный идентификатор меню.</summary>
        string MenuId { get; }
    }
}