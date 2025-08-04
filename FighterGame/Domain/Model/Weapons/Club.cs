using FighterGame.Domain.Dice;
using FighterGame.Domain.Model.Types;

namespace FighterGame.Domain.Model.Weapons
{
    public class Club: IWeapon
    {
        public IDice Damage => new D8();
        public DamageType DamageType { get; }
        
        public Club( DamageType damageType )
        {
            DamageType = damageType;
        }
    }
}