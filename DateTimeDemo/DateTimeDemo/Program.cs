using System;

namespace DateTimeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Current time.
            Console.Write("Current Time : ");
            Console.WriteLine(DateTime.Now + "\n");

            //Specify the year, month, and day.
            Console.Write("Specified Year/Month/Day : ");
            Console.WriteLine(new DateTime(2018, 3, 26) + "\n");

            //Properties of a Datetime object.
            Console.WriteLine("Properties of a DateTime object . . .");
            DateTime date = new DateTime(2012, 12, 25);
            Console.WriteLine("Year = " + date.Year);
            Console.WriteLine("Month = " + date.Month);
            Console.WriteLine("Day = " + date.Day);
            Console.WriteLine("Day of Week: " + date.DayOfWeek);
            Console.WriteLine("Time of Day: " + date.TimeOfDay + "\n");
            /*
             * A single tick represents one hundred nanoseconds or one ten-millionth of a second.
             * There are 10,000 ticks in a millisecond, or 10 million ticks in a second.
             */
            Console.WriteLine("# of Ticks : " + date.Ticks);

            //UtcNow
            Console.WriteLine("Gets a DateTime object that is set to the current date");
            Console.WriteLine("and time on this computer, expressed as the Coordinated");
            Console.WriteLine("Universal Time (UTC).");
            Console.WriteLine(DateTime.UtcNow);

            //Used to keep program from exiting while debugging.
            Console.WriteLine("\nPress any key to exit program . . .");
            Console.ReadKey();
        }
    }
}
