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

        private decimal amount;

        public decimal Amount
        {
            get
            {
                return amount;
            }
            set
            {
                if (value < 0)
                {
                    throw new Exception("The amount of money cannot be negative");
                }
                else amount = value;
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

        public static Money operator +(Money a, Money b)
        {
            if (a.Currency != b.Currency)
            {
                Bank.ExchangeMoney(ref b, a.Currency);
                a.Amount += b.Amount;
                return a;
            }
            else
            {
                a.Amount += b.Amount;
                return a;
            }
        }

        public static Money operator -(Money a, Money b)
        {
            if (a.Currency != b.Currency)
            {
                Bank.ExchangeMoney(ref b, a.Currency);
                a.Amount -= b.Amount;
                return a;
            }
            else
            {
                a.Amount -= b.Amount;
                return a;
            }
        }

        public static Money operator *(Money a, int b)
        {
            a.Amount *= b;
            return a;
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