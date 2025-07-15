namespace Casino.Core
{
    public abstract record BetResult( Money Amount )
    {
        public abstract bool IsWin { get; }
    }
    
    public record LoseResult( Money Amount ) : BetResult( Amount )
    {
        public override bool IsWin => false;
    }
    
    public record WinResult( Money Amount ) : BetResult( Amount )
    {
        public override bool IsWin => true;
    }
}