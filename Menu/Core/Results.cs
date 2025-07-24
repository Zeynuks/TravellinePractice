namespace Menu.Core
{
    /// <summary>
    /// Фабричные методы для создания стандартных <see cref="CommandResult"/>.
    /// </summary>
    public static class Results
    {
        /// <summary>Результат продолжения текущего меню.</summary>
        public static CommandResult Continue() => new(FlowAction.Continue);
        /// <summary>Результат возврата к предыдущему меню.</summary>
        public static CommandResult Back()     => new(FlowAction.Back);
        /// <summary>Результат выхода из приложения.</summary>
        public static CommandResult Exit()     => new(FlowAction.Exit);
        /// <summary>Результат перехода к меню с указанным идентификатором.</summary>
        /// <param name="id">Идентификатор целевого меню.</param>
        public static CommandResult Navigate(string id) => new(FlowAction.Navigate, id);
    }
}