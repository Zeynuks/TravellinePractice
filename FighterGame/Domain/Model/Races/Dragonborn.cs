using FighterGame.Domain.Dice;
using FighterGame.Domain.Model.Types;

namespace FighterGame.Domain.Model.Races
{
    public class Dragonborn: IRace
    {
        public int MaxHealth => 26;
        public DamageType DamageResist => DamageType.Fire;
        public IDice HealDice => new DiceTypes.D6();
    }
}