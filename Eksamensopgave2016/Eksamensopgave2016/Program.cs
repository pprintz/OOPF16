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
            Console.WriteLine(peter + " " + peter.UserId + "   " + peter.Username);
            Console.WriteLine(nullUser + " " + nullUser.UserId + "   " + nullUser.Username);
            peter.Balance = 20;
            Console.ReadKey();
            //ui.Start();
        }
    }
}
