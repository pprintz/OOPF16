using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls.WebParts;

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
            do
                try
                {
                    ui.Start(sc);
                }
                catch (UserDoesNotExistException userException)
                {
                    ui.DisplayUserNotFound(userException.Username);
                }
                catch (ProductDoesNotExistException productException)
                {
                    ui.DisplayProductNotFound("" + productException.ProductID);
                }
                catch (InsufficientCreditsException insufficientCreditsException)
                {
                    ui.DisplayInsufficientCash(insufficientCreditsException.Client, insufficientCreditsException.Message);
                }
                catch (ProductNotActiveException productNotActiveException)
                {
                    ui.DisplayGeneralError(productNotActiveException.Message);

                }
                catch (TooFewArgumentsForAdminFuncException tooFewArgumentsForAdminFuncException)
                {
                    ui.DisplayGeneralError("Not enough arguments for : " + tooFewArgumentsForAdminFuncException.AdminFuncName);
                }
                finally
                {
                    Console.ReadKey();
                } while (true);
        }
    }
}
