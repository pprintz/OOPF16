using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2016
{
    public class User : IComparable
    {
        public static int GlobalUserCounter;
        public User(string firstname, string lastname, string email, string username)
        {
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
            Username = username;
            GlobalUserCounter++;
            LowBalance += NotifyUserAboutLowBalance;
        }
        //Method there notifies user about balance, when event has happened
        private void NotifyUserAboutLowBalance(User user, decimal balance)
        {
            Console.WriteLine($"Dear {user.Username}\nYour balance is under 50!! ({balance})");
        }

        public int UserID { get; } = GlobalUserCounter;

        private string _firstname;
        public string Firstname
        {
            get
            {
                return _firstname;
            }
            set
            {
                _firstname = value;
                if (value == null)
                {
                    _firstname = "\"Intet navn\"";
                }
            }
        }

        public string Lastname { get; }

        private string _username;
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = "no username";
                if (value.All(c => char.IsLower(c) || char.IsNumber(c) || c == '_'))
                {
                    _username = value;
                }
            }
        }

        private string _email, _localPart, _domain;
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                string[] substrings = value.Split('@');
                if (substrings.Length != 2)
                {
                    _email = null;
                }
                else {
                    _email = null;
                    _localPart = substrings[0];
                    _domain = substrings[1];
                    if (
                    // Most important checks, to short circuit if the value is invalid according to special chars
                    _domain.Contains(".") &&
                    _domain.Last() != '.' &&
                    _domain.Last() != '-' &&
                    _domain.First() != '.' &&
                    _domain.First() != '-' &&
                    _localPart.Last() != '.' &&
                    _localPart.Last() != '-' &&
                    _localPart.First() != '.' &&
                    _localPart.First() != '-' &&

                    _localPart.All(c =>
                    char.IsLetterOrDigit(c) ||
                    c == '.' ||
                    c == '-' ||
                    c == '_') &&
                    _domain.All(c =>
                    char.IsLetterOrDigit(c) ||
                    c == '.' ||
                    c == '-'))
                    {
                        _email = value;
                    }
                }
            }
        }
        //Makes event "LowBalance" which takes delegate "UserBalanceNotification"
        public delegate void UserBalanceNotification(User user, decimal balance);
        public event UserBalanceNotification LowBalance;
        private decimal _balance;

        public decimal Balance
        {
            get
            {
                return _balance;
            }
            set
            {
                _balance = value;
                if (Balance < 50)
                {
                    //Triggers the event, and checks if event is null
                    LowBalance?.Invoke(this, Balance);
                }
            }
        }
        public override string ToString()
        {
            return $"{Firstname} {Lastname} ({Email})";
        }
        public override int GetHashCode()
        {
            return UserID;
        }
        public override bool Equals(object obj)
        {
            return GetHashCode() == obj.GetHashCode();
        }
        public int CompareTo(object obj)
        {
            if (GetHashCode() > obj.GetHashCode())
            {
                return -1;
            }
            else if (GetHashCode() < obj.GetHashCode())
            {
                return 1;
            }
            else return 0;
        }
    }
}
