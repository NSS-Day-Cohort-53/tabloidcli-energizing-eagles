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
        private AuthorRepository _authorRepository;
        private BlogRepository _blogRepository;
        private NoteRepository _noteRepository;
        private string _connectionString;

        // sets the previously mentioned field
        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _authorRepository = new AuthorRepository(connectionString);
            _blogRepository = new BlogRepository(connectionString);
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

        private Post Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a Post:";
            }

            Console.WriteLine(prompt);

            List<Post> posts = _postRepository.GetAll();

            for (int i = 0; i < posts.Count; i++)
            {
                Post post = posts[i];
                Console.WriteLine($" {i + 1}) {post.Title} ({post.Url})");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return posts[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private Author ChooseAuthor(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose an Author:";
            }

            Console.WriteLine(prompt);

            List<Author> authors = _authorRepository.GetAll();

            for (int i = 0; i < authors.Count; i++)
            {
                Author author = authors[i];
                Console.WriteLine($" {i + 1}) {author.FullName}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return authors[choice - 1];
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Invalid Selection. Cannot add/update author.");
                }
                return null;
            }
        }

        private Blog ChooseBlog(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a blog:";
            }

            Console.WriteLine(prompt);

            List<Blog> blogs = _blogRepository.GetAll();

            for (int i = 0; i < blogs.Count; i++)
            {
                Blog blog = blogs[i];
                Console.WriteLine($" {i + 1}) {blog.ToString()}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return blogs[choice - 1];
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Invalid Selection. Cannot add/update blog.");
                }
                return null;
            }
        }

        private void Add()

        {
            Console.WriteLine("New Post");
            Post post = new Post();

            Console.Write("Title: ");
            post.Title = Console.ReadLine();

            Console.Write("URL: ");
            post.Url = Console.ReadLine();

            bool success = false;
            DateTime publishDate;
            do
            {
                Console.Write("Publish Date (example: 1/1/2000): ");
                success = DateTime.TryParse(Console.ReadLine(), out publishDate);
            }
            while (!success);
            post.PublishDateTime = publishDate;

            Author chosenAuthor = ChooseAuthor("Who is the author?");
            while (chosenAuthor == null)
            {
                chosenAuthor = ChooseAuthor("Please choose an author.");
            }
            post.Author = chosenAuthor;

            Blog chosenBlog = ChooseBlog("What blog is this post for?");
            while (chosenBlog == null)
            {
                chosenBlog = ChooseBlog("Please choose a blog.");
            }
            post.Blog = chosenBlog;

            _postRepository.Insert(post);

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
