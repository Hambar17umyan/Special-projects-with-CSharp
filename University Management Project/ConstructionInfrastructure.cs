using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static University_Management_Project.University;

namespace University_Management_Project
{
    internal static class ConstructionInfrastructureAdmin
    {
        public static readonly Money ServiceMoney;
        public static readonly Money OneSeatMoney;

        static ConstructionInfrastructureAdmin()
        {
            ServiceMoney = new Money(5000, Currency.USD);
            OneSeatMoney = new Money(100, Currency.USD);
        }

        public static int CountAffordableSeats(BudgetInputModel budget)
        {
            Money money = new Money(0, ServiceMoney.Currency);
            for (int i = 0; i < budget.Money.Length; i++)
            {
                money += budget.Money[i];
            }


            if (money.Amount - ServiceMoney.Amount >= 0)
            {
                money -= ServiceMoney;
                Money oneSeatMoney = OneSeatMoney;
                Bank.ExchangeMoney(ref oneSeatMoney, money.Currency);
                return (int)(money.Amount / oneSeatMoney.Amount);
            }
            else return 0;
        }

    }
}