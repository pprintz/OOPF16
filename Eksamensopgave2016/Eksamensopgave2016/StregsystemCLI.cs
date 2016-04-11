using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.IO;
using System.Security.Authentication.ExtendedProtection;
using System.Text;

namespace Eksamensopgave2016
{
    class StregsystemCLI : IStregsystemUI
    {
        public StregsystemCLI(IStregsystem stregsystem)
        {
            _stregsystem = stregsystem;
        }
        //
        public void ParseEnteredCommandAndWriteStuff(string commandEntered, StregsystemController controller)
        {
            controller.ParseCommand(commandEntered);
        }

        //First check is if the input derives from Product, if it is then it sets end date of the season to the userinput from standard input
        public void MakeSeasonItemInSeason(Product p)
        {
            if (typeof(Product).IsSubclassOf(typeof(Product)))
            {
                SeasonalProduct seasonalProduct = p as SeasonalProduct;
                seasonalProduct.InSeason = true;
                Console.WriteLine("How many days is this item available for from now?");
                int days;
                if (int.TryParse(Console.ReadLine(), out days))
                {
                    seasonalProduct.SeasonEndDate = seasonalProduct.SeasonStartDate.AddDays(days);
                }
                else
                {
                    DisplayGeneralError("Number of days has to be a integer");
                }
            }
            else
            {
                Console.WriteLine(p + " Is not a seasonal item..");
            }
        }
        private readonly IStregsystem _stregsystem;
        // Event declaring event
        public event Stregsystem.StregsystemEvent CommandEntered;
        //Method there notifies user about balance, when event(UserBalanceWarning) is raised
        private void NotifyUserThatBalanceIsLow(User user, Product product, int count)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            if (count == 1)
            {
                Console.WriteLine($"Dear {user.Firstname}.. your balance is low after buying {product.Name}!" +
                                  $"\n Current balance = Balance {user.Balance + product.Price} - Price {product.Price} = {user.Balance}");
            }
            else
            {
                Console.WriteLine($"Dear {user.Firstname}.. your balance is low after buying {count} x {product.Name}!" +
                                  $"\n Current balance = Balance {user.Balance + product.Price * count} - Price {product.Price * count} = {user.Balance}");
            }
            Console.BackgroundColor = ConsoleColor.White;
        }

