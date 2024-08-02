using Pizza_Ordering_Application.Application_Models.Domain_Models;
using Pizza_Ordering_Application.Enums;

namespace Pizza_Ordering_Application.Factory_Folder
{
    internal class GourmetPizzaFactory : Factory
    {
        public GourmetPizzaFactory(int workersCount) : base(workersCount)
        {
            Menue.TryAddingItem(BuyableType.Margherita, new TimeSpan(0, 0, 5));
            Menue.TryAddingItem(BuyableType.FourCheese, new TimeSpan(0, 0, 4));
            Menue.TryAddingItem(BuyableType.Mediterranean, new TimeSpan(0, 0, 3));
            Menue.TryAddingItem(BuyableType.PestoDelight, new TimeSpan(0, 0, 2));
            Menue.TryAddingItem(BuyableType.SpinachAndFeta, new TimeSpan(0, 0, 1));
            Menue.TryAddingItem(BuyableType.MushroomMadness, new TimeSpan(0, 0, 0));
            Menue.TryAddingItem(BuyableType.Veggie, new TimeSpan(0, 0, 0));

        }
    }
}