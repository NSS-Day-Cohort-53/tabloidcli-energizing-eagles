using TabloidCLI.UserInterfaceManagers;
using System;
using System.Collections.Generic;

namespace TabloidCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Welcome user!! We are so happy you are here!");
            Console.WriteLine("--------------------------------------------");

            // MainMenuManager implements the IUserInterfaceManager interface
            IUserInterfaceManager ui = new MainMenuManager();
            while (ui != null)
            {
                // Each call to Execute will return the next IUserInterfaceManager we should execute
                // When it returns null, we should exit the program;
                ui = ui.Execute();
            }
        }
    }
}