        private void markCurrentPosition()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
        }
        private void unmarkCurrentPosition()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void DisplayCommands()
        {
            Console.WriteLine("You have several options:");
            Console.WriteLine("'username'  show summary of user\n" +
                              "'username productID(integer)' - Quickbuy(executes a buy transaction, buyer = username, product = productID \n" +
                              "'username count(integer) productID(integer)' - Multibuy(this executes a number(count) of transactions) ");
        }


        //Makes satisfying user(valid email and username) and appends userinfo to Userlist.csv
        public void MakeUser()
        {
            Console.WriteLine("Do you want to make a new user?(anything/no)");
            string input = Console.ReadLine();
            if (input == "no")
            {
                return;
            }
            Console.Write("Enter first name(s): ");
            string firstName = Console.ReadLine();
            Console.Write("Enter last name(s): ");
            string lastName = Console.ReadLine();
            string email;
            do
            {
                Console.Write("Enter an valid email(eX_A-mpl3@123.coM): ");
                email = Console.ReadLine();
            } while (!User.IsEmailValid(email));
            string username;
            do
            {
                Console.Write("Enter an valid username(no special chars): ");
                username = Console.ReadLine();
            } while (!User.IsUsernameValid(username));
            User newUser = new User(firstName, lastName, email, username);
            StringBuilder sb = new StringBuilder();
            sb.Append($"{firstName},{lastName},{email},{username}{Environment.NewLine}");
            File.AppendAllText(Environment.CurrentDirectory + "/Resources/Userlist.csv", sb.ToString());
            _stregsystem.Users.Add(newUser);
        }

        //Reads users from Userlist.csv, and adds them to the user list.
        private void LoadUsers()
        {
            StreamReader userlistStreamReader = new StreamReader(Environment.CurrentDirectory + @"\Resources\Userlist.csv");
            string lineBuffer;
            while ((lineBuffer = userlistStreamReader.ReadLine()) != null)
            {
                string[] subStrings = lineBuffer.Split(',');
                User user = new User(subStrings[0], subStrings[1], subStrings[2], subStrings[3]);
                _stregsystem.Users.Add(user);

            }
            User.GlobalUserCounter = _stregsystem.Users.Max(u => u.UserID);
            userlistStreamReader.Close();
        }
        //Reads TransactionLogfile.csv and adds them to transaction list.
        private IEnumerable<Transaction> LoadTransactions()
        {
            StreamReader reader = new StreamReader(Environment.CurrentDirectory + @"\Resources\TransactionLogfile.csv");
            List<Transaction> transactions = new List<Transaction>();
            string lineBuffer;
            while ((lineBuffer = reader.ReadLine()) != null)
            {
                Transaction transaction = null;

                string[] subStrings = lineBuffer.Split(';');
                //Reads all the InsertCashTransactions
                if (subStrings[0].Contains("InsertCash"))
                {
                    string[] dateStrings = subStrings[4].Split('-');
                    transaction = new InsertCashTransaction(_stregsystem.GetUserByUsername(subStrings[1]),
                                                            decimal.Parse(subStrings[2]))
                    {
                        Date = new DateTime(int.Parse(dateStrings[2]),
                        int.Parse(dateStrings[1]),
                        int.Parse(dateStrings[0]), int.Parse(dateStrings[3]), int.Parse(dateStrings[4]), int.Parse(dateStrings[5])),
                        BalanceAfterTransaction = decimal.Parse(subStrings[5])
                    };
                }
                //Reads all the BuyTransactions
                else
                {
                    string[] dateStrings = subStrings[5].Split('-');
                    transaction = new BuyTransaction(_stregsystem.GetUserByUsername(subStrings[1]),
                                                     _stregsystem.GetProductByID(int.Parse(subStrings[2])))
                    {
                        ProductPriceAtTransaction = decimal.Parse(subStrings[3]),
                        Date = new DateTime(int.Parse(dateStrings[2]),
                        int.Parse(dateStrings[1]),
                        int.Parse(dateStrings[0]), int.Parse(dateStrings[3]), int.Parse(dateStrings[4]), int.Parse(dateStrings[5])),
                        BalanceAfterTransaction = decimal.Parse(subStrings[6])
                    };
                }
                //Adds every BuyTransaction/InsertCashTransactions to the list
                transactions.Add(transaction);
            }
            reader.Close();
            return transactions;
        }
        //Reads the list of transactions, sets the balance to latest balance
        private void SetBalanceOfUsers()
        {
            var ts = _stregsystem.Transactions.OrderByDescending(t => t.TransactionID);
            foreach (Transaction transaction in ts)
            {
                User user = _stregsystem.GetUserByUsername(transaction.Client.Username);
                if (!user.IsLoaded)
                {
                    user.Balance = transaction.BalanceAfterTransaction;
                    user.IsLoaded = true;
                }
            }
        }

        public void ShowAllInactiveProducts()
        {
            foreach (Product product in _stregsystem.Products.Values.Where(p => p.Active == false))
            {
                Console.WriteLine(product);
            }
        }
        private void ShowActiveProducts()
        {
            foreach (Product activeProduct in _stregsystem.ActiveProducts)
            {
                Console.WriteLine(activeProduct);
            }
        }

        private void LoadUsersAndTransactionsAndSetBalanceAndSubscribeToCommandEntered()
        {
            CommandEntered += ParseEnteredCommandAndWriteStuff;
            _stregsystem.UserBalanceWarning += NotifyUserThatBalanceIsLow;
            LoadUsers();
            _stregsystem.Transactions = LoadTransactions().ToList();
            SetBalanceOfUsers();
            firstTime = false;
        }
        static bool firstTime = true;
        // When start is called, users and transactions are loaded from csv file.
        // Users last known balance are found by looking at their latest transaction
        // Start calls MakeUser() if there is no users.
        // The do while loop is the CLI, it shows all the active products, and reads a line from standard input stream.
        // The 'CommandEntered' event is raised, and then runs the function "ParseEnteredCommand.." which is subscribed.
        private bool _running = true;
        public void Start(StregsystemController controller)
        {
            if (firstTime)
            {
                LoadUsersAndTransactionsAndSetBalanceAndSubscribeToCommandEntered();
            }
            if (_stregsystem.Users.Count == 0)
            {
                Console.WriteLine("There is no users in the database.. Please make an user!\n\n");
                MakeUser();
            }
            do
            {
                unmarkCurrentPosition();
                Console.Clear();
                Console.Title = "F-Klubbens Stregsystem";
                Console.WriteLine("Et bud på CLI baseret stregsystem til F-klubben\naf Peter Madsen\n");
                ShowActiveProducts();
                markCurrentPosition();
                string command = Console.ReadLine();
                if (CommandEntered != null) CommandEntered.Invoke(command, controller);
                Console.ReadKey();
            } while (_running);
        }
        public void DisplayUserNotFound(string username)
        {
            Console.WriteLine($"User '{username}' not found!");
        }
        public void DisplayProductNotFound(string productID)
        {
            Console.WriteLine($"No product with '{productID}' was found");
        }
        public void DisplayUserInfo(User user)
        {
            Console.WriteLine($"Username: {user.Username}{Environment.NewLine}" +
                              $"Name: {user.Firstname} {user.Lastname}{Environment.NewLine}" +
                              $"Balance: {user.Balance}Kr");
        }
        public void DisplayUserSummary(User user)
        {
            unmarkCurrentPosition();
            Console.Clear();
            DisplayUserInfo(user);
            Console.WriteLine("\nList of transactions, latest first:");
            foreach (Transaction transaction in _stregsystem.GetTransactions(user, 10))
            {
                Console.WriteLine(transaction);
            }
            if (user.Balance < 50)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine($"Balance is low: {user.Balance}Kr left!!");
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        public void DisplayTooManyArgumentsError(string command)
        {
            Console.WriteLine("Too many arguments... Try with one(Username) or two(Username productID)");
        }
        public void DisplayAdminCommandNotFoundMessage(string adminCommand)
        {
            Console.WriteLine($"The command '{adminCommand}' is not a valid admin command");
        }
        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            Console.WriteLine($"{transaction.Client} buys {transaction.Item.Name} for {transaction.Item.Price}Kr");
        }
        public void DisplayUserBuysProduct(int count, BuyTransaction transaction)
        {
            Console.WriteLine($"{transaction.Client} buys {count} x {transaction.Item.Name} for {transaction.Item.Price * count}Kr");
        }
        public void Close()
        {
            Console.WriteLine("Closing program...");
            Console.ReadKey();
            Environment.Exit(0);
        }
        public void DisplayInsufficientCash(User user, string productInfo)
        {
            Console.WriteLine($"Insufficient cash to buy {productInfo}.. Current balance:{user.Balance}");
        }
        public void DisplayGeneralError(string errorString)
        {
            Console.WriteLine($"**ERROR** {errorString} **ERROR**");
        }
    }
}