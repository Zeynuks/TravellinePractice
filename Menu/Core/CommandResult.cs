namespace Menu.Core
{
    /// <summary>
    /// Представляет результат выполнения команды: действие и идентификатор следующего меню.
    /// </summary>
    /// <param name="Action">Действие после выполнения команды.</param>
    /// <param name="NextMenuId">
    /// Идентификатор следующего меню (используется при навигации), либо <c>null</c>.
    /// </param>
    public record CommandResult(FlowAction Action, string? NextMenuId = null);
}