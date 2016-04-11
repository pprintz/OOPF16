using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2016
{
    class InsufficientCreditsException : Exception
    {
        public InsufficientCreditsException(User client, Product item)
        {
            Client = client;
            Item = item;
            Message = $"{item.Name} for {item.Price}Kr";
        }
        public InsufficientCreditsException(User client, Product item, int count)
        {
            Client = client;
            Item = item;
            Count = count;
            Message = $"{count} x {item.Name} for {item.Price*count}Kr";
        }
        public override string Message { get; }
        public User Client { get; }
        public Product Item { get; }
        public int Count { get; }
    }
}
