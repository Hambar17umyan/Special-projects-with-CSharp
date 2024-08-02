using Pizza_Ordering_Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering_Application.Application_Models
{
    internal class OrderInputModel
    {
        public readonly List<int> Items; // The ID-s of items

        public OrderInputModel(params int[] items)
        {
            Items = new List<int>(items);
        }
    }
}
