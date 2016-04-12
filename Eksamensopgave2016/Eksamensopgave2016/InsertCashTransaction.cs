/// <summary>
/// 20135332
/// Peter Viggo Printz Madsen
/// Eksamens opgave OOP F16
/// </summary>
namespace Eksamensopgave2016
{
    public class InsertCashTransaction : Transaction
    {
        public InsertCashTransaction(User client, decimal amount) : base(client)
        {
            Amount = amount;
            BalanceAfterTransaction = client.Balance + amount;
        }
        public override void Execute()
        {
            Client.Balance += Amount;
        }
        public override string ToString()
        {
            return $"Transaction Info: {base.ToString()}\nINSERT CASH of {Amount}Kr... New balance ({BalanceAfterTransaction}Kr) ";
        }
    }
}
