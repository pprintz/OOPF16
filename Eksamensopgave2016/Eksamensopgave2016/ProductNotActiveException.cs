using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2016
{
    class ProductNotActiveException : Exception
    {
        public ProductNotActiveException(Product item)
        {
            Message = $"{item.Name} is not active";
        }
        public override string Message { get; }
    }
}
