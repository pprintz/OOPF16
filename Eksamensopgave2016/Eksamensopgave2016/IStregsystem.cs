using System;
using System.Collections.Generic;

namespace Eksamensopgave2016
{
    public interface IStregsystem
    {
        List<User> Users { get; set; }
        Dictionary<int, Product> Products { get; }
        IEnumerable<Product> ActiveProducts { get; }
        InsertCashTransaction AddCreditsToAccount(User user, decimal amount);
        BuyTransaction BuyProduct(User user, Product product);
        Product GetProductByID(int productID);
        IEnumerable<Transaction> GetTransactions(User user, int count);
        User GetUser(Func<User, bool> predicate);
        User GetUserByUsername(string username);
        event User.UserBalanceNotification UserBalanceWarning;
    }
}