using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Repositories;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class BlogManager: BlogRepository, IUserInterfaceManager
    {

        public BlogManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _authorRepository = new AuthorRepository(connectionString);
            _connectionString = connectionString;
        }


        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Blog Menu");
            Console.WriteLine(" 1) Add Blog to faves");
            Console.WriteLine(" 2) View Blogs");
            Console.WriteLine(" 3) Remove Blog");
            Console.WriteLine(" 4) Edit Blog");
            Console.WriteLine(" 0) Go Back");

            int menuOp = int.Parse(Console.ReadLine());

            switch (menuOp)
            {
                case 1:

                    break;
                case 2:

                    break;
                case 3:

                    break;
                case 4:

                    break;
                case 0:

                    break;
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


        }

    }
}
