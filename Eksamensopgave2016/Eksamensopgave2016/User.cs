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

        public static bool IsUsernameValid(string username)
        {
            if (username.All(c => char.IsLower(c) || char.IsNumber(c) || c == '_'))
            {
                return true;
            }
            return false;
        }
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
                if(IsUsernameValid(value))
                {
                    _username = value;
                }
            }
        }

        public static bool IsEmailValid(string email)
        {
            string[] substrings = email.Split('@');
            if (substrings.Length != 2)
            {
                return false;
            }
            string _localPart = substrings[0];
            string _domain = substrings[1];
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
                return true;
            return false;
        }

        private string _email;
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                if (IsEmailValid(value))
                {
                    _email = value;
                }
            }
        }
        //Makes event "LowBalance" which takes delegate "UserBalanceNotification"
        public delegate void UserBalanceNotification(User user, Product product);
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
            return UserID - ((User)obj).UserID;
        }
    }
}
