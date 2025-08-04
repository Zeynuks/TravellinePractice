using FighterGame.Domain.Dice;
using FighterGame.Domain.Model.Types;

namespace FighterGame.Domain.Model.Weapons
{
    public class Spear : IWeapon
    {
        public IDice Damage => new D10();
        public DamageType DamageType { get; }
        
        public Spear( DamageType damageType )
        {
            DamageType = damageType;
        }
    }
}