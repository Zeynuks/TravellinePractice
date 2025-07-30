using FighterGame.Domain.Dice;
using FighterGame.Domain.Model.Types;

namespace FighterGame.Domain.Model.Races
{
    public class Human : IRace
    {
        public int MaxHealth => 20;
        public DamageType DamageResist => DamageType.Physical;
        public IDice HealDice => new DiceTypes.D4();
    }
}