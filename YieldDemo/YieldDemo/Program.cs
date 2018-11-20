using System;
using System.Collections.Generic;

namespace YieldDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get the powers of 2 up to an exponent of 8
            foreach (var result in PowersOf(2, 8))
            {
                Console.Write("{0} ", result);
            }
            Console.WriteLine();
            foreach (var result in PowersOf(2, 8))
            {
                Console.Write("{0} ", result);
            }
            Console.WriteLine();

            // Traverse through a factory of galaxies
            Console.WriteLine();
            var galaxies = new Galaxies();
            foreach (var galaxy in galaxies.NextGalaxy)
            {
                Console.WriteLine(galaxy.ToString());
            }
            Console.WriteLine();
            for (var i = 0; i < 10; i++)
            {
                var g = galaxies.NextGalaxy;
                Console.WriteLine(g.ToString());
            }
            Console.WriteLine();

            // Keep console from closing
            Console.Write("\nPress any key to exit the program . . .");
            Console.ReadKey();
        }

        public static IEnumerable<int> PowersOf(int number, int exponent)
        {
            var result = 1;
            for (var i = 0; i < exponent; i++)
            {
                result *= number;
                yield return result;// The function will pick up where it left off the next time it is called
            }
        }
    }

    public class Galaxies
    {
        public IEnumerable<Galaxy> NextGalaxy
        {
            get
            {
                yield return new Galaxy { Name = "Tadpole", MegaLightYears = 400 };
                yield return new Galaxy { Name = "Pinwheel", MegaLightYears = 25 };
                yield return new Galaxy { Name = "Milky Way", MegaLightYears = 0 };
                yield return new Galaxy { Name = "Andromeda", MegaLightYears = 3 };
            }
        }
    }

    public class Galaxy
    {
        public string Name { get; set; }
        public int MegaLightYears { get; set; }

        public override string ToString()
        {
            return Name + " - Mega Light Years: " + MegaLightYears;
        }
    }
}
