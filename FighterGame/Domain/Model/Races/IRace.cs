using FighterGame.Domain.Dice;
using FighterGame.Domain.Model.Types;

namespace FighterGame.Domain.Model.Races
{
    public interface IRace
    {
        public int MaxHealth { get; }
        public DamageType DamageResist { get; }
        public IDice HealDice { get; }
    }
}