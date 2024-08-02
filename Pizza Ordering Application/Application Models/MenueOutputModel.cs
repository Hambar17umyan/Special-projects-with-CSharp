using Pizza_Ordering_Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering_Application.Application_Models
{
    internal class MenueOutputModel
    {
        public readonly List<BuyableType> Names;
        public readonly List<TimeSpan> MakingTimes;

        public MenueOutputModel(Menue menue)
        {
            Names = new List<BuyableType>(menue.Names);
            MakingTimes = new List<TimeSpan>(menue.MakingTimes);
        }
    }
}
