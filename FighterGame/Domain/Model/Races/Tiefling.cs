using FighterGame.Domain.Dice;
using FighterGame.Domain.Model.Types;

namespace FighterGame.Domain.Model.Races
{
    public class Tiefling: IRace
    {
        public int MaxHealth => 18;
        public DamageType DamageResist => DamageType.Necrotic;
        public IDice HealDice => new DiceTypes.D6();
    }
}