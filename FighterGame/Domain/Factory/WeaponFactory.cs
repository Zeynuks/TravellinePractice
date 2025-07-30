using FighterGame.Domain.Model.Types;
using FighterGame.Domain.Model.Weapons;

namespace FighterGame.Domain.Factory
{
    public class WeaponFactory
    {
        public IWeapon CreateWeapon( WeaponType weaponType, DamageType damageType )
        {
            return weaponType switch
            {
                WeaponType.Club => new Club( damageType ),
                WeaponType.Dagger => new Dagger( damageType ),
                WeaponType.Spear => new Spear( damageType ),
                WeaponType.Sword => new Sword( damageType ),
                WeaponType.Unarmed => new NullWeapon( damageType ),
                _ => throw new ArgumentOutOfRangeException( nameof( weaponType ) )
            };
        }
    }
}