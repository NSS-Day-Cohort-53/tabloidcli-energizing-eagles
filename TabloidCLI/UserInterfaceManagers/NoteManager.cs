using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Repositories;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    public class NoteManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
        private AuthorRepository _authorRepository;
        private BlogRepository _blogRepository;
        private NoteRepository _noteRepository;
        private string _connectionString;

        public NoteManager(IUserInterfaceManager parentUI, string connectionString, int postId)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _authorRepository = new AuthorRepository(connectionString);
            _blogRepository = new BlogRepository(connectionString);
            _noteRepository = new NoteRepository(postId, connectionString);
            _connectionString = connectionString;
        }
        //public NoteRepository(string connectionString) : base(connectionString) { }
          public IUserInterfaceManager Execute()
        {
            Console.WriteLine("---------");
            Console.WriteLine("Note Menu");
            Console.WriteLine("---------");
            Console.WriteLine("1) List Notes");
            Console.WriteLine("2) Add Note");
            Console.WriteLine("3) Delete note ");
            Console.WriteLine("0) Go back ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ListNotes();
                    return this;
                case "2":
                    AddNotes();
                    return this;
                case "3":
                    DeleteNote();
                    return this;
                case "0":
                    return _parentUI;
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

        private void AddNotes()
        {
            Console.WriteLine("Which post you like to put a note on!");
            List<Post> posts = _postRepository.GetAll();
            foreach (Post post in posts)
            {
                Console.WriteLine($"{post.Id}) {post.Title}");
            }
            int PostId = int.Parse(Console.ReadLine());


            Console.WriteLine("Add a note to a post!");
            Note n = new Note();

            Console.Write("Title: ");
            n.Title = Console.ReadLine();
            n.CreateDateTime = DateTime.Now;
            Console.Write("Content: ");
            n.Content = Console.ReadLine();
            n.PostId = PostId;


            _noteRepository.Insert(n);

        }

        private void DeleteNote()
        {
            Console.WriteLine("Which post you like to a remove your note on!");
            List<Post> posts = _postRepository.GetAll();
            foreach (Post post in posts)
            {
                Console.WriteLine($"{post.Id}) {post.Title}");
            }
            int PostId = int.Parse(Console.ReadLine());

            List<Note> notes = _noteRepository.GetAll();

            foreach (Note note in notes)
            {
                Console.WriteLine($"Id:{note.Id} Title:{note.Title} created on {note.CreateDateTime} Content: {note.Content}");
            }
            int noteId = int.Parse(Console.ReadLine());
            _noteRepository.Delete(noteId);
            Console.WriteLine("Note deleted!");
        }

    }

}

