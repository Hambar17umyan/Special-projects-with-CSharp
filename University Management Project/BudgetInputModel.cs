using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Management_Project
{
    internal struct BudgetInputModel
    {
        public Money[] Money { get; set; }

        public BudgetInputModel(params Money[] money)
        {
            Money = new Money[4];
            for (int i = 0; i < Money.Length; i++)
            {
                Money[i] = new Money(0, (Currency)i);
            }

            foreach (var item in money)
            {
                Money[(int)item.Currency].Amount += item.Amount;
            }
        }

        public BudgetInputModel()
        {
            Money = new Money[4];
            for (int i = 0; i < Money.Length; i++)
            {
                Money[i] = new Money(0, (Currency)i);
            }
        }
    }
}
