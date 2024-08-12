using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizza_Ordering_Application.Application_Models.Domain_Models;
using Pizza_Ordering_Application.Enums;
using Pizza_Ordering_Application.Pizza_Folder;

namespace Pizza_Ordering_Application.Factory_Folder
{
    internal class ClassicPizzaFactory : Factory
    {
        public ClassicPizzaFactory(int workersCount) : base(workersCount)
        {
            Menue.TryAddingItem(BuyableType.Margherita, new TimeSpan(0, 0, 5));
            Menue.TryAddingItem(BuyableType.Pepperoni, new TimeSpan(0, 0, 4));
            Menue.TryAddingItem(BuyableType.Hawaiian, new TimeSpan(0, 0, 3));
            Menue.TryAddingItem(BuyableType.Supreme, new TimeSpan(0, 0, 2));
            Menue.TryAddingItem(BuyableType.MeatLovers, new TimeSpan(0, 0, 1));
            Menue.TryAddingItem(BuyableType.Veggie, new TimeSpan(0, 0, 0));

        }
    }
}
