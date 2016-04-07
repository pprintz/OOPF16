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
            User peter = new User("Peter Viggo", "Printz Madsen", "pmadse13@student.aau.dk", "pprintz");
            User nullUser = new User(null, "Printz Madsen", "-pmadse13@studentaau.dk", "pprintZ");
            Console.WriteLine($"{peter.UserID}| {peter} {peter.Username}");
            Console.WriteLine($"{nullUser.UserID}| {nullUser} {nullUser.Username}");
            IStregsystem stregsystem = new Stregsystem();
            IStregsystemUI ui = new StregsystemCLI(stregsystem);
            StregsystemController sc = new StregsystemController(ui, stregsystem);
            Console.ReadKey();
            //ui.Start();
        }
    }
}
