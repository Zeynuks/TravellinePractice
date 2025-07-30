using FighterGame.Domain.Model.Types;

namespace FighterGame.Domain.Model.Class
{
    public interface IClass
    {
        List<WeaponType> WeaponSkills { get; }
        double DamageModify { get; }
    }
}