using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2016
{
    class ProductDoesNotExistException : Exception
    {
        public readonly int ProductID;

        public ProductDoesNotExistException(int productID)
        {
            ProductID = productID;
        }
    }
}
