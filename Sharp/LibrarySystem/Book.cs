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
    internal class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }

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
