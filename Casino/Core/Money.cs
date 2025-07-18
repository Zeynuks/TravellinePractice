using System.Globalization;

namespace Casino.Core
{
    public readonly struct Money : IParsable<Money>
    {
        public decimal Amount { get; }

        public Money( decimal amount )
        {
            if ( amount < 0m )
            {
                throw new Exception( "Значение не может быть отрицательным." );
            }

            Amount = amount;
        }

        public static Money operator +( Money a, Money b )
            => new( a.Amount + b.Amount );

        public static Money operator -( Money a, Money b )
        {
            if ( a.Amount < b.Amount )
            {
                throw new Exception( "Недостаточно средств." );
            }

            return new Money( a.Amount - b.Amount );
        }

        public static Money operator *( Money a, int multiplier )
        {
            if ( multiplier < 0 )
            {
                throw new Exception( "Множитель не может быть отрицательным." );
            }

            return new Money( a.Amount * multiplier );
        }

        public static bool operator >( Money a, Money b ) => a.Amount > b.Amount;
        public static bool operator <( Money a, Money b ) => a.Amount < b.Amount;
        public static bool operator >=( Money a, Money b ) => a.Amount >= b.Amount;
        public static bool operator <=( Money a, Money b ) => a.Amount <= b.Amount;

        public override string ToString() => Amount.ToString( "0.##" );

        public static Money Parse( string s, IFormatProvider? provider )
        {
            if ( decimal.TryParse( s, NumberStyles.Currency, provider ?? CultureInfo.InvariantCulture,
                    out decimal result ) )
            {
                return new Money( result );
            }

            throw new Exception( "Неверный формат для Money." );
        }

        public static bool TryParse( string? s, IFormatProvider? provider, out Money result )
        {
            result = default;

            if ( string.IsNullOrEmpty( s ) )
            {
                return false;
            }

            if ( !decimal.TryParse( s, NumberStyles.Currency, provider ?? CultureInfo.InvariantCulture,
                    out decimal amount ) )
            {
                return false;
            }

            result = new Money( amount );

            return true;
        }
    }
}