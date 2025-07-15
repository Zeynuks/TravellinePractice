namespace Casino.Core
{
    public record WinResult( Money Amount ) : BetResult( Amount )
    {
        public override bool IsWin => true;
    }
}