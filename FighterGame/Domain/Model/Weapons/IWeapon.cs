using FighterGame.Domain.Model.Types;

namespace FighterGame.Domain.Model.Weapons
{
    public interface IWeapon
    {
        public int Damage { get; }
        public DamageType DamageType { get; }
    }
}