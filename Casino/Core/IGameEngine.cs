namespace Casino.Core
{
    public interface IGameEngine
    {
        BetResult PlayRound( Money bet, int mult );
    }
}