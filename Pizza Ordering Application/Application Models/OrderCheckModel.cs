using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering_Application.Application_Models
{
    internal class OrderCheckModel
    {
        public QR OrderCode { get; private set; }
        public Message Message { get; private set; }
        public int OrderID { get; private set; }
        public OrderCheckModel()
        {
            OrderCode = new QR();
            Message = Message.Success;
            _count++;
            OrderID = _count;
        }

        private OrderCheckModel(OrderCheckModel orderCheckModel)
        {
            OrderCode = new QR(orderCheckModel.OrderCode);
            Message = orderCheckModel.Message;
            OrderID = orderCheckModel.OrderID;
            _count++;
        }


        private static readonly OrderCheckModel _failedCheck;
        private static int _count;
        static OrderCheckModel()
        {
            _count = 0;
            _failedCheck = new OrderCheckModel();
            _failedCheck.OrderCode = QR.NullCode;
            _failedCheck.OrderID = 0;
        }

        public static OrderCheckModel GetNullCheck(Message message)
        {
            var a = new OrderCheckModel(_failedCheck);
            a.Message = message;
            return a;
        }

    }
}
