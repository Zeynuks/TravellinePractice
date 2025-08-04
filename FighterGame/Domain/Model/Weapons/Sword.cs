using FighterGame.Domain.Dice;
using FighterGame.Domain.Model.Types;

namespace FighterGame.Domain.Model.Weapons
{
    public class Sword: IWeapon
    {
        public IDice Damage => new D12();
        public DamageType DamageType { get; }
        
        public Sword( DamageType damageType )
        {
            DamageType = damageType;
        }
    }
}