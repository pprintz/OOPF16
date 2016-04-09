using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Eksamensopgave2016
{
    public class StregsystemController
    {
        public StregsystemController(IStregsystemUI ui, IStregsystem stregsystem)
        {
            UI = ui;
            Stregsystem = stregsystem;
            LoadAdminCommands();
        }

        public delegate void AdminCommand();

        private Dictionary<string, AdminCommand> adminCommands = new Dictionary<string, AdminCommand>();

        public IStregsystem Stregsystem { get; set; }

        public IStregsystemUI UI { get; set; }

        private User CheckUsernameInput(string input)
        {
            User user = Stregsystem.GetUserByUsername(input);
            if (user == null)
            {
                UI.DisplayUserNotFound(input);
            }
            return user;
        }
        private Product CheckProductIDInput(string input)
        {
            int id;
            Product product = null;
            if (int.TryParse(input, out id))
            {
                product = Stregsystem.GetProductByID(id);
            }
            else
            {
                UI.DisplayGeneralError(input + " is not a valid productID");
            }
            return product;
        }

        private string[] inputADMCommands;
        public void ParseCommand(string command)
        {
            string[] commands = command.Split(' ');
            if (command.Length == 0)
            {
            }
            else if (commands[0].First() == ':' && commands.Length < 4)
            {
                inputADMCommands = commands;
                CheckForAdminCommands(inputADMCommands);
            }
            else if (commands.Length == 1)
            {
                User user = CheckUsernameInput(commands[0]);
                UI.DisplayUserMenu(user);
            }
            else if (commands.Length == 2)
            {
                User user = CheckUsernameInput(commands[0]);
                Product product = CheckProductIDInput(commands[1]);
                if (user != null && product != null)
                {
                    UI.DisplayUserBuysProduct(Stregsystem.BuyProduct(user, product));
                }
            }
            else
            {
                UI.DisplayTooManyArgumentsError(command);
            }
        }

        private void CheckForAdminCommands(string[] input)
        {
            AdminCommand inputCommand;
            if (adminCommands.TryGetValue(input[0], out inputCommand))
            {
                inputCommand();
            }
            else
            {
                UI.DisplayGeneralError("Not a valid admin command");
            }
        }
        private void LoadAdminCommands()
        {
            adminCommands.Add(":q", UI.Close);
            adminCommands.Add(":quit", UI.Close);
            adminCommands.Add(":activate", () => CheckProductIDInput(inputADMCommands[1]).Active = true);
            adminCommands.Add(":deactivate", () => CheckProductIDInput(inputADMCommands[1]).Active = false);
            adminCommands.Add(":crediton", () => CheckProductIDInput(inputADMCommands[1]).CanBeBoughtOnCredit = true);
            adminCommands.Add(":creditoff", () => CheckProductIDInput(inputADMCommands[1]).CanBeBoughtOnCredit = false);
            adminCommands.Add(":addcredits", () => Stregsystem.AddCreditsToAccount(
                                                     CheckUsernameInput(inputADMCommands[1])
                                                     , decimal.Parse(inputADMCommands[2])));
        }
    }
}