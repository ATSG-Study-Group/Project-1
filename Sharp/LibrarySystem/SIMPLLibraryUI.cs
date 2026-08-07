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
        // the default constructor for the class -- SIMPL+ can only call the default constructor
        public SIMPLLibraryUI() 
        { 
        }

        // initialization logic goes here
        public ushort Init()
        {
            return 1;
        }

    }
}
