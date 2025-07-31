using FighterGame.Domain.Dice;
using FighterGame.Domain.Model.Types;

namespace FighterGame.Domain.Model.Weapons
{
    public class NullWeapon : IWeapon
    {
        public int Damage => new DiceTypes.D4().Roll();
        public DamageType DamageType { get; }

        public NullWeapon( DamageType damageType )
        {
            DamageType = damageType;
        }
    }
}