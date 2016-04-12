using System;
/// <summary>
/// 20135332
/// Peter Viggo Printz Madsen
/// Eksamens opgave OOP F16
/// </summary>
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
