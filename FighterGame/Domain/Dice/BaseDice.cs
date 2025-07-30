namespace FighterGame.Domain.Dice
{
    public abstract class BaseDice : IDice
    {
        public int Sides { get; }

        protected BaseDice( int sides )
        {
            ArgumentOutOfRangeException.ThrowIfLessThan( sides, 2 );
            Sides = sides;
        }

        public virtual int Roll()
        {
            return Random.Shared.Next( 1, Sides + 1 );
        }
    }
}