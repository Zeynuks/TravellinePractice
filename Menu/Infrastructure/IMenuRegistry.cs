using Menu.Commands;
using Menu.Infrastructure.Menu;

namespace Menu.Infrastructure
{
    /// <summary>Реестр команд для навигации по строковым идентификаторам.</summary>
    public interface IMenuRegistry
    {
        /// <summary>Добавляет команду в реестр.</summary>
        /// <param name="cmd">Команда с уникальным <see cref="ICommand.Title"/> или <c>null</c>.</param>
        void Add( IMenu cmd );

        /// <summary>Пытается получить команду по её идентификатору.</summary>
        /// <param name="id">Идентификатор команды.</param>
        /// <param name="cmd">Найденная команда или <c>null</c>.</param>
        /// <returns><c>true</c>, если команда найдена; иначе <c>false</c>.</returns>
        bool TryGet( string id, out IMenu? cmd );
    }
}