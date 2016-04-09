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
        public List<User> Users { get; set; } = new List<User>();
        public Dictionary<int, Product> Products { get; } = ProductList.LoadAAUProducts();
        public IEnumerable<Product> ActiveProducts => Products.Values.Where(p => p.Active);
        public delegate void StregsystemEvent(string commandEntered, StregsystemController controller);
        public event User.UserBalanceNotification UserBalanceWarning;

        public Stregsystem()
        {
            UserBalanceWarning += NotifyUserThatBalanceIsLow;
        }

        //Method there notifies user about balance, when event is triggered 
        private void NotifyUserThatBalanceIsLow(User user, Product product)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine($"Dear {user.Firstname}.. your balance is low after buying {product.Name}!\n Current balance = Balance{user.Balance + product.Price} - Price{product.Price} = {user.Balance}");
            Console.BackgroundColor = ConsoleColor.White;
        }

        public InsertCashTransaction AddCreditsToAccount(User user, decimal amount)
        {
            InsertCashTransaction transaction = new InsertCashTransaction(user, amount);
            ExecuteTransaction(transaction);
            return transaction;
        }

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
            bool isIDValid = Products.TryGetValue(productID, out item);
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
            if (Users.Exists(u => u.Username == username))
                return Users.First(u => u.Username == username);
            return null;
        }

    }
}