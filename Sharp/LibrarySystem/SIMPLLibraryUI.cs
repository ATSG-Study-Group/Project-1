using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crestron.SimplSharp;
using CrestronSharp;

namespace LibrarySystem
{
    // Class to function as our interface between C# and SIMPL+
    public class SIMPLLibraryUI
    {
        // crate a variable to refrence the library core.
        LibraryCore myCore;

        // the default constructor for the class -- SIMPL+ can only call the default constructor
        public SIMPLLibraryUI() 
        { 
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
