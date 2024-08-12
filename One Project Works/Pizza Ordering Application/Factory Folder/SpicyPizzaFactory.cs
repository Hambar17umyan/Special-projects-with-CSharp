using Pizza_Ordering_Application.Application_Models.Domain_Models;
using Pizza_Ordering_Application.Enums;

namespace Pizza_Ordering_Application.Factory_Folder
{
    internal class SpicyPizzaFactory : Factory
    {
        public SpicyPizzaFactory(int workersCount) : base(workersCount)
        {
            Menue.TryAddingItem(BuyableType.BBQChicken, new TimeSpan(0, 0, 5));
            Menue.TryAddingItem(BuyableType.BuffaloChicken, new TimeSpan(0, 0, 4));
            Menue.TryAddingItem(BuyableType.Pepperoni, new TimeSpan(0, 0, 3));
            Menue.TryAddingItem(BuyableType.Supreme, new TimeSpan(0, 0, 2));
            Menue.TryAddingItem(BuyableType.SpinachAndFeta, new TimeSpan(0, 0, 1));
            Menue.TryAddingItem(BuyableType.MeatLovers, new TimeSpan(0, 0, 0));
        }
    }
}