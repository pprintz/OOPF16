using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2016
{
    class ProductDoesNotExistException : Exception
    {
        public ProductDoesNotExistException(int productID)
        {
            Message = $"There is no product with the ID of: {productID}";
        }
        public override string Message { get; }
    }
}
