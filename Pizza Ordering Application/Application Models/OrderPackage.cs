using Pizza_Ordering_Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering_Application.Application_Models
{
    internal class OrderPackage
    {
        public readonly List<IBuyable?> Items;
        public OrderPackage(params IBuyable?[] items)
        {
            Items = new List<IBuyable?>(items);
        }



        public static OrderPackage FailedPackage 
        {
            get
            {
                return new OrderPackage();
            }
        }

    }
}
