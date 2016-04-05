using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*20121234_Peter_Aage_Nielsen*/

namespace Eksamensopgave2016
{
    class Program
    {
        static void Main(string[] args)
        {
            //IStregsystem stregsystem = new Stregsystem();
            //IStregsystemUI ui = new StregsystemCLI(stregsystem);
            //StregsystemController sc = new StregsystemController(ui, stregsystem);
            User peter = new User("Peter Viggo", "Printz Madsen", "pmadse13@student.aau.dk", "pprintz");
            User nullUser = new User(null, "Printz Madsen", "-pmadse13@studentaau.dk", "pprintZ");
            Console.WriteLine($"{peter.UserID}| {peter} {peter.Username}");
            Console.WriteLine($"{nullUser.UserID}| {nullUser} {nullUser.Username}");
            peter.Balance = 20;
            Product øl = new Product("Beer", 6);
            øl.Active = true;
            InsertCashTransaction cash = new InsertCashTransaction(peter, 100);
            BuyTransaction buy = new BuyTransaction(peter, øl);
            cash.Execute();
            Console.WriteLine(cash);
            buy.Execute();
            Console.WriteLine(buy);
            Console.ReadKey();
            //ui.Start();
        }
    }
}
