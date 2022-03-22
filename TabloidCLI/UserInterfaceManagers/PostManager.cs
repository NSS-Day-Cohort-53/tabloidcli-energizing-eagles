using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Repositories;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    public class PostManager : IUserInterfaceManager
    {
        // private field read only  of the interface type
        private readonly IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
        private NoteRepository _noteRepository;
        private string _connectionString;

        // sets the previously mentioned field
        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _noteRepository = new NoteRepository(connectionString);
            _connectionString = connectionString;
        }
        // displays and runs menu for the posts // needs to be worked on switch empty
        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Post Menu");

            Console.WriteLine(" 1) List Posts");
            Console.WriteLine(" 2) Add Post");
            Console.WriteLine(" 3) Edit Post");
            Console.WriteLine(" 4) Remove Post");
            Console.WriteLine(" 5) Note Management");
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
                case "5":
                    NoteManagement();
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
            List<Post> posts = _postRepository.GetAll();
            foreach (Post post in posts)
            {
                Console.WriteLine($"{post.Title}: {post.Url}");
            }
        }

        private void Add()
        {
            throw new NotImplementedException();
        }

        private void Edit()
        {
            throw new NotImplementedException();
        }

        private void Remove()
        {
            throw new NotImplementedException();
        }

        private IUserInterfaceManager NoteManagement()
        {
            Console.WriteLine("Note Menu");
            Console.WriteLine("1) List Notes");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    ListNotes();
                    return this;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;



            }
            
            
                
        }
        private void ListNotes()
        {

            List<Note> notes = _noteRepository.GetAll();

            foreach (Note note in notes)
            {
                Console.WriteLine($"Title:{note.Title}  created on {note.CreateDateTime} Content: {note.Content}");
            }
        }

    }
}
