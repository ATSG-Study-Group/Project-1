using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crestron.SimplSharp;

namespace HelloWorld
{
    /*  Variable conversions between SIMPL+ and C# are as follows:
     *  SIMPL+ Type           C# Type
     *  -------------------------------
     *  INTEGER               ushort
     *  LONG_INTEGER          uint
     *  SIGNED_INTEGER        short
     *  SIGNED_LONG_INTEGER   int
     *  STRING                SimplSharpString
     *  BOOLEAN  does note exist in SIMPL+
     *  
     */


    /// <summary>
    /// Class1 is our frist examples of ineracting between C# and Crestron SIMPL+.
    /// </summary>
    public class Class1
    {
        /// <summary>
        /// Prints "Hello, World!" to the Crestron console.
        /// </summary>
        public void SayHello()
        {
            // // creates a single line comment, or an end of line comment
            // This method prints "Hello, World!" to the Crestron console  
            CrestronConsole.PrintLine("Hello, World!");   // comment at the end of a line

            /* This is a multi-line comment
             * 
             * more details about the comment can be added here
             */
        }

        /// <summary>
        /// method that takes a string from SIMPL+ and prints it to the Crestron console.
        /// </summary>
        /// <param name="data">string to print to console</param>
        public void StringToCSharp(SimplSharpString data)
        {
            string command = data.ToString();

            switch (command.ToLower())
            { 
                case "sayhello":
                    SayHello();
                    break;
            }


            // inline conversion of SimplSharpString to string using the ToString() method
            // CrestronConsole.PrintLine("Hello, {0}!", data.ToString());


        }
    }
}
