using System;
using System.IO;
using System.Collections.Generic;

namespace Eksamensopgave2016
{
    class Stregsystem : IStregsystem
    {
        List<Transaction> Transactions = new List<Transaction>(); 
        public IEnumerable<Product> ActiveProducts { get; }
        public InsertCashTransaction AddCreditsToAccount(User user, decimal amount)
        {
            InsertCashTransaction transaction = new InsertCashTransaction(user, amount);
            return transaction;
        }

        public BuyTransaction BuyProduct(User user, Product product)
        {
            BuyTransaction transaction = new BuyTransaction(user, product);
            return transaction;
        }

        public void ExecuteTransaction(Transaction transaction)
        {
            Transactions.Add(transaction);
            transaction.Execute();
            File.AppendAllText(Environment.CurrentDirectory + @"\TransactionLogfile.txt", "\n" + transaction);
        }

        public Product GetProductByID(int productID)
        {
            return null;
        }

        public IEnumerable<Transaction> GetTransactions(User user, int count)
        {
            return null;
        }

        public User GetUser(Func<User, bool> predicate)
        {
            return null;
        }

        public User GetUserByUsername(string username)
        {
            return null;
        }

        public event User.UserBalanceNotification UserBalanceWarning;
    }
}