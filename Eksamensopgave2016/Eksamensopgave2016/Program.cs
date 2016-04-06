using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
/*20121234_Peter_Aage_Nielsen*/

namespace Eksamensopgave2016
{
    class Program
    {
        static void Main(string[] args)
        {
            //IStregsystemUI ui = new StregsystemCLI(stregsystem);
            //StregsystemController sc = new StregsystemController(ui, stregsystem);
            IStregsystem stregsystem = new Stregsystem();
            User peter = new User("Peter Viggo", "Printz Madsen", "pmadse13@student.aau.dk", "pprintz");
            User nullUser = new User(null, "Printz Madsen", "-pmadse13@studentaau.dk", "pprintZ");
            Console.WriteLine($"{peter.UserID}| {peter} {peter.Username}");
            Console.WriteLine($"{nullUser.UserID}| {nullUser} {nullUser.Username}");
            peter.Balance = 20;
            ProductList katalog = new ProductList();
            var list = katalog.LoadProducts();
            foreach (Product product in list)
            {
                Console.WriteLine(product);
            }
            Console.ReadKey();
            //ui.Start();
        }
    }
}
