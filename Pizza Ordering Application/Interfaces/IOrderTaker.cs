using Pizza_Ordering_Application.Application_Models;
using Pizza_Ordering_Application.Application_Models.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering_Application.Interfaces
{
    internal interface IOrderTaker
    {
        OrderCheckModel Order(OrderInputModel order);
        (Message message, OrderPackage package) TryTakingOrder(OrderCheckModel check);

    }
}
