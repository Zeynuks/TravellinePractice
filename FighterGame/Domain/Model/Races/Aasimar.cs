using FighterGame.Domain.Dice;
using FighterGame.Domain.Model.Types;

namespace FighterGame.Domain.Model.Races
{
    public class Aasimar : IRace
    {
        public int MaxHealth => 16;
        public DamageType DamageResist => DamageType.Radiant;
        public IDice HealDice => new DiceTypes.D8();
    }
}