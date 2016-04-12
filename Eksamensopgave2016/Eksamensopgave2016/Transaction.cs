using System;
/// <summary>
/// 20135332
/// Peter Viggo Printz Madsen
/// Eksamens opgave OOP F16
/// </summary>
namespace Eksamensopgave2016
{
    public abstract class Transaction
    {
        public static int GlobalTransactionCounter = 1;
        protected Transaction(User client)
        {
            Client = client;
            GlobalTransactionCounter++;
            Date = DateTime.Now;
        }


        public int TransactionID { get; set; } = GlobalTransactionCounter;
        public User Client { get;}
        public DateTime Date { get; set; }
        public decimal BalanceAfterTransaction { get; set; }
        public decimal Amount { get; set; }
        public override string ToString()
        {
            return $"{TransactionID} {Client} (DATE:{Date})";
        }
        public abstract void Execute();
    }
}
