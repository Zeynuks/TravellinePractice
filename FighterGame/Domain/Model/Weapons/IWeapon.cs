using FighterGame.Domain.Dice;
using FighterGame.Domain.Model.Types;

namespace FighterGame.Domain.Model.Weapons
{
    public interface IWeapon
    {
        public IDice Damage { get; }
        public DamageType DamageType { get; }
    }
}