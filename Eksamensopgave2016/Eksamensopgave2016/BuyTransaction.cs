namespace Eksamensopgave2016
{
    public class BuyTransaction : Transaction
    {
        public Product Item { get; set; }
        public decimal ProductPriceAtTransaction { get; set; }
        public BuyTransaction(User client, Product item) : base(client)
        {
            Item = item;
            ProductPriceAtTransaction = item.Price;
            Amount = item.Price;
            BalanceAfterTransaction = client.Balance - ProductPriceAtTransaction;
        }


        public override string ToString()
        {
            return $"Transaction Info: {base.ToString()}" +
                   $"\nPURCHASE of: {Item.Name} at {ProductPriceAtTransaction}Kr... Balance after transaction: {BalanceAfterTransaction}Kr";
        }
        public override void Execute()
        {
            if (!Item.Active)
            {
                throw new ProductNotActiveException(Item);
            }
            if (Client.Balance <= Amount && !Item.CanBeBoughtOnCredit)
            {
                throw new InsufficientCreditsException(Client, Item);
            }
            Client.Balance -= Amount;
        }
    }
}
