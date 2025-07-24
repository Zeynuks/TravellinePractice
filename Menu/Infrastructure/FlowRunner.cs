using Menu.Commands;
using Menu.Core;

namespace Menu.Infrastructure
{
    /// <summary>
    /// Управляет исполнением команд меню, поддерживая стек навигации.
    /// </summary>
    public sealed class FlowRunner
    {
        private readonly ICommandRegistry _registry;
        private readonly Stack<ICommand> _stack = new();

        /// <summary>
        /// Создаёт новый экземпляр <see cref="FlowRunner"/>.
        /// </summary>
        /// <param name="start">Стартовая команда (корневое меню).</param>
        /// <param name="registry">Реестр для поиска команд по идентификатору.</param>
        /// <exception cref="ArgumentNullException">Если <paramref name="start"/> или <paramref name="registry"/> равны <c>null</c>.</exception>
        public FlowRunner(ICommand start, ICommandRegistry registry)
        {
            _registry = registry ;
            _stack.Push(start);
        }

        /// <summary>
        /// Запускает цикл обработки команд до достижения действия <see cref="FlowAction.ExitApp"/>.
        /// </summary>
        public void Run()
        {
            while (_stack.Count > 0)
            {
                var current = _stack.Peek();
                var res = current.Execute();

                switch (res.Action)
                {
                    case FlowAction.Continue:
                        continue;
                    case FlowAction.Back:
                        _stack.Pop();
                        continue;
                    case FlowAction.Navigate:
                        if (res.NextMenuId == null || !_registry.TryGet(res.NextMenuId, out var next))
                        {
                            throw new InvalidOperationException($"Неизвестный id: {res.NextMenuId}");
                        }

                        _stack.Push(next!);
                        continue;
                    case FlowAction.ExitApp:
                        return;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}