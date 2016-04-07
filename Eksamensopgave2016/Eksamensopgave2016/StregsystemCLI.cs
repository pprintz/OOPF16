using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.IO;
using System.Text;

namespace Eksamensopgave2016
{
    class StregsystemCLI : IStregsystemUI
    {
        public StregsystemCLI(Stregsystem stregsystem)
        {
            this.stregsystem = stregsystem;
        }
        private readonly IStregsystem stregsystem = new Stregsystem();
        public event Stregsystem.StregsystemEvent CommandEntered;

        private void drawUI()
        {
            foreach (Product activeProduct in stregsystem.ActiveProducts)
            {
                Console.WriteLine(activeProduct);
            }
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

        public void MakeUser()
        {
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
            File.AppendAllText(@"C: \Users\peter\Desktop\OOPF16\Eksamensopgave2016\Eksamensopgave2016\bin\Debug\Resources\Userlist.csv", sb.ToString());
            stregsystem.Users.Add(newUser);
        }

        private bool _running = true;
        public void Start(StregsystemController controller)
        {
            StreamReader userlistStreamReader = new StreamReader(@"C: \Users\peter\Desktop\OOPF16\Eksamensopgave2016\Eksamensopgave2016\bin\Debug\Resources\Userlist.csv");
            while (userlistStreamReader.ReadLine() != null)
            {
                string line = userlistStreamReader.ReadLine();
                string[] subStrings = line?.Split(',');
                if (subStrings != null)
                {
                    User user = new User(subStrings[0], subStrings[1], subStrings[2], subStrings[3]);
                    stregsystem.Users.Add(user);
                }
            }
            userlistStreamReader.Close();
            if (stregsystem.Users.Count == 0)
            {
                Console.WriteLine("There is no users in the database.. Please make an user!\n\n");
                MakeUser();
            }
            do
            {
                unmarkCurrentPosition();
                drawUI();
                markCurrentPosition();
                controller.ParseCommand(Console.ReadLine());
            } while (_running);
        }
        public StregsystemCLI(IStregsystem stregsystem)
        {
            this.stregsystem = stregsystem;
        }
        public void DisplayUserNotFound(string username)
        {
            Console.WriteLine($"User '{username}' not found!");
        }
        public void DisplayProductNotFound(string product)
        {
            Console.WriteLine($"The product '{product}' not found");
        }
        public void DisplayUserInfo(User user)
        {
            Console.WriteLine(user);
        }
        public void DisplayTooManyArgumentsError(string command)
        {
            Console.WriteLine(@"Too many arguments..");
        }
        public void DisplayAdminCommandNotFoundMessage(string adminCommand)
        {
            Console.WriteLine($"The command '{adminCommand}' is not a valid admin command");
        }
        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            Console.WriteLine($"{transaction.Client} buys {transaction.Item} for {transaction.Item.Price}");
        }
        public void DisplayUserBuysProduct(int count, BuyTransaction transaction)
        {
        }
        public void Close()
        {
            _running = false;
        }
        public void DisplayInsufficientCash(User user, Product product)
        {
            Console.WriteLine($"Insufficient cash to buy {product.Name} for {product.Price}.. Current balance:{user.Balance}");
        }
        public void DisplayGeneralError(string errorString)
        {
            Console.WriteLine(@"**ERROR**");
        }

    }
}