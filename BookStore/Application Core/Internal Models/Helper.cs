using Application_Core.Public_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application_Core.Internal_Models
{
    public static class Helper
    {
        private static HashSet<int> _identificators;
        static Helper()
        {
            _identificators = new HashSet<int>();
        }
        public static int GenerateUniqueIdentificator(int digitNumber)
        {
            Random random = new Random();
            while (true)
            {
                int n = random.Next((int)Math.Pow(10, digitNumber - 1), (int)Math.Pow(10, digitNumber) - 1);
                int sz = _identificators.Count;
                _identificators.Add(n);
                if (sz < _identificators.Count)
                {
                    return n;
                }
            }
        }

        //TODO..
        public static bool CheckIfThereAreSufficientBooks(int isbn, uint count)
        {
            string item = File.ReadAllText(@$"..\..\..\..\{isbn}.txt");
            var desItem = JsonSerializer.Deserialize<BookModel>(item);
            return desItem.StockQuantity >= count;
        }
    }
}
