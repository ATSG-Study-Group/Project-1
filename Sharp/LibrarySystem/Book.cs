using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crestron.SimplSharp;

namespace LibrarySystem
{
    /// <summary>
    /// Contains all the properites of a book, from the catalog.json file
    /// </summary>
    internal class Book : IComparable<Book> //interface that implements comparisons
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// defines the default CompareTo behavior for a book
        /// </summary>
        /// <param name="other">the other book to compare against</param>
        /// <returns>either -1, 0, or 1</returns>
        public int CompareTo(Book other)  //default comparison function between two books compare author name
        {
            int result = this.Author.CompareTo(other.Author);
            if (result == 0) // Author is the same for both books
            { 
                result = this.Title.CompareTo(other.Title); // if author is the same, compare the titles
            }
            return result;
        }

        /// <summary>
        /// Prints the details about a book to console
        /// </summary>
        public void PrintBook()
        {
            CrestronConsole.PrintLine("       Title: {0}", Title);
            CrestronConsole.PrintLine("      Author: {0}", Author);
            CrestronConsole.PrintLine("       Genre: {0}", Genre);
            CrestronConsole.PrintLine(" Description: {0}", Description);
            CrestronConsole.PrintLine("");
        }

        
    }
}
