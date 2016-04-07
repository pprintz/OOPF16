using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2016
{
    public class InsertCashTransaction : Transaction
    {
        public InsertCashTransaction(User client, decimal amount) : base(client)
        {
            Amount = amount;
        }
        public override void Execute()
        {
            Client.Balance += Amount;
        }
        public override string ToString()
        {
            return $"Transaction Info: {base.ToString()}\nINSERT CASH of {Amount}Kr... New balance ({Client.Balance}Kr) ";
        }
    }
}
