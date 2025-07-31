namespace Menu.UI
{
    /// <summary>
    /// Абстракция пользовательского ввода-вывода (консоль, GUI и т. д.).
    /// </summary>
    public interface IUserInterface
    {
        /// <summary>Выводит строку текста с переходом на новую строку.</summary>
        /// <param name="text">Текст для вывода.</param>
        void WriteLine( string? text );

        /// <summary>Выводит строку текста без перехода на новую строку.</summary>
        /// <param name="text">Текст для вывода.</param>
        void Write( string text );

        /// <summary>Считывает строку от пользователя.</summary>
        /// <param name="prompt">Опциональная подсказка перед вводом.</param>
        /// <returns>Введённая пользователем строка или <c>null</c>.</returns>
        string? ReadLine( string? prompt = null );

        /// <summary>Считывает значение заданного типа от пользователя.</summary>
        /// <typeparam name="T">Структурный тип, поддерживающий <see cref="IParsable{T}"/>.</typeparam>
        /// <param name="prompt">Опциональная подсказка перед вводом.</param>
        /// <returns>Введённое пользователем значение типа <typeparamref name="T"/>.</returns>
        T ReadValue<T>( string? prompt = null )
            where T : struct, IParsable<T>;

        public ConsoleKey ReadKey( bool intercept = false );

        /// <summary>Очищает текущее окно или консоль.</summary>
        void Clear();
    }
}