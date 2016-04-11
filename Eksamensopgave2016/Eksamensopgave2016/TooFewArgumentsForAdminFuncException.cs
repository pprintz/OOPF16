using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2016
{
    class TooFewArgumentsForAdminFuncException : Exception
    {
        public TooFewArgumentsForAdminFuncException(string adminFuncName)
        {
            AdminFuncName = adminFuncName;
        }

        public string AdminFuncName { get; }
    }
}
