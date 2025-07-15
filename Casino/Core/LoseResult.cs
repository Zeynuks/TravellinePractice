namespace Casino.Core
{
    public record LoseResult( Money Amount ) : BetResult( Amount )
    {
        public override bool IsWin => false;
    }
}