using FighterGame.Domain.Model;

namespace FighterGame.Domain.Repository
{
    public interface IFighterRepository
    {
        public void AddFighter( IFighter fighter );
        public IReadOnlyList<IFighter> GetAllFighters();
    }
}