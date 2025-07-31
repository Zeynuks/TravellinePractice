namespace FighterGame.Domain.Dice
{
    public interface IDice
    {
        int Sides { get; }
        int Roll();
    }
}