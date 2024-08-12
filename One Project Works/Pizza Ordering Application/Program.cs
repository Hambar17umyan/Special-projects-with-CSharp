using Pizza_Ordering_Application.Application_Models.Domain_Models;
using Pizza_Ordering_Application.Application_Models;

namespace Pizza_Ordering_Application
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Application application = Application.BuyAM;

            var a = application.Factories[0].Order(new OrderInputModel(5, 4));

            Thread.Sleep(6600);

            var b = application.Factories[0].TryTakingOrder(a);
            var c = application.Factories[0].TryTakingOrder(a);
        }
    }
}
