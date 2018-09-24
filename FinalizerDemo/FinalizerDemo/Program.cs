using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalizerDemo
{
    class Program
    {
        class First
        {
            ~First()
            {
                System.Diagnostics.Trace.WriteLine("First's destructor is called.");
            }
        }

        class Second : First
        {
            ~Second()
            {
                System.Diagnostics.Trace.WriteLine("Second's destructor is called.");
            }
        }

        class Third : Second
        {
            ~Third()
            {
                System.Diagnostics.Trace.WriteLine("Third's destructor is called.");
            }
        }

        static void Main(string[] args)
        {
            // Destructor text is output to VS output window
            Third t = new Third();
        }
    }
}
