using FighterGame.Domain.Dice;
using FighterGame.Domain.Model.Types;

namespace FighterGame.Domain.Model.Weapons
{
    public class Club: IWeapon
    {
        public int Damage => new DiceTypes.D8().Roll();
        public DamageType DamageType { get; }
        
        public Club( DamageType damageType )
        {
            DamageType = damageType;
        }
    }
}