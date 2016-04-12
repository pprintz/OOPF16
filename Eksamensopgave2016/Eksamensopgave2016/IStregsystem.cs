using System;
using System.Collections.Generic;
/// <summary>
/// 20135332
/// Peter Viggo Printz Madsen
/// Eksamens opgave OOP F16
/// </summary>
namespace Eksamensopgave2016
{
    public interface IStregsystem
    {
        event User.UserBalanceNotification UserBalanceWarning;
        List<User> Users { get; }
        List<Transaction> Transactions { get; set; }
        Dictionary<int, Product> Products { get; }
        IEnumerable<Product> ActiveProducts { get; }
        InsertCashTransaction AddCreditsToAccount(User user, decimal amount);
        BuyTransaction BuyProduct(User user, Product product, int countOfItems);
        BuyTransaction BuyProduct(User user, Product product);
        Product GetProductByID(int productID);
        IEnumerable<Transaction> GetTransactions(User user, int count);
        User GetUser(Func<User, bool> predicate);
        User GetUserByUsername(string username);
    }
}