using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GotoDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] arr = new int[,] { { 0, 1, 2, 3 }, { 4, 5, 6, 7 }, { 8, 9, 10, 11 } };

            int myNum = 6;

            for (var row = 0; row < arr.GetLength(0); row++)
            {
                for (var col = 0; col < arr.GetLength(1); col++)
                {
                    Console.Write(arr[row, col] + "  ");
                    if (arr[row, col] == myNum)
                    {
                        goto found;
                    }
                }
                Console.WriteLine();
            }

        found:
            Console.WriteLine("\n\n\nPress Enter to exit the program . . .");
            Console.ReadLine();
        }
    }
}
