using FighterGame.Domain.Model.Types;
using FighterGame.Domain.Model.Weapons;

namespace FighterGame.Domain.Model.Class
{
    public class Gladiator : IClass
    {
        public List<WeaponType> WeaponSkills => [ WeaponType.Spear, WeaponType.Unarmed ];
        public double DamageModify => 1;
    }
}