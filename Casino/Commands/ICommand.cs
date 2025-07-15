using Casino.Core;
using Casino.UI;

namespace Casino.Commands
{
    public interface ICommand
    {
        void Execute( IUserInterface ui, IGameEngine engine, Wallet wallet );

        bool ShouldExit { get; }
        //TODO Придумать как избавится от флага
    }
}