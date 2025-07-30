using FighterGame.Domain.Model.Types;
using FighterGame.Domain.Model.Weapons;

namespace FighterGame.Domain.Model.Class
{
    public class Knight : IClass
    {
        public List<WeaponType> WeaponSkills => [ WeaponType.Sword, WeaponType.Dagger ];
        public double DamageModify => 0.8;
    }
}