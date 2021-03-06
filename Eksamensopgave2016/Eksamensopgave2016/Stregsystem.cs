﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/// <summary>
/// 20135332
/// Peter Viggo Printz Madsen
/// Eksamens opgave OOP F16
/// </summary>
namespace Eksamensopgave2016
{
    public class Stregsystem : IStregsystem
    {
        public delegate void StregsystemEvent(string commandEntered);

        public event User.UserBalanceNotification UserBalanceWarning;
        public List<User> Users { get; set; } = new List<User>();
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
        public Dictionary<int, Product> Products { get; set; } = ProductList.LoadAAUProducts();
        public IEnumerable<Product> ActiveProducts => Products.Values.Where(p => p.Active);


        //Makes and executes a InsertCastTransaction
        public InsertCashTransaction AddCreditsToAccount(User user, decimal amount)
        {
            InsertCashTransaction transaction = new InsertCashTransaction(user, amount);
            ExecuteTransaction(transaction);
            return transaction;
        }
        //BuyProduct: Makes transaction(s) based on user and product(count).
        //Executes transaction with "ExecuteTransaction" and raises the event
        // if balance is under 50 after transaction.
        //(EventName?.Invoke(input) would raise event aswell, but is less readable code. Returns the transaction.
        public BuyTransaction BuyProduct(User user, Product product)
        {
            BuyTransaction transaction = new BuyTransaction(user, product);
            ExecuteTransaction(transaction);
            if (user.Balance < 50)
            {
                if (UserBalanceWarning != null) UserBalanceWarning(user, product, 1);
            }
            return transaction;
        }
        public BuyTransaction BuyProduct(User user, Product product, int count)
        {
            BuyTransaction transaction = null;
            if (user.Balance >= product.Price * count || product.CanBeBoughtOnCredit)
            {
                for (int index = 0; index < count; index++)
                {
                    transaction = new BuyTransaction(user, product);
                    ExecuteTransaction(transaction);
                }
                if (user.Balance < 50)
                {
                    if (UserBalanceWarning != null) UserBalanceWarning(user, product, count);
                }
                return transaction;
            }
            throw new InsufficientCreditsException(user, product, count);
        }
        //Executes the transaction, which executes different depending on transaction type. Appends the transaction info in TransactionLogfile.csv
        public void ExecuteTransaction(Transaction transaction)
        {
            Transactions.Add(transaction);
            transaction.Execute();
            StringBuilder sb = new StringBuilder();
            if (transaction is BuyTransaction)
            {
                BuyTransaction bt = transaction as BuyTransaction;
                sb.Append($"{bt.GetType()};{bt.Client.Username};{bt.Item.ProductID}" +
                          $";{bt.ProductPriceAtTransaction};{bt.TransactionID}" +
                          $";{bt.Date.Day}-{bt.Date.Month}-{bt.Date.Year}-{bt.Date.Hour}-{bt.Date.Minute}-{bt.Date.Second}" +
                          $";{bt.BalanceAfterTransaction}");
            }
            else
            {
                InsertCashTransaction it = transaction as InsertCashTransaction;
                sb.Append($"{it.GetType()};{it.Client.Username};{it.Amount}" +
                          $";{it.TransactionID}" +
                          $";{it.Date.Day}-{it.Date.Month}-{it.Date.Year}-{it.Date.Hour}-{it.Date.Minute}-{it.Date.Second}" +
                          $";{it.BalanceAfterTransaction}");
            }
            File.AppendAllText(Environment.CurrentDirectory + "/Resources/TransactionLogfile.csv", sb + Environment.NewLine);
        }
        //Throws exception if the product does not exist - TryGetValue return false
        public Product GetProductByID(int productID)
        {
            Product item;
            if (!Products.TryGetValue(productID, out item))
            {
                throw new ProductDoesNotExistException(productID);
            }
            return item;
        }
        //Sorts transaction list - Latest first, takes 10 of the latest transactions
        public IEnumerable<Transaction> GetTransactions(User user, int count)
        {
            return Transactions.OrderByDescending(t => t.TransactionID).Where(t => t.Client.UserID == user.UserID).Take(10);
        }
        public User GetUser(Func<User, bool> predicate)
        {
            return Users.First(predicate);
        }
        public User GetUserByUsername(string username)
        {
            if (!User.IsUsernameValid(username))
            {
                return null;
            }
            if (Users.Exists(u => u.Username == username))
            {
                return Users.First(u => u.Username == username);
            }
            throw new UserDoesNotExistException(username);
        }
        //Reads users from Userlist.csv, and adds them to the user list.
        private void LoadUsers()
        {
            StreamReader userlistStreamReader = new StreamReader(Environment.CurrentDirectory + @"\Resources\Userlist.csv");
            string lineBuffer;
            while ((lineBuffer = userlistStreamReader.ReadLine()) != null)
            {
                string[] subStrings = lineBuffer.Split(',');
                User user = new User(subStrings[0], subStrings[1], subStrings[2], subStrings[3]);
                Users.Add(user);

            }
            if (Users.Count > 0)
            {
                User.GlobalUserCounter = Users.Max(u => u.UserID);
            }
            userlistStreamReader.Close();
        }
        //Reads TransactionLogfile.csv and adds them to transaction list.
        private IEnumerable<Transaction> LoadTransactions()
        {
            StreamReader reader = new StreamReader(Environment.CurrentDirectory + @"\Resources\TransactionLogfile.csv");
            List<Transaction> transactions = new List<Transaction>();
            string lineBuffer;
            while ((lineBuffer = reader.ReadLine()) != null)
            {
                Transaction transaction = null;

                string[] subStrings = lineBuffer.Split(';');
                //Reads all the InsertCashTransactions
                if (subStrings[0].Contains("InsertCash"))
                {
                    string[] dateStrings = subStrings[4].Split('-');
                    transaction = new InsertCashTransaction(GetUserByUsername(subStrings[1]),
                                                            decimal.Parse(subStrings[2]))
                    {
                        Date = new DateTime(int.Parse(dateStrings[2]),
                        int.Parse(dateStrings[1]),
                        int.Parse(dateStrings[0]), int.Parse(dateStrings[3]), int.Parse(dateStrings[4]), int.Parse(dateStrings[5])),
                        BalanceAfterTransaction = decimal.Parse(subStrings[5])
                    };
                }
                //Reads all the BuyTransactions
                else
                {
                    string[] dateStrings = subStrings[5].Split('-');
                    transaction = new BuyTransaction(GetUserByUsername(subStrings[1]),
                                                     GetProductByID(int.Parse(subStrings[2])))
                    {
                        ProductPriceAtTransaction = decimal.Parse(subStrings[3]),
                        Date = new DateTime(int.Parse(dateStrings[2]),
                        int.Parse(dateStrings[1]),
                        int.Parse(dateStrings[0]), int.Parse(dateStrings[3]), int.Parse(dateStrings[4]), int.Parse(dateStrings[5])),
                        BalanceAfterTransaction = decimal.Parse(subStrings[6])
                    };
                }
                //Adds every BuyTransaction/InsertCashTransactions to the list
                transactions.Add(transaction);
            }
            reader.Close();
            return transactions;
        }
        //Reads the list of transactions, sets the balance to latest balance
        private void SetBalanceOfUsers()
        {
            var ts = Transactions.OrderByDescending(t => t.TransactionID);
            foreach (Transaction transaction in ts)
            {
                User user = GetUserByUsername(transaction.Client.Username);
                if (!user.IsLoaded)
                {
                    user.Balance = transaction.BalanceAfterTransaction;
                    user.IsLoaded = true;
                }
            }
        }
       public void LoadUsersAndTransactionsAndSetBalanceAndSubscribeToCommandEntered()
        {
            LoadUsers();
            //Wont load transactions if there is no users, loading transactions dos
            if (Users.Count != 0)
            {
                Transactions = LoadTransactions().ToList();
            }
            SetBalanceOfUsers();
        }
    }
}