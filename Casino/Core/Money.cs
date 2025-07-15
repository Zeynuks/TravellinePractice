namespace Casino.Core
{
    public readonly struct Money
    {
        public decimal Amount { get; }
        public static Money Zero => new(0m);

        public Money(decimal amount)
        {
            if (amount < 0m)
                throw new Exception("Сумма не может быть отрицательной.");
            Amount = amount;
        }

        public static Money operator +(Money a, Money b)
            => new(a.Amount + b.Amount);

        public static Money operator -(Money a, Money b)
        {
            if (a.Amount < b.Amount)
                throw new Exception("Недостаточно средств.");
            return new Money(a.Amount - b.Amount);
        }

        public static Money operator *(Money a, int multiplier)
            => new(a.Amount * multiplier);

        public static bool operator >(Money a, Money b) => a.Amount > b.Amount;
        public static bool operator <(Money a, Money b) => a.Amount < b.Amount;
        public static bool operator >=( Money a, Money b ) => a.Amount >= b.Amount;
        public static bool operator <=( Money a, Money b ) => a.Amount <= b.Amount;

        public override string ToString() => Amount.ToString("0.##");
    }
}