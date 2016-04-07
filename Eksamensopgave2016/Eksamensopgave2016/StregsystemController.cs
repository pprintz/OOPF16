using System;

namespace Eksamensopgave2016
{
    public class StregsystemController
    {

        public StregsystemController(IStregsystemUI ui, IStregsystem stregsystem)
        {
            UI = ui;
            Stregsystem = stregsystem;
            UI.Start(this);
        }

        public IStregsystem Stregsystem { get; set; }

        public IStregsystemUI UI { get; set; }

        public void ParseCommand(string command)
        {
            string[] commands = command.Split(' ');
            if (command == null)
            {
                throw new Exception();
            }
            else if (commands.Length == 1)
            {
            }
            else if (commands.Length == 2)
            {

            }
            else
            {
                UI.DisplayTooManyArgumentsError(command);
            }
        }
    }
}