using System.Data.SqlTypes;

namespace University_Management_Project
{
    internal struct Budget
    {
        public Money[] Money { get; set; }

        public Budget()
        {
            Money = new Money[4];
        }

        public static Budget operator -(Budget budget, Money money)
        {
            if (budget.Money[(int)money.Currency] < money)
                throw new ArgumentException("The money on your budget is less then you want to extract.");

            var res = new Budget();
            res.Money = budget.Money;
            res.Money[(int)money.Currency].Amount = budget.Money[(int)money.Currency].Amount - money.Amount;
            return res;
        }

        public static Budget operator +(Budget budget, Money money)
        {
            var res = new Budget();
            res.Money = budget.Money;
            res.Money[(int)money.Currency].Amount = budget.Money[(int)money.Currency].Amount + money.Amount;
            return res;
        }
    }
}