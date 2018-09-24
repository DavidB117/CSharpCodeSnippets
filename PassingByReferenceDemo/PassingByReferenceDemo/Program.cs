using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassingByReference
{
    class Program
    {
        public class Dog
        {
            public string Name { get; set; } = string.Empty;
            public string Breed { get; set; } = string.Empty;

            public Dog(string name = "Fido", string breed = "Golden Retriever")
            {
                Name = name;
                Breed = breed;
            }

            public void CallDog()
            {
                Console.WriteLine(Name + "!");
            }
        }

        static void Main(string[] args)
        {
            Program p = new Program();

            // Passing an object by value
            var d = new Dog();
            d.CallDog();
            p.ChangeDogName(d, "Pavlov");
            d.CallDog();
            Console.WriteLine();

            // Passing a primitive by value
            var i = 0;
            Console.WriteLine("integer value: " + i);
            p.IncrementInteger(i);
            p.IncrementInteger(i);
            p.IncrementInteger(i);
            Console.WriteLine("integer value: " + i);
            Console.WriteLine();

            // Passing a collection by value
            var l = new List<string>();
            Console.WriteLine(String.Join(", ", l));
            p.FillList(l);
            Console.WriteLine(String.Join(", ", l));
            Console.WriteLine();

            // Passing a primitive by ref
            var i2 = 10;
            Console.WriteLine("integer 2 value: " + i2);
            p.IncrementIntegerByRef(ref i2);
            p.IncrementIntegerByRef(ref i2);
            p.IncrementIntegerByRef(ref i2);
            Console.WriteLine("integer 2 value: " + i2);
            Console.WriteLine();

            Console.WriteLine("Press Enter to exit the program . . .");
            Console.ReadLine();
        }

        private void IncrementIntegerByRef(ref int i)
        {
            i++;
        }

        private void FillList(List<string> l)
        {
            l.Add("Wayne");
            l.Add("Bruce");
            l.Add("John");
        }

        private void IncrementInteger(int i)
        {
            i++;
        }

        private void ChangeDogName(Dog dog, string newName)
        {
            dog.Name = newName;
        }

    }
}
