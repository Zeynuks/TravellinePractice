namespace FighterGame.Domain
{
    public class FighterRepository
    {
        private readonly Dictionary<Guid, IFighter> _fighters = new();

        public void AddFighter( IFighter fighter )
        {
            Guid id = Guid.NewGuid();
            _fighters.Add( id, fighter );
        }

        public Dictionary<Guid, IFighter> GetAllFighters()
        {
            return _fighters;
        }

        public List<IFighter> GetFighters( IEnumerable<Guid> fighterIds )
        {
            List<IFighter> result = [ ];

            foreach ( Guid id in fighterIds )
            {
                if ( _fighters.TryGetValue( id, out IFighter? fighter ) )
                {
                    result.Add( fighter );
                }
            }

            return result;
        }
    }
}