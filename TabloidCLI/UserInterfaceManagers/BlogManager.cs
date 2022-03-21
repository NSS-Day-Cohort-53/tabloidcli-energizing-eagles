using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class BlogManager: BlogRepository, IUserInterfaceManager
    {
       
        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Blog Menu");
            Console.WriteLine(" 1) Add Blog");
            Console.WriteLine(" 4) View Blogs");
            Console.WriteLine(" 5) Remove Blog");
            Console.WriteLine(" 5) Edit Blog");
            Console.WriteLine(" 0) Go Back");

            return this;
        }

    }
}
