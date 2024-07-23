using System.Data.SqlTypes;

namespace University_Management_Project
{
    internal partial class University
    {
        private struct Budget
        {
            public Money[] Money { get; set; }

            public Budget()
            {
                Money = new Money[4];
                for(int i = 0; i < Money.Length; i++)
                {
                    Money[i] = new Money(0, (Currency)i);
                }
            }

            public Budget(BudgetInputModel budget):this()
            {
                Money[0] = budget.Money[0];
                Money[1] = budget.Money[1];
                Money[2] = budget.Money[2];
                Money[3] = budget.Money[3];
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
}