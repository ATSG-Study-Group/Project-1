using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem
{
    /// <summary>
    /// library catalog - root object of the catalog.json file
    /// </summary>
    internal class Catalog
    {
        public List<Book> BookList { get; set; }

        /// <summary>
        /// Prints the catalog to console
        /// </summary>
        public void PrintCatalog()
        {
            foreach (Book book in BookList)
            {
                book.PrintBook();
            }
        }
    }
}
