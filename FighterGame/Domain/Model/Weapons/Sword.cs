using FighterGame.Domain.Dice;
using FighterGame.Domain.Model.Types;

namespace FighterGame.Domain.Model.Weapons
{
    public class Sword: IWeapon
    {
        public int Damage => new DiceTypes.D12().Roll();
        public DamageType DamageType { get; }
        
        public Sword( DamageType damageType )
        {
            DamageType = damageType;
        }
    }
}