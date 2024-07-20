using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace University_Management_Project
{
    internal static class Bank
    {
        private static decimal[,] ExchangeTable = new decimal[,]
        {
            { 1, USD_EUR, USD_AMD, USD_RUB},
            { EUR_USD, 1, EUR_AMD, EUR_RUB},
            { AMD_USD, AMD_EUR, 1, AMD_RUB},
            { RUB_USD, RUB_EUR, RUB_AMD, 1}
        };

        public static Money RateExchange(Money money, Currency destinationCurrency)
        {
            return new Money(ExchangeTable[(int)money.Currency, (int)destinationCurrency] * money.Amount, destinationCurrency);
        }

        public static void ExchangeMoney(ref Money money, Currency currency)
        {
            money = RateExchange(money, currency);
        }

        private static decimal USD_EUR = 0.9028m;
        private static decimal USD_AMD = 386m;
        private static decimal USD_RUB = 86.9369m;

        private static decimal EUR_USD = 1 / USD_EUR;
        private static decimal EUR_AMD = 419m;
        private static decimal EUR_RUB = 94.3694m;

        private static decimal AMD_USD = 1 / USD_AMD;
        private static decimal AMD_EUR = 1 / EUR_AMD;
        private static decimal AMD_RUB = 0.23m;

        private static decimal RUB_USD = 1 / USD_RUB;
        private static decimal RUB_EUR = 1 / EUR_RUB;
        private static decimal RUB_AMD = 1 / AMD_RUB;
    }
}
