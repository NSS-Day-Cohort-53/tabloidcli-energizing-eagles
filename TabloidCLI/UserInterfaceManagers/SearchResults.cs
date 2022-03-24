using System;
using System.Collections.Generic;

namespace TabloidCLI.UserInterfaceManagers
{
    public class SearchResults<T>
    {
        //field list of type T?
        private List<T> _results = new List<T>();

        public string Title { get; set; } = "Search Results";

        // this is true when there are no results and otherwise false
        public bool NoResultsFound
        {
            get
            {
                return _results.Count == 0;
            }
        }
        
        // adds a result to th list _results
        public void Add(T result)
        {
            _results.Add(result);
        }

        // shows list title and results
        public void Display()
        {
            Console.WriteLine(Title);

            foreach (T result in _results)
            {   
                Console.WriteLine("-------------------------------------------------------------------------------");
                Console.WriteLine(" " + result);
                Console.WriteLine("-------------------------------------------------------------------------------");
            }

            Console.WriteLine();
        }
    }
}
