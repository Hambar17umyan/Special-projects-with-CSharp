using Pizza_Ordering_Application.Factory_Folder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering_Application.Application_Models.Domain_Models
{
    internal class Application
    {
        public readonly List<Factory> Factories;

        private Application(params Factory[] factories)
        {
            Factories = new List<Factory>(factories);
        }

        public readonly static Application BuyAM;
        static Application()
        {
            BuyAM = new Application(new ClassicPizzaFactory(2), new GourmetPizzaFactory(1), new SpicyPizzaFactory(3));
        }
    }
}
