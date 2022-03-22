using System;
using System.Collections.Generic;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    public class TagManager : IUserInterfaceManager
    {
        // private field read only  of the interface type
        private readonly IUserInterfaceManager _parentUI;
        private TagRepository _tagRepository;
        private string _connectionString;


        // sets the previously mentioned field
        public TagManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _tagRepository = new TagRepository(connectionString);
            _connectionString = connectionString;
        }
        // displays and runs menu for the tags // needs to be worked on switch empty
        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Tag Menu");
            Console.WriteLine(" 1) List Tags");
            Console.WriteLine(" 2) Add Tag");
            Console.WriteLine(" 3) Edit Tag");
            Console.WriteLine(" 4) Remove Tag");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    Add();
                    return this;
                case "3":
                    Edit();
                    return this;
                case "4":
                    Remove();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }
        // needs to be worked on
       
        private void List()
        {
            //need to list all the tags
            // had to make a list of all the tags
            List<Tag> tags = _tagRepository.GetAll();
            //display all the tags using a foreach loop.
            foreach (Tag tag in tags)
            {
                Console.WriteLine(tag);
            }


        }
        private Tag Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose an Author:";
            }
            Console.WriteLine(prompt);
            List<Tag> tags = _tagRepository.GetAll();

            for (int i = 0; i < tags.Count; i++)
            {
                Tag tag = tags[i];
                Console.WriteLine($" {i + 1}) {tag.Name}");
            }

            Console.Write("> ");
            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return tags[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }

        }

        private void Add()
        {
            Console.WriteLine("Insert new tag name");
            Tag tag = new Tag();
            tag.Name = Console.ReadLine();
            _tagRepository.Insert(tag);


        }

        private void Edit()
        {
            Tag tagToEdit = Choose("Which tag would you like to edit?");
            if (tagToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("Changed name or leave it blank to not make any changes: ");
            string Name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(Name))
            {
                tagToEdit.Name = Name;
            }

            _tagRepository.Update(tagToEdit);
        }
      
        private void Remove()
        {
            Tag tagToDelete = Choose("Which tag would you like to remove?");
            if (tagToDelete != null)
            {
                _tagRepository.Delete(tagToDelete.Id);
            }
            Console.WriteLine("Tag successfully deleted");
        }
    }
}
