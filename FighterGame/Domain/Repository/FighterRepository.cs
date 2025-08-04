using FighterGame.Domain.Model;

namespace FighterGame.Domain.Repository
{
    public class FighterRepository : IFighterRepository
    {
        private readonly List<IFighter> _fighters = new();

        public void AddFighter( IFighter fighter )
        {
            if ( fighter == null )
            {
                throw new ArgumentNullException( nameof( fighter ), "Боец не может быть null." );
            }

            if ( _fighters.Any( f => f.Id == fighter.Id ) )
            {
                throw new InvalidOperationException( $"Боец с Id {fighter.Id} уже существует." );
            }

            _fighters.Add( fighter );
        }

        public IReadOnlyList<IFighter> GetAllFighters()
        {
            return _fighters.AsReadOnly();
        }
    }
}