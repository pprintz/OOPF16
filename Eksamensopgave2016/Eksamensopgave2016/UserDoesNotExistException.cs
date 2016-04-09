using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2016
{
    class UserDoesNotExistException : Exception
    {
        public UserDoesNotExistException(string username)
        {
        }
    }
}
