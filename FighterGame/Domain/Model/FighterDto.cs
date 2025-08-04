using FighterGame.Domain.Model.Types;

namespace FighterGame.Domain.Model
{
    public class FighterDto
    {
        public string Name { get; set; }
        public ClassType Class { get; set; }
        public RaceType Race { get; set; }
        public ArmorType Armor { get; set; }
        public WeaponType Weapon { get; set; }
        public DamageType Damage { get; set; }

        public FighterDto()
        {
            Name = "Алекс";
            Class = ClassType.Knight;
            Race = RaceType.Human;
            Armor = ArmorType.PaddedArmor;
            Weapon = WeaponType.Sword;
            Damage = DamageType.Physical;
        }
    }
}