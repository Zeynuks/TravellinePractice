using FighterGame.Domain.Model.Types;

namespace FighterGame.Domain.Model.Class
{
    public class Knight : IClass
    {
        public List<WeaponType> WeaponSkills => [ WeaponType.Sword, WeaponType.Dagger ];
        public double DamageModify => 0.8;
    }
}