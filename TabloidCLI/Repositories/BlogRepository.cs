using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabloidCLI.Repositories
{
    public class BlogRepository: DatabaseConnector
    {
        public string Title { set; get; }
        public string Url { get; set; }

        public BlogRepository(string connectionString) : base(connectionString) { }
        
    }
}
