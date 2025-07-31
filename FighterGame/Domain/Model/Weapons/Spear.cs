using FighterGame.Domain.Dice;
using FighterGame.Domain.Model.Types;

namespace FighterGame.Domain.Model.Weapons
{
    public class Spear : IWeapon
    {
        public int Damage => new DiceTypes.D10().Roll();
        public DamageType DamageType { get; }
        
        public Spear( DamageType damageType )
        {
            DamageType = damageType;
        }
    }
}