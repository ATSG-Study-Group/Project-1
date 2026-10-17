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
    /// Event Args class to pass book details back to SIMPL+
    /// </summary>
    public class BookArgs : EventArgs  // defining BookArgs as a derivitive class of EventArgs.  
    {
        //properties to pass between C# and SIMPL+
        public SimplSharpString Title { get; set; }
        public SimplSharpString Author { get; set; }
        public SimplSharpString Genre { get; set; }
        public SimplSharpString Description { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public BookArgs() { }

        internal BookArgs(Book book)
        {
            if (book != null)
            {
                this.Title = book.Title;
                this.Author = book.Author;
                this.Genre = book.Genre;
                this.Description = book.Description;
            }
            else
            {
                this.Title = "";
                this.Author = "";
                this.Genre = "";
                this.Description = "";
            }
        }

    }
}
