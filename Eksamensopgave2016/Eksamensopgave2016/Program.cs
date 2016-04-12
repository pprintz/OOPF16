using System;
using System.IO;
/// <summary>
/// 20135332
/// Peter Viggo Printz Madsen
/// Eksamens opgave OOP F16
/// </summary>
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
                    ui.Start();
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
                    ui.DisplayInsufficientCash(insufficientCreditsException.Client,
                        insufficientCreditsException.Message);
                }
                catch (ProductNotActiveException productNotActiveException)
                {
                    ui.DisplayGeneralError(productNotActiveException.Message);

                }
                catch (TooFewArgumentsForAdminFuncException tooFewArgumentsForAdminFuncException)
                {
                    ui.DisplayGeneralError("Not enough arguments for  " + "'" +
                                           tooFewArgumentsForAdminFuncException.AdminFuncName + "'");
                }
                catch (FileNotFoundException fileNotFoundException)
                {
                    using (StreamWriter w = File.AppendText(fileNotFoundException.FileName)) ;
                }
                finally
                {
                    Console.ReadKey();
                } while (true);
        }
    }
}
