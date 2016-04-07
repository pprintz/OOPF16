using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace Eksamensopgave2016
{
    public class Stregsystem : IStregsystem
    {
        public readonly List<Transaction> Transactions = new List<Transaction>();
        private readonly Dictionary<int, Product> _products = ProductList.LoadProducts();

        public Stregsystem()
        {
            UserBalanceWarning += NotifyUserThatBalanceIsLow; 
        }

        //Method there notifies user about balance, when event is triggered 
        private void NotifyUserThatBalanceIsLow(User user, Product product)
        {
            Console.WriteLine($"Dear {user.Firstname}.. your balance is low after buying {product.Name}!\n Balance = {user.Balance+product.Price} - {product.Price} = {user.Balance}");
        }

        public List<User> Users { get; set; } = new List<User>();
        public IEnumerable<Product> ActiveProducts => _products.Values.Where(p => p.Active);

        public InsertCashTransaction AddCreditsToAccount(User user, decimal amount)
        {
            InsertCashTransaction transaction = new InsertCashTransaction(user, amount);
            ExecuteTransaction(transaction);
            return transaction;
        }

        public delegate void StregsystemEvent();
        public event User.UserBalanceNotification UserBalanceWarning;

        public BuyTransaction BuyProduct(User user, Product product)
        {
            BuyTransaction transaction = new BuyTransaction(user, product);
            ExecuteTransaction(transaction);
            if (user.Balance < 50)
            {
                UserBalanceWarning?.Invoke(user, product);
            }
            return transaction;
        }

        public void ExecuteTransaction(Transaction transaction)
        {
            Transactions.Add(transaction);
            transaction.Execute();
            File.AppendAllText(Environment.CurrentDirectory + "/Resources/TransactionLogfile.csv", Environment.NewLine + Environment.NewLine + transaction);
        }

        public Product GetProductByID(int productID)
        {
            Product item;
            bool isIDValid = _products.TryGetValue(productID, out item);
            if (!isIDValid)
            {
                throw new ProductDoesNotExistException(productID);
            }
            return item;
        }

        public IEnumerable<Transaction> GetTransactions(User user, int count)
        {
            return Transactions.Where(t => t.Client.Equals(user)).Take(10);
        }

        public User GetUser(Func<User, bool> predicate)
        {
            return Users.First(predicate);
        }
        public User GetUserByUsername(string username)
        {
            return Users.First(u => u.Username == username);
        }

    }
}