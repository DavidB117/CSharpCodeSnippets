using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursiveInnerExceptionPrinting
{
    class Program
    {
        private const string CONST_LINE_BREAK = "\n";

        static void Main(string[] args)
        {
            Exception firstException = new Exception("first exception");
            Exception secondException = new Exception("second exception", firstException);
            Exception thirdException = new Exception("third exception", secondException);
            Exception fourthException = new Exception("fourth exception", thirdException);
            Exception fifthException = new Exception("fifth exception", fourthException);
            Exception sixthException = new Exception("sixth exception", fifthException);
            Exception seventhException = new Exception("seventh exception", sixthException);
            Exception topLevel = new Exception("top level exception", seventhException);

            var innerExceptions = new StringBuilder();

            GetInnerExceptions(topLevel, innerExceptions);
            //GetInnerExceptions(topLevel, innerExceptions, int.MaxValue);

            Console.WriteLine("Inner Exceptions");
            Console.WriteLine("----------------");
            Console.WriteLine(innerExceptions.ToString());

            Console.Write("\n\n\nPress Enter to exit . . .");
            Console.ReadLine();
        }

        private static void GetInnerExceptions(Exception ex, StringBuilder innerExceptions)
        {
            if (ex != null)
            {
                innerExceptions.Append("Source: " + ex.Source + CONST_LINE_BREAK);
                innerExceptions.Append("Message: " + ex.Message + CONST_LINE_BREAK);
                innerExceptions.Append("Stack Trace: " + ex.StackTrace + CONST_LINE_BREAK);
                innerExceptions.Append(CONST_LINE_BREAK);
                if (ex.InnerException != null)
                {
                    GetInnerExceptions(ex.InnerException, innerExceptions);
                }
            }
        }

        private static void GetInnerExceptions(Exception ex, StringBuilder innerExceptions, int levelsDeep, int n = 0)
        {
            if (ex != null && n <= levelsDeep)
            {
                innerExceptions.Append("Source: " + ex.Source + CONST_LINE_BREAK);
                innerExceptions.Append("Message: " + ex.Message + CONST_LINE_BREAK);
                innerExceptions.Append("Stack Trace: " + ex.StackTrace + CONST_LINE_BREAK);
                innerExceptions.Append(CONST_LINE_BREAK);
                if (ex.InnerException != null)
                {
                    GetInnerExceptions(ex.InnerException, innerExceptions, levelsDeep, n + 1);
                }
            }
        }

    }
}
