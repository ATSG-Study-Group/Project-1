using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crestron.SimplSharp;
using CrestronSharp;

namespace LibrarySystem
{
    internal class LibraryCore
    {


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



            // If we make it all the way here, set catalogLoaded
            catalogLoaded = true;
        }


        //--------------------------------------------------------------

        /// <summary>
        /// Indicates whether debugging is enabled.
        /// </summary>
        public bool Debug = false;

    }
}
