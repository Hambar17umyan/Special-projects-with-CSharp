using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Core.Public_Models
{
    public class Message
    {
        public readonly string Title;
        public readonly string Text;

        public Message(string title, string text)
        {
            Title = title;
            Text = text;
        }

        public Message(string title)
        {
            Title = title;
            Text = "";
        }


        public static readonly Message Success;
        static Message()
        {
            Success = new Message("Success!");
        }
    }
}
