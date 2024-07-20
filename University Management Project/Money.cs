using System.Diagnostics.CodeAnalysis;

namespace University_Management_Project
{
    internal struct Money
    {
        public Money(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public decimal Amount
        {
            get
            {
                return Amount;
            }
            set
            {
                if (value < 0)
                {
                    throw new Exception("The amount of money cannot be negative");
                }
                else Amount = value;
            }
        }
        public Currency Currency { get; set; }

        public static bool operator <(Money a, Money b)
        {
            if (a.Currency != b.Currency)
            {
                Bank.ExchangeMoney(ref b, a.Currency);
            }
            return a.Amount < b.Amount;
        }

        public static bool operator >(Money a, Money b)
        {
            if (a.Currency != b.Currency)
            {
                Bank.ExchangeMoney(ref b, a.Currency);
            }
            return a.Amount > b.Amount;
        }

        public static bool operator ==(Money a, Money b)
        {
            if (a.Currency != b.Currency)
            {
                Bank.ExchangeMoney(ref b, a.Currency);
            }
            return a.Amount == b.Amount;
        }

        public static bool operator !=(Money a, Money b)
        {
            return !(a == b);
        }

        public static bool operator <=(Money a, Money b)
        {
            return !(a > b);
        }

        public static bool operator >=(Money a, Money b)
        {
            return !(a < b);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;
            if (obj.GetType() != typeof(Money))
                return false;

            Money m = (Money)obj;
            return m == this;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}