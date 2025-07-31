using FighterGame.Domain.Model.Class;
using FighterGame.Domain.Model.Types;

namespace FighterGame.Domain.Factory
{
    public class ClassFactory
    {
        public IClass CreateClass( ClassType classType )
        {
            return classType switch
            {
                ClassType.Gladiator => new Gladiator(),
                ClassType.Knight => new Knight(),
                ClassType.Rogue => new Rogue(),
                _ => throw new ArgumentOutOfRangeException( nameof( classType ) )
            };
        }
    }
}