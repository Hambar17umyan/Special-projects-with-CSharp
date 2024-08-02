using Pizza_Ordering_Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering_Application.Application_Models.Domain_Models
{
    internal abstract class Factory : IOrderTaker
    {

        protected Menue Menue;
        public MenueOutputModel IntroduceTheMenue()
        {
            return new MenueOutputModel(Menue);
        }

        protected int _freeWorkersCount;
        protected int _totalWorkersCount;
        protected Queue<OrderModel> _waitingOrders;
        protected List<ReadyOrderPackage> _readyOrders;
        public Factory(int workersCount)
        {
            Menue = new Menue();
            _freeWorkersCount = 0;
            _waitingOrders = new Queue<OrderModel>();
            _readyOrders = new List<ReadyOrderPackage>();

            Thread OrderDistribution = new Thread(new ThreadStart(KeepTrackOnOrders));
            OrderDistribution.Start();

            _freeWorkersCount = workersCount;
            _totalWorkersCount = workersCount;
        }

        //TODO
        public OrderCheckModel Order(OrderInputModel order)
        {
            foreach (var a in order.Items)
                if (Menue.Names.Count <= a)
                    return OrderCheckModel.GetNullCheck(new Message($"There are no items with index {a} in our factory."));

            var check = new OrderCheckModel();
            _waitingOrders.Enqueue(new OrderModel(order, QR.ScanQR(check.OrderCode), check.OrderID));
            return check;
        }

        private void KeepTrackOnOrders()
        {
            while (true)
            {
                if (_freeWorkersCount > 0)
                {
                    if (_waitingOrders.Count > 0)
                    {
                        _freeWorkersCount--;
                        Thread newOrderThread = new Thread(new ParameterizedThreadStart(MakeOrder));
                        newOrderThread.Start(_waitingOrders.Dequeue());
                    }
                }
            }
        }

        private void MakeOrder(object? orderModelAsObj)
        {
            var orderInputModel = orderModelAsObj as OrderModel;
            if (orderInputModel != null)
            {
                List<IBuyable?> list = new List<IBuyable?>();
                foreach (var a in orderInputModel.Items)
                {
                    list.Add(MakeTheItem(a));
                }
                var b = new ReadyOrderPackage(orderInputModel.Code);
                b.Items = list;
                _readyOrders.Add(b);
                Console.WriteLine(orderInputModel.OrderID.ToString() + ": Ready!");
            }
            _freeWorkersCount++;
            //TODO
            //workers, ready, waiting
        }

        private IBuyable? MakeTheItem(int index)
        {
            Thread.Sleep(Menue.MakingTimes[index]);
            var a = Menue.Names[index];
            var b = Helper.ConvertToBuyable(a);
            return b;
        }

        public (Message message, OrderPackage package) TryTakingOrder(OrderCheckModel check)
        {
            foreach (var item in _readyOrders)
            {
                if(QR.ScanQR(check.OrderCode) == item.OrderCode)
                {
                    _readyOrders.Remove(item);
                    return (Message.Success, new OrderPackage(item.Items.ToArray()));
                }
            }
            foreach (var item in _waitingOrders)
            {
                if(item.Code == QR.ScanQR(check.OrderCode))
                {
                    return (new Message("Fail!", "Your order is not ready yet."), OrderPackage.FailedPackage);
                }
            }

            return (new Message("Fail!", "There are no orders with that check code."), OrderPackage.FailedPackage);
        }
    }
}
