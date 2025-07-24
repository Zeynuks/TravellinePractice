namespace Menu.Core
{
    /// <summary>
    /// Перечисляет возможные действия после выполнения команды в меню.
    /// </summary>
    public enum FlowAction
    {
        /// <summary>Продолжить выполнение текущей команды.</summary>
        Continue,
        /// <summary>Вернуться к предыдущему пункту меню.</summary>
        Back,
        /// <summary>Перейти к следующему меню по идентификатору.</summary>
        Navigate,
        /// <summary>Выйти из приложения.</summary>
        ExitApp
    }
}