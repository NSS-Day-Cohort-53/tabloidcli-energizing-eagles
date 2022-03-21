﻿using System;

namespace TabloidCLI.UserInterfaceManagers
{
    public class MainMenuManager : IUserInterfaceManager
    {
        private const string CONNECTION_STRING = 
            @"Data Source=localhost\SQLEXPRESS;Database=TabloidCLI;Integrated Security=True";

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Main Menu");

            Console.WriteLine(" 1) Journal Management");
            Console.WriteLine(" 2) Blog Management");
            Console.WriteLine(" 3) Author Management");
            Console.WriteLine(" 4) Post Management");
            Console.WriteLine(" 5) Tag Management");
            Console.WriteLine(" 6) Search by Tag");
            Console.WriteLine(" 0) Exit");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                //Journal Management
                case "1": return new JournalManager(this, CONNECTION_STRING);
                //case 2 doenst work or display any new options
                //Blog Management
                case "2": throw new NotImplementedException();
                    // author manager
                case "3": return new AuthorManager(this, CONNECTION_STRING);
                //case 4 doesnt work!
                //Post Management
                case "4": throw new NotImplementedException();
                    // tag manager
                case "5": return new TagManager(this, CONNECTION_STRING);
                    // search manager
                case "6": return new SearchManager(this, CONNECTION_STRING);
                case "0":
                    Console.WriteLine("Good bye");
                    return null;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }
    }
}
