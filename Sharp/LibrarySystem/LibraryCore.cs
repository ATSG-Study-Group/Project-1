using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crestron.SimplSharp;
//using CrestronSharp;
using Newtonsoft.Json;

namespace LibrarySystem
{
    internal class LibraryCore
    {
        private Catalog myCatalog;

        // default constructor
        public LibraryCore() 
        {
           catalogLoaded = false;
        }

        // indicates if the catalog has been loaded
        private bool catalogLoaded;
        /// <summary>
        /// True if the catalog is loaded
        /// </summary>
        public bool CatalogLoaded {  get { return catalogLoaded; } }

        /// <summary>
        /// Loads the catalog file from the file system and parses it into the datastructure
        /// </summary>
        public void LoadCatalog()
        {
            // read the catalog data in from the file
            FileOperations myFileOperations = new FileOperations();

            if (myFileOperations.GetContent("\\NVRAM\\catalog.json") != FileOperations.ErrorEnum.SUCCESS)
            {
                catalogLoaded = false;
                ErrorLog.Warn("Unable to load catalog file from file system.");
                return;
            }

            if (Debug)
            {
                CrestronConsole.PrintLine("catalog file loaded");
                CrestronConsole.PrintLine(myFileOperations.Content);
            }


            // parse the json into the data structue
            try
            {
                // calling newtonsoft to parse the json file into an instance of Catalog
                myCatalog =  JsonConvert.DeserializeObject<Catalog>(myFileOperations.Content);

                if (myCatalog == null) //failed to parse correctly
                {
                    catalogLoaded = false;
                    ErrorLog.Warn("Unable to parse catalog file.");
                    return;
                }

                if (Debug)
                {
                    CrestronConsole.PrintLine("Catalog Loaded!");
                    myCatalog.PrintCatalog();
                }

            }
            catch (Exception e)
            {
                // C# 101 way of handling an exception
                catalogLoaded = false;
                ErrorLog.Error("Exception thrown while parsing catalog.json: {0}", e.Message);
                return;
            }


            // If we make it all the way here, set catalogLoaded
            catalogLoaded = true;
        }

        /// <summary>
        /// method that returns a list containing the entire catalog
        /// </summary>
        /// <returns>the list of books in the catalog</returns>
        public List<Book> GetCatalog()
        {
            if (catalogLoaded)
                return myCatalog.BookList.ToList();  //creating a new list from the master list and returning that.
            else
            {
                ErrorLog.Warn("GetCatalog called before the catalog was loaded.");
                return null;    // if catalog is not loaded, still need to return something, in this case we return a null.
            }
        }

        /// <summary>
        /// What property to sort the catalog by.
        /// </summary>
        public enum SortType
        { 
            Author, Title, Genre
        }

        /// <summary>
        /// returns the catalog sorted by a property
        /// </summary>
        /// <param name="sortBy">The property to sort by</param>
        /// <returns>the sorted list of books in the catalog</returns>
        public List<Book> GetCatalog(SortType sortBy)
        {
            List<Book> books = GetCatalog();    //creates a new list of books from the catalog, or null if not available
            //CrestronConsole.PrintLine("made my copy");
            if (books != null)  // check to make sure books contains books
            {
                //CrestronConsole.PrintLine("made it to the switch");
                switch (sortBy)
                {
                    case SortType.Author:
                        //CrestronConsole.PrintLine("sort by author");
                        books.Sort();  //uses the default CompareTo for book
                        break;
                    case SortType.Title:
                        // sort uses a lambda function to define how to compare two books for the sort 
                        // for this example we are using two books x and y  then the lambda operator => followed by logic that does the comparison of x and y
                        // this is shorthand for a method: private int compare (book x, book y) that returns -1 if x < y, 0 if they are the same, and 1 if x > y
                        books.Sort((x,y) => x.Title.CompareTo(y.Title)); 
                        break;
                    case SortType.Genre:
                        books.Sort((x,y) => x.Genre.CompareTo(y.Genre)); 
                        break;
                }
            }
            return books;
        }

        //--------------------------------------------------------------

        /// <summary>
        /// Indicates whether debugging is enabled.
        /// </summary>
        public bool Debug = false;

    }
}
