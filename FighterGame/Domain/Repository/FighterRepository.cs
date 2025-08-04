using FighterGame.Domain.Model;

namespace FighterGame.Domain.Repository
{
    public class FighterRepository : IFighterRepository
    {
        private readonly Dictionary<Guid, IFighter> _fighters = new();

        public void AddFighter( IFighter fighter )
        {
            if ( fighter == null )
            {
                throw new ArgumentNullException( nameof( fighter ), "Боец не может быть null." );
            }

            if ( !_fighters.TryAdd( fighter.Id, fighter ) )
            {
                throw new InvalidOperationException( $"Боец с Id {fighter.Id} уже существует." );
            }
        }

        public IReadOnlyList<IFighter> GetAllFighters()
        {
            return _fighters.Values.ToList().AsReadOnly();
        }
    }
}