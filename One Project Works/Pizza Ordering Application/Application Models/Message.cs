using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering_Application.Application_Models
{
    internal class Message
    {
        public static readonly Message Success;

        public readonly string Title;
        public readonly string? Describtion;

        public Message(string title, string describtion)
        {
            Title = title;
            Describtion = describtion;
        }

        public Message(string title)
        {
            Title = title;
        }

        static Message()
        {
            Success = new Message("Success");
        }
    }
}
