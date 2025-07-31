using Menu.Commands;
using Menu.Core;
using Menu.Infrastructure.Menu;

namespace Menu.Infrastructure
{
    /// <summary>
    /// Управляет исполнением команд меню, поддерживая стек навигации.
    /// </summary>
    public class FlowRunner
    {
        private readonly IMenuRegistry _registry;
        private readonly Stack<IMenu> _menuStack = new();

        /// <summary>
        /// Создаёт новый экземпляр <see cref="FlowRunner"/>.
        /// </summary>
        /// <param name="start">Стартовая команда (корневое меню).</param>
        /// <param name="registry">Реестр для поиска команд по идентификатору.</param>
        /// <exception cref="ArgumentNullException">Если <paramref name="start"/> или <paramref name="registry"/> равны <c>null</c>.</exception>
        public FlowRunner( IMenu start, IMenuRegistry registry )
        {
            _registry = registry;
            _menuStack.Push( start );
        }

        /// <summary>
        /// Запускает цикл обработки команд до достижения действия <see cref="FlowAction.Exit"/>.
        /// </summary>
        public void Run()
        {
            while ( _menuStack.Any() )
            {
                IMenu current = _menuStack.Peek();
                CommandResult res = current.Execute();

                switch ( res.Action )
                {
                    case FlowAction.Continue:
                        continue;
                    case FlowAction.Back:
                        _menuStack.Pop();
                        continue;
                    case FlowAction.Navigate:
                        if ( res.NextMenuId is null || !_registry.TryGet( res.NextMenuId, out IMenu? next ) )
                        {
                            throw new InvalidOperationException( $"Неизвестный id: {res.NextMenuId}" );
                        }

                        _menuStack.Push( next! );
                        continue;
                    case FlowAction.Exit:
                        return;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}