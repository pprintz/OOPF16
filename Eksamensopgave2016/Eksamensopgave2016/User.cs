using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2016
{
    public class User : IComparable
    {
        public static int GlobalUserId;
        public User(string firstname, string lastname, string email, string username)
        {
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
            Username = username;
            GlobalUserId++;
            BalanceChanged += notifyUser;
        }
        //Method there notifies user about balance, when event has happened
        private void notifyUser(User user, decimal balance)
        {
            Console.WriteLine($"Dear {user.Username}\nYour balance is under 50!! ({balance})");
        }

        public int UserId { get; } = GlobalUserId;
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
                Console.WriteLine(value);
                if (substrings.Length != 2)
                {
                    _email = null;
                }
                else {
                    _email = "not valid";
                    _localPart = substrings[0];
                    _domain = substrings[1];
                    if (
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
                    char.IsLetter(c) ||
                    char.IsNumber(c) ||
                    c == '.' ||
                    c == '-' ||
                    c == '_') &&

                    _domain.All(c =>
                    char.IsNumber(c) ||
                    char.IsLetter(c) ||
                    c == '.' ||
                    c == '-'))
                    {
                        _email = value;
                    }
                }
            }
        }
        //Makes event "BalanceChanged" which takes delegate "UserBalanceNotification"
        public delegate void UserBalanceNotification(User user, decimal balance);
        public event UserBalanceNotification BalanceChanged;
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
                    BalanceChanged?.Invoke(this, Balance);
                }
            }
        }
        public override string ToString()
        {
            return $"{Firstname} {Lastname} ({Email})";
        }
        public override int GetHashCode()
        {
            return UserId;
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
