using System;

namespace Timoshenko.Nsudotnet.Calendar
{
    class Calendar
    {
        static void Main(string[] args)
        {
            Console.Write("Please,enter the date: ");
            String date = Console.ReadLine();
            DateTime enteredate;

            if (!DateTime.TryParse(date, out enteredate))
            {
                Console.WriteLine("Incorrect format of data!Try again.");
                Console.ReadLine();
                return;
            }

            DateTime currentDay = enteredate.AddDays(1 - enteredate.Day);
            currentDay = currentDay.AddDays(1 - (int)currentDay.DayOfWeek);

            do
            {
                if ((int)currentDay.DayOfWeek == 0 || (int)currentDay.DayOfWeek == 6)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                Console.Write("{0} ", currentDay.ToString("ddd"));
                Console.ResetColor();
                currentDay = currentDay.AddDays(1);

            } while ((int)currentDay.DayOfWeek != 1);
            Console.WriteLine();

            int workingDays = 0;
            currentDay = currentDay.AddDays(-7);
            while (currentDay.Month <= enteredate.Month)
            {
                do
                {
                    if (currentDay.Month != enteredate.Month)
                    { Console.Write("  "); }

                    else
                    {
                        if ((int)currentDay.DayOfWeek == 0 || ((int)currentDay.DayOfWeek) == 6)
                            Console.ForegroundColor = ConsoleColor.Red;
                        else
                            workingDays++;

                        if (enteredate == currentDay)
                            Console.BackgroundColor = ConsoleColor.Blue;

                        if (DateTime.Today == currentDay)
                            Console.BackgroundColor = ConsoleColor.DarkGray;

                        Console.Write(currentDay.Day.ToString("D2"));

                    }
                    Console.ResetColor();
                    Console.Write(" ");
                    currentDay = currentDay.AddDays(1);

                } while ((int)currentDay.DayOfWeek != 1);
                Console.WriteLine();

            }
            Console.WriteLine("There is {0} working days in this mounth.", workingDays);
            Console.ReadLine();
        }
    }
}
