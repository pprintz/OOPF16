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
            IStregsystem stregsystem = new Stregsystem();
            IStregsystemUI ui = new StregsystemCLI(stregsystem);
            StregsystemController sc = new StregsystemController(ui, stregsystem);
            try
            {
                ui.Start(sc);
                Console.ReadKey();
            }
            catch (UserDoesNotExistException)
            {

                ui.MakeUser();
            }
        }
    }
}
