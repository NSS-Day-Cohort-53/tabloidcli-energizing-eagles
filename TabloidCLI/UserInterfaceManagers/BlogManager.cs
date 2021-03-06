using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Repositories;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    public class BlogManager: IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private BlogRepository _blogRepository;
        private TagRepository _tagRepository;
        private string _connectionString;
        public BlogManager(IUserInterfaceManager parentUI, string connectionString) 
        {
            _parentUI = parentUI;
            _blogRepository = new BlogRepository(connectionString);
            _connectionString = connectionString;
            _tagRepository = new TagRepository(connectionString);
        }


        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("---------");
            Console.WriteLine("Blog Menu");
            Console.WriteLine("---------");
            Console.WriteLine(" 1) Add Blog to faves");
            Console.WriteLine(" 2) View Blogs");
            Console.WriteLine(" 3) Remove Blog");
            Console.WriteLine(" 4) Edit Blog");
            Console.WriteLine(" 5) Add tag to blog");
            Console.WriteLine(" 6) Remove tag from blog");
            Console.WriteLine(" 7) View blog's posts");
            Console.WriteLine(" 0) Go Back");

            int menuOp = int.Parse(Console.ReadLine());

            switch (menuOp)
            {
                case 1:
                    AddToDB();
                    break;
                case 2:
                    Show();
                    break;
                case 3:
                    DeleteById();
                    break;
                case 4:
                    Edit();
                    break;
                case 5:
                    AddTag();
                    break;
                case 6:
                    RemoveTagFromBlog();
                    break;
                case 0:
                    return _parentUI;
            }

            return this;
        }

        private void AddToDB()
        {
            Blog blog = new Blog();

            Console.WriteLine("What is the blog's title? ");
           blog.Title = Console.ReadLine();

            Console.WriteLine("whats the url? ");
            blog.Url = Console.ReadLine();

            _blogRepository.insert(blog);
        }

        private void Show()
        {
           foreach(Blog blog in _blogRepository.GetAll())
            {
                Console.WriteLine($"{blog.Id}: {blog.Title}");
            }
            Console.Write("Select a Blog Id that you would like to see: ");
            int selectedBlogId = int.Parse(Console.ReadLine());
            Blog selectedBlog = _blogRepository.GetById(selectedBlogId);
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine($"{selectedBlog.Id}) {selectedBlog.Title} || URL:{selectedBlog.Url}");
            if (selectedBlog.Tags.Count > 0)
            {
                Console.Write("Tags: ");
                foreach (Tag tag in selectedBlog.Tags)
                {
                    Console.WriteLine(" " + tag.Name);
                }
            }
            else
            {
                Console.WriteLine("This blog has no tags yet!");
            }
            Console.WriteLine("------------------------------------------------------------------");
        }

        private void DeleteById()
        {
            Show();
            Console.WriteLine($"Which Blog do you want to delete ");
            int blogChoice = int.Parse(Console.ReadLine());
            _blogRepository.Delete(blogChoice);
        }

        private Blog Choose(string prompt = null)
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
                Console.WriteLine($" {i + 1}) {blog.Title}");
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
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private void Edit()
        {
            /*Show();
            Console.WriteLine("Which blog would you like to edit?");
            int blogChoice = int.Parse(Console.ReadLine()); */


            Blog blogToEdit = Choose("Which blog would you like to edit?");
            if (blogToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New Title (blank to leave unchanged: ");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
            {
                blogToEdit.Title = title;
            }
            Console.Write("New URL (blank to leave unchanged: ");
            string url = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(url))
            {
                blogToEdit.Url = url;
            }
            
            _blogRepository.Update(blogToEdit.Id, blogToEdit.Title, blogToEdit.Url);

        }

        private void AddTag ()
        {
            foreach (Blog blog in _blogRepository.GetAll())
            {
                Console.WriteLine($"{blog.Id}: {blog.Title}");
            }
            Console.Write("Select the blog id number you wish to add a tag to: ");
            int blogId = int.Parse(Console.ReadLine());

            
            List<Tag> tags = _tagRepository.GetAll();
            foreach (Tag tag in tags)
            {
                Console.WriteLine($"Id: {tag.Id}  Name:{tag.Name}");
            }
            Console.Write("Select a tag id number to apply: ");
            int tagId = int.Parse(Console.ReadLine());

           Tag SelectedTag =tags.Find(t => t.Id == tagId);

            _blogRepository.AddTagToBlog(blogId, tagId);



        }

        private void RemoveTagFromBlog()
        {
            //obtain amount 
            int blogCount = _blogRepository.GetAll().Count();

            foreach (int i in _blogRepository.blogIds())
            {
                    Blog selectedBlog = _blogRepository.GetById(i );
                if (selectedBlog != null)
                {
                    Console.WriteLine("------------------------------------------------------------------");
                    Console.WriteLine($"{selectedBlog.Id}) {selectedBlog.Title} || URL:{selectedBlog.Url}");
                    if (selectedBlog.Tags.Count > 0)
                    {
                        Console.Write("Tags: ");
                        foreach (Tag tag in selectedBlog.Tags)
                        {
                            Console.WriteLine(" " + tag.Name);
                        }
                    }
                    else
                    {
                        Console.WriteLine("This blog has no tags yet!");
                    }
                    Console.WriteLine("------------------------------------------------------------------");
                }

            }

            Console.Write("\n\nSelect the Id of a blog that you want to remove a tag from: ");
            int blogId = int.Parse(Console.ReadLine());

            // now need to display a blog and its tags
            Blog selectBlog = _blogRepository.GetById(blogId);
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine($"{selectBlog.Id}) {selectBlog.Title} || URL:{selectBlog.Url}");
            if (selectBlog.Tags.Count > 0)
            {
                Console.Write("Tags: ");
                foreach (Tag tag in selectBlog.Tags)
                {
                    Console.WriteLine($"\n{tag.Id})  {tag.Name}");
                }
            }
            else
            {
                Console.WriteLine("This blog has no tags yet!");
            }
            Console.WriteLine("------------------------------------------------------------------");

            Console.Write("Select a tag id to delete from this blog: ");
            int TagId = int.Parse(Console.ReadLine());

            int blogTagId =_blogRepository.SelectBlogTagId(TagId, selectBlog.Id);

            _blogRepository.DeleteTagFromBlog(blogTagId);
        }

        
    }
}
