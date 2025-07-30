using FighterGame.Domain.Model.Races;
using FighterGame.Domain.Model.Types;

namespace FighterGame.Domain.Factory
{
    public class RaceFactory
    {
        public IRace CreateRace( RaceType raceType )
        {
            return raceType switch
            {
                RaceType.Aasimar => new Aasimar(),
                RaceType.Dragonborn => new Dragonborn(),
                RaceType.Human => new Human(),
                RaceType.Tiefling => new Tiefling(),
                _ => throw new ArgumentOutOfRangeException( nameof( raceType ) )
            };
        }
    }
}