using FighterGame.Domain.Model.Types;

namespace FighterGame.Domain.Model.Class
{
    public class Gladiator : IClass
    {
        public List<WeaponType> WeaponSkills => [ WeaponType.Spear, WeaponType.Unarmed ];
        public double DamageModify => 1;
    }
}