using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering_Application.Application_Models
{
    internal class OrderModel
    {
        public readonly List<int> Items;
        public readonly int Code;
        public readonly int OrderID;
        public OrderModel(OrderInputModel orderInputModel, int qr, int id)
        {
            Items = orderInputModel.Items;
            Code = qr;
            OrderID = id;
        }
    }
}
