using FighterGame.Domain.Model.Armor;
using FighterGame.Domain.Model.Types;
using FighterGame.Domain.Model.Weapons;

namespace FighterGame.Domain.Model.Class
{
    public interface IClass
    {
        List<WeaponType> WeaponSkills { get; }
        double DamageModify { get; }
    }
}