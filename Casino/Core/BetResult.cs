namespace Casino.Core
{
    public abstract record BetResult( Money Amount )
    {
        public abstract bool IsWin { get; }
    }
}