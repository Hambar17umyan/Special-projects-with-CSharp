using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizza_Ordering_Application.Enums;

namespace Pizza_Ordering_Application.Application_Models
{
    internal class Menue
    {
        public List<BuyableType> Names;
        public List<TimeSpan> MakingTimes;
        public Menue()
        {
            Names = new List<BuyableType>();
            MakingTimes = new List<TimeSpan>();
        }

        public Message TryAddingItem(BuyableType name, TimeSpan timeSpan)
        {
            foreach (var item in Names)
            {
                if (name == item)
                    return new Message("Fail.", "There is already an item with that name!");
            }

            Names.Add(name);
            MakingTimes.Add(timeSpan);

            return Message.Success;
        }
        public Message TryRemovingItem(BuyableType name)
        {
            int index = Names.IndexOf(name);
            if (index == -1)
                return new Message("Fail.", $"There are no items with the name {name}.");
            Names.RemoveAt(index);
            MakingTimes.RemoveAt(index);
            return Message.Success;
        }
        public Message TryRemovingItem(int index)
        {
            if (index >= Names.Count)
                return new Message("Fail.", $"There are no items with the index {index}.");

            Names.RemoveAt(index);
            MakingTimes.RemoveAt(index);
            return Message.Success;
        }
        public Message TryChangingMakingTime(BuyableType name, TimeSpan newTimeSpan)
        {
            int index = Names.IndexOf(name);
            if (index == -1)
                return new Message("Fail.", $"There are no items with the name {name}.");

            MakingTimes[index] = newTimeSpan;
            return Message.Success;
        }
        public Message TryChangingMakingTime(int index, TimeSpan newTimeSpan)
        {
            if (index >= MakingTimes.Count)
                return new Message("Fail.", $"There are no items with the index {index}.");

            MakingTimes[index] = newTimeSpan;
            return Message.Success;
        }
    }
}
