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

    // defines what a function/method will look like in terms of return and parameters. 
    public delegate void SIMPLDelegate();
    public delegate void SIMPLStringDelegate(SimplSharpString data);

    /// <summary>
    /// Class1 is our frist examples of ineracting between C# and Crestron SIMPL+.
    /// </summary>
    public class Class1
    {
        // defines a property that can be set from SIMPL+ to allow for a callback to be made from C# to SIMPL+.
        public SIMPLDelegate myCallback { get; set; }
        public SIMPLStringDelegate myStringCallback { get; set; }



        public ushort Initialized = 0;

        public SimplSharpString Name;

        public ushort Init()
        {
            Initialized = 1;

            // do some initialization here

            /* if successful, 
            { 
                returnVal = 1;          
            }
            */

            return Initialized;

        }


        /// <summary>
        /// Prints "Hello, World!" to the Crestron console.
        /// </summary>
        public SimplSharpString SayHello()
        {
            // // creates a single line comment, or an end of line comment
            // This method prints "Hello, World!" to the Crestron console  
            CrestronConsole.PrintLine("Hello, World!");   // comment at the end of a line

            string hi;

            hi = "Hello, World!";

            return new SimplSharpString(hi);


            //hi = new SimplSharpString();
            //hi = "Hello, World!";
            //hi = new SimplSharpString("Hello, World!");
            //return hi;


            //return new SimplSharpString("Hello, World!");

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
                case "callback":
                    // check to make sure something has been assigned to mycallback
                    if (myCallback != null)
                    {
                        myCallback();  // calls the method assigned to myCallback from SIMPL+
                    }
                    break;
                case "fredcallback":
                    if (myStringCallback != null)
                    {
                        myStringCallback(new SimplSharpString("Hello Fred!"));
                    }
                    break;
            }

            // inline conversion of SimplSharpString to string using the ToString() method
            // CrestronConsole.PrintLine("Hello, {0}!", data.ToString());


        }
    }
}
