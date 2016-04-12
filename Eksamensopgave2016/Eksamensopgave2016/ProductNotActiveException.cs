using System;
/// <summary>
/// 20135332
/// Peter Viggo Printz Madsen
/// Eksamens opgave OOP F16
/// </summary>
namespace Eksamensopgave2016
{
    public class ProductNotActiveException : Exception
    {
        public ProductNotActiveException(Product item)
        {
            Message = $"{item.Name} is not active";
        }
        public override string Message { get; }
    }
}
