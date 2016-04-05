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
            Message = $"Dear Mr.{client.Lastname}.. Your balance is too low: {client.Balance}!\n{item.Name}: {item.Price}";
        }

        public override string Message { get; }


    }
}
