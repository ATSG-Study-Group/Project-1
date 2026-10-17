using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crestron.SimplSharp;
using CrestronSharp;

namespace LibrarySystem
{
    /// <summary>
    /// Delegate to let simpl+ know when we the display list has been updated
    /// </summary>
    /// <param name="bookCount">how many books are in the new list</param>
    public delegate void ListUpdateDelegate(ushort bookCount);


    /// <summary>
    /// Delegate to pass the details about a book back to SIMPL+
    /// </summary>
    /// <param name="SelectedBookArgs">the details about the book</param>
    public delegate void SelectedBookDelegate(BookArgs SelectedBookArgs);

    // Class to function as our interface between C# and SIMPL+
    public class SIMPLLibraryUI
    {
        // crate a variable to refrence the library core.
        LibraryCore myCore;

        // list of books to be displayed on the UI
        private List<Book> displayList;

        private Book myBook;

        // the default constructor for the class -- SIMPL+ can only call the default constructor
        public SIMPLLibraryUI() 
        { 
        }

        /// <summary>
        /// callback to regiser for list Updates
        /// </summary>
        public ListUpdateDelegate ListUpdateCallback { get; set; }

        /// <summary>
        /// callback to register to for book updates
        /// </summary>
        public SelectedBookDelegate SelectedBookCallback { get; set; }

        /// <summary>
        /// gets the data to be displayed on an individual line of the display list
        /// </summary>
        /// <param name="index">index of the line to display</param>
        /// <returns>the data to display</returns>
        public SimplSharpString GetListLine(ushort index)
        {
            // validate displayList
            if (displayList != null)
            {
                // validate index
                if ((index > 0) && (index <= displayList.Count))
                {
                    // build our response
                    // String.Format allows you to creat a string and apply formatting similar to a print statement
                    string s = String.Format("{0}\n{1}", displayList[index-1].Title, displayList[index-1].Author);  // C# is 0 based indexes

                    // return the response
                    return new SimplSharpString(s);  // remember SIMPL+ uses a different string format than C#
                }
            }
            return "";
        }

        /// <summary>
        /// Select a specific book from the list
        /// </summary>
        /// <param name="index">index of the book to select</param>
        public void SelectBook(ushort index)
        {
            // check to make sure the list exists
            if (displayList != null)
            {
                // check to make sure index points to a valid book
                if ((index > 0) && (index <= displayList.Count))
                {
                    // set myBook to the book at index
                    myBook = displayList[index - 1]; // remember arrays in C# are 0 based index
                }
                else 
                    myBook = null;  // if the code block is a single line, C# will imply the { }
                
            }
            else { myBook = null; }  // for single line code blocks, you can put it all on one line

            // return the details about the book to SIMPL+
            if (SelectedBookCallback != null)
            {
                SelectedBookCallback(new BookArgs(myBook));  // because all I'm using bookargs for is to pass it to SlectectedBookCallback,
                                                             // I'm not creating a BookArgs variable, I'm crating new BookArgs and passing it right in 
                                                             // to SelectedBookCallback
            }

        }

        // initialization logic goes here
        public ushort Init()
        {
            myCore = new LibraryCore();
            return 1;
        }

        // load the catalog from file system
        public ushort LoadCatalog()
        {
            if (myCore != null)
            {
                // call load catalog
                myCore.LoadCatalog();

                // test to see if it loaded correctly
                if (myCore.CatalogLoaded)
                {
                    //displayList = myCore.GetCatalog();
                    displayList = myCore.GetCatalog(LibraryCore.SortType.Author);  //load the complete catalog into the display list
                    if ((displayList != null) && (ListUpdateCallback != null)) // check to confirm there is data in the display list and that someone is listening
                    {
                        ListUpdateCallback((ushort)displayList.Count);   // (ushort) is a cast to tell the compiler to treat the int value as a ushort
                    }
                    return 1;
                }
            }
            return 0;
        }


        //-----------------------------------------

        // ushort property Debug that is accessable to SIMPL+
        public ushort Debug
        {
            // get is run whenever you look up the current value of Debug
            get 
            {
                if (myCore != null) // check to make sure myCore is pointing to a Library Core
                {
                    if (myCore.Debug) { return 1; }
                    return 0;
                }
                else { return 0; }
            }
            // set is run whenever you try to set Debug to a value
            // that value is passed into set as the variable 'value'
            set 
            {
                if (myCore != null)
                {
                    if(value > 0) { myCore.Debug = true; }
                    else { myCore.Debug = false; }
                    CrestronConsole.PrintLine("Debug is set to {0}", myCore.Debug);
                    // Console.WriteLine(""); - windows console version - google it for help and ideas
                }
            }
        }

    }
}
