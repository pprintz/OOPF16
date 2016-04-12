using System;
/// <summary>
/// 20135332
/// Peter Viggo Printz Madsen
/// Eksamens opgave OOP F16
/// </summary>
namespace Eksamensopgave2016
{
    public class InsufficientCreditsException : Exception
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
