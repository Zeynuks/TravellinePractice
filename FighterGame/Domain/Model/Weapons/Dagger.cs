using FighterGame.Domain.Dice;
using FighterGame.Domain.Model.Types;

namespace FighterGame.Domain.Model.Weapons
{
    public class Dagger : IWeapon
    {
        public int Damage => new DiceTypes.D6().Roll();
        public DamageType DamageType { get; }

        public Dagger( DamageType damageType )
        {
            DamageType = damageType;
        }
    }
}