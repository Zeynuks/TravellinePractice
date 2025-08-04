using FighterGame.Domain.Model;

namespace FighterGame.Domain.Repository
{
    public class FighterRepository : IFighterRepository
    {
        private readonly List<IFighter> _fighters = new();

        public void AddFighter( IFighter fighter )
        {
            _fighters.Add( fighter );
        }

        public List<IFighter> GetAllFighters()
        {
            return _fighters;
        }
    }
}