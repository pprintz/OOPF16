/// <summary>
/// 20135332
/// Peter Viggo Printz Madsen
/// Eksamens opgave OOP F16
/// </summary>
namespace Eksamensopgave2016
{
    public interface IStregsystemUI
    {

        void MakeSeasonItemInSeason(Product p);
        event Stregsystem.StregsystemEvent CommandEntered; 
        void DisplayCommands();
        void MakeUser();
        void ShowAllInactiveProducts();
        void Start();
        void DisplayUserNotFound(string username);
        void DisplayProductNotFound(string product);
        void DisplayUserInfo(User user);
        void DisplayUserSummary(User user);
        void DisplayTooManyArgumentsError(string command);
        void DisplayAdminCommandNotFoundMessage(string adminCommand);
        void DisplayUserGetsCredits(InsertCashTransaction transaction);
        void DisplayUserBuysProduct(BuyTransaction transaction);
        void DisplayUserBuysProduct(int count, BuyTransaction transaction);
        void Close();
        void DisplayInsufficientCash(User user, string productInfo);
        void DisplayGeneralError(string errorString);
    }
}