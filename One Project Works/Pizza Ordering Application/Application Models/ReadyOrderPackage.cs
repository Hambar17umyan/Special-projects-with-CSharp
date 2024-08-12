using Pizza_Ordering_Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering_Application.Application_Models
{
    internal class ReadyOrderPackage
    {
        public int OrderCode;
        public List<IBuyable?> Items;
        public ReadyOrderPackage(int orderCode)
        {
            Items = new List<IBuyable?>();
            OrderCode = orderCode;
        }
    }
}
