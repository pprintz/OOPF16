using System;
using System.Linq;
/// <summary>
/// 20135332
/// Peter Viggo Printz Madsen
/// Eksamens opgave OOP F16
/// </summary>
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

        public bool IsLoaded { get; set; }
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
                    _firstname = "..";
                }
            }
        }
        private string _lastname;
        public string Lastname
        {
            get
            {
                return _lastname;
            }
            set
            {
                _lastname = value;
                if (value == null)
                {
                    _lastname = "..";
                }
            }
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
                if (IsUsernameValid(value))
                {
                    _username = value;
                }
            }
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

        public static bool IsUsernameValid(string username)
        {
            if (username.All(c => char.IsLower(c) || char.IsNumber(c) || c == '_'))
            {
                return true;
            }
            return false;
        }
        public static bool IsEmailValid(string email)
        {
            string[] substrings = email.Split('@');
            if (substrings.Length != 2)
            {
                return false;
            }
            string localPart = substrings[0];
            string domain = substrings[1];
            if (
                // Most important checks, to short circuit if the value is invalid according to special chars
                domain.Contains(".") &&
                domain.Last() != '.' &&
                domain.Last() != '-' &&
                domain.First() != '.' &&
                domain.First() != '-' &&
                localPart.Last() != '.' &&
                localPart.Last() != '-' &&
                localPart.First() != '.' &&
                localPart.First() != '-' &&

                localPart.All(c =>
                    char.IsLetterOrDigit(c) ||
                    c == '.' ||
                    c == '-' ||
                    c == '_') &&
                domain.All(c =>
                    char.IsLetterOrDigit(c) ||
                    c == '.' ||
                    c == '-'))
                return true;
            return false;
        }

        //Delegate "UserBalanceNotification"
        public delegate void UserBalanceNotification(User user, Product product, int count);
        public decimal Balance { get; set; }

        public override string ToString()
        {
            return $"{Firstname} {Lastname} ({Email})";
        }

        //Taken inspiration in Josh Bloch's - Effective Java
        //Multiplying two primenumbers is more likely to generate a unique hashcode 
        public override int GetHashCode()
        {
            int hash = 7;
            int primeNumber = 17;
            hash = hash*primeNumber + Firstname.GetHashCode();
            hash = hash*primeNumber + Lastname.GetHashCode();
            hash = hash*primeNumber + Username.GetHashCode();
            return hash;
        }

        public override bool Equals(object obj)
        {
            User user = obj as User;
            if (user == null)
            {
                return false;
            }
            return GetHashCode() == user.GetHashCode();
        }
        public int CompareTo(object obj)
        {
            return UserID - ((User)obj).UserID;
        }
    }
}
