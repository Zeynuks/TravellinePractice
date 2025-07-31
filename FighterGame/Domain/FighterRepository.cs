namespace FighterGame.Domain
{
    public class FighterRepository
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

        public List<IFighter> GetFighters( IEnumerable<Guid> fighterIds )
        {
            return _fighters.Where(f => fighterIds.Contains(f.Id))
                .ToList();
        }
    }
}