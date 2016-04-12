using System;
/// <summary>
/// 20135332
/// Peter Viggo Printz Madsen
/// Eksamens opgave OOP F16
/// </summary>
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
