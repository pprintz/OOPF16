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
        // Dictionary with the different adminCommands, takes a string as key ":activate" examp, and takes a delegate as value, with form of an action.
        private readonly Dictionary<string, AdminFunction> _adminCommands = new Dictionary<string, AdminFunction>();
        public delegate void AdminFunction();
        public IStregsystem Stregsystem { get; set; }
        public IStregsystemUI UI { get; set; }

        // user is null if the input username is not valid(User.IsUsernameValid) IStregsystem.GetUserByUsername
        private User CheckUsernameInput(string input)
        {
            User user = Stregsystem.GetUserByUsername(input);
            if (user == null)
            {
                UI.DisplayGeneralError($"'{input}' is not a valid username, NO SPECIAL CHARS!");
            }
            return user;
        }
        //Tries to convert the string to an int, if it fails it updates the UI with a general error message
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
                UI.DisplayGeneralError($"'{input}' is not a valid productID");
            }
            return product;
        }

        private string[] _inputAdmCommands;
        //Method there select the correct method according to the input string
        public void ParseCommand(string command)
        {
            string[] commands = command.Split(' ');
            if (command.Length == 0)
            {
                UI.DisplayGeneralError("Please type a command. type ':help' for help");
            }
            //Every admin command starts with ':' checks the dictionary if it contains a key == first substring (":activate" etc.)
            else if (commands[0].First() == ':' && commands.Length < 4)
            {
                _inputAdmCommands = commands;
                CheckForAdminCommands(_inputAdmCommands);
            }
            //If one word is entered, checks the word if its an existing user, then updates the UI with user summary
            else if (commands.Length == 1)
            {
                User user = CheckUsernameInput(commands[0]);
                if (user != null)
                {
                    UI.DisplayUserSummary(user);
                }
            }
            //Two words triggers a quickbuy, executes and updates UI with user(first string) buys product(second string) if the input is valid
            else if (commands.Length == 2)
            {
                User user = CheckUsernameInput(commands[0]);
                Product product = CheckProductIDInput(commands[1]);
                if ((user != null) && (product != null))
                {
                    UI.DisplayUserBuysProduct(Stregsystem.BuyProduct(user, product));
                }
            }
            //Three words triggers a multibuy, if the input is valid, then it executes and displays the purchase.
            else if (commands.Length == 3)
            {
                User user = CheckUsernameInput(commands[0]);
                Product product = CheckProductIDInput(commands[2]);
                int countOfItems;
                if (int.TryParse(commands[1], out countOfItems) && (user != null) && (product != null))
                {
                    if (countOfItems > 0)
                    {
                        if (countOfItems > 30)
                        {
                            UI.DisplayGeneralError("Please dont try with more than 30 items");
                        }
                        else
                        {
                            UI.DisplayUserBuysProduct(countOfItems, Stregsystem.BuyProduct(user, product, countOfItems));
                        }
                    }
                    else
                    {
                        UI.DisplayGeneralError($"Count can't be negative: {countOfItems} ");
                    }
                }

            }
            else
            {
                UI.DisplayTooManyArgumentsError(command);
            }
        }

        //Checks if input corresponds to a valid admincommand.
        //catches IndexOutOfrangeException if user dont gives the right amount of arguments to the functions
        //Throws it again as Customized exception. Gets handled in main.
        private void CheckForAdminCommands(string[] input)
        {
            AdminFunction inputFunction;
            if (_adminCommands.TryGetValue(input[0], out inputFunction))
            {
                try
                {
                    inputFunction();
                }
                catch (IndexOutOfRangeException)
                {
                    throw new TooFewArgumentsForAdminFuncException(input[0]);
                }
            }
            else
            {
                UI.DisplayGeneralError("Not a valid admin command");
            }
        }

        private void LoadAdminCommands()
        {
            _adminCommands.Add(":q", UI.Close);
            _adminCommands.Add(":quit", UI.Close);
            _adminCommands.Add(":activate", () =>
            {
                CheckProductIDInput(_inputAdmCommands[1]).Active = true;
            });

            _adminCommands.Add(":deactivate", () => CheckProductIDInput(_inputAdmCommands[1]).Active = false);
            _adminCommands.Add(":crediton", () => CheckProductIDInput(_inputAdmCommands[1]).CanBeBoughtOnCredit = true);
            _adminCommands.Add(":creditoff", () => CheckProductIDInput(_inputAdmCommands[1]).CanBeBoughtOnCredit = false);
            _adminCommands.Add(":addcredits", () => Stregsystem.AddCreditsToAccount(
                                                     CheckUsernameInput(_inputAdmCommands[1])
                                                     , decimal.Parse(_inputAdmCommands[2])));
            _adminCommands.Add(":makeuser", UI.MakeUser);
            _adminCommands.Add(":help", UI.DisplayCommands);
            _adminCommands.Add(":show", UI.ShowAllInactiveProducts);
            _adminCommands.Add(":inseason", () => UI.MakeSeasonItemInSeason(CheckProductIDInput(_inputAdmCommands[1])));
        }
    }
}