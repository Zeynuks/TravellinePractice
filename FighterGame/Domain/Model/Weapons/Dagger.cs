using FighterGame.Domain.Dice;
using FighterGame.Domain.Model.Types;

namespace FighterGame.Domain.Model.Weapons
{
    public class Dagger : IWeapon
    {
        public IDice Damage => new D6();
        public DamageType DamageType { get; }

        public Dagger( DamageType damageType )
        {
            DamageType = damageType;
        }
    }
}