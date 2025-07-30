namespace FighterGame.Domain.Dice
{
    public class DiceTypes
    {
        public sealed class D4 : BaseDice
        {
            public D4() : base( 4 ) { }
        }

        public sealed class D6 : BaseDice
        {
            public D6() : base( 6 ) { }
        }

        public sealed class D8 : BaseDice
        {
            public D8() : base( 8 ) { }
        }

        public sealed class D10 : BaseDice
        {
            public D10() : base( 10 ) { }
        }

        public sealed class D12 : BaseDice
        {
            public D12() : base( 12 ) { }
        }

        public sealed class D20 : BaseDice
        {
            public D20() : base( 20 ) { }
        }
    }
}