using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2016
{
    class InsertCashTransaction : Transaction
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
            return  $"INSERT CASH of {Amount}Kr... New balance ({Client.Balance}Kr)\nTransaction Info: {base.ToString()}";
        }
    }
}
