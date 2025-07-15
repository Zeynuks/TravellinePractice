namespace Casino.Core
{
    public class Wallet
    {
        public Money Balance { get; private set; }

        public Wallet( Money initialBalance )
        {
            if ( initialBalance < Money.Zero )
                throw new Exception( "Баланс не может быть отрицательным." );

            Balance = initialBalance;
        }

        public void Debit( Money amount )
        {
            if ( amount <= Money.Zero )
                throw new Exception( "Сумма списания должна быть положительной." );

            if ( amount > Balance )
                throw new Exception( "Недостаточно средств." );

            Balance -= amount;
        }

        public void Credit( Money amount )
        {
            if ( amount < Money.Zero )
                throw new Exception( "Сумма зачисления должна быть неотрицательной." );

            try
            {
                decimal newAmount = Balance.Amount + amount.Amount;
                Balance = new Money( newAmount );
            }
            catch ( Exception )
            {
                throw new Exception( $"Баланс не может превышать {decimal.MaxValue}." );
            }
        }
    }
}