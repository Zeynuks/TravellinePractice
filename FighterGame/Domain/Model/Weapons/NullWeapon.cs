using FighterGame.Domain.Dice;
using FighterGame.Domain.Model.Types;

namespace FighterGame.Domain.Model.Weapons
{
    public class NullWeapon : IWeapon
    {
        public IDice Damage => new D4();
        public DamageType DamageType { get; }

        public NullWeapon( DamageType damageType )
        {
            DamageType = damageType;
        }
    }
}