using System;
/// <summary>
/// 20135332
/// Peter Viggo Printz Madsen
/// Eksamens opgave OOP F16
/// </summary>
namespace Eksamensopgave2016
{
    public class UserDoesNotExistException : Exception
    {
        public UserDoesNotExistException(string username)
        {
            Username = username;
        }
        public string Username { get; }
    }
}
