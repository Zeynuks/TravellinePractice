using FighterGame.Domain.Model.Types;

namespace FighterGame.Domain.Model
{
    public class FighterDto
    {
        public string Name { get; set; } = "Алекс";
        public ClassType Class { get; set; } = ClassType.Knight;
        public RaceType Race { get; set; } = RaceType.Human;
        public ArmorType Armor { get; set; } = ArmorType.PaddedArmor;
        public WeaponType Weapon { get; set; } = WeaponType.Sword;
        public DamageType Damage { get; set; } = DamageType.Physical;
    }
}