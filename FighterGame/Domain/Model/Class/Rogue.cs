using FighterGame.Domain.Model.Types;
using FighterGame.Domain.Model.Weapons;

namespace FighterGame.Domain.Model.Class
{
    public class Rogue : IClass
    {
        public List<WeaponType> WeaponSkills => [ WeaponType.Dagger, WeaponType.Club ];
        public double DamageModify => 1.3;
    }
}