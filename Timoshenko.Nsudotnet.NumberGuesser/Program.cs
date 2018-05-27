using System;
using System.Collections.Generic;

namespace Timoshenko.Nsudetnet.NumberGuesser
{
    class NumberGuesser
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Hi!Please,write your name:");
            string name = Console.ReadLine();

            Random rand = new Random();
            int number =  rand.Next(100);
            

            string[] looserstring = { "Oh, dear {0},you are looser.",
                                 "{0}, maybe not today. {0}, maybe not this year.",
                                 "Go study,schoolchild."};


            List<string> experienceList = new List<string>();

            DateTime start = DateTime.Now;

            int chosenValue;

            while (true)
            {
                Console.WriteLine("Let's play one game.You chould guess the number in my mind.Write a number from 0 to 100:");
                string line = Console.ReadLine();
                Boolean good = int.TryParse(line,out chosenValue);

                if (line.Contains("q"))
                {
                    Console.WriteLine("Bad player,i am sorry.Goodbye,dear {0}", name);
                    Console.ReadLine();
                    return;
                }

                if (good)
                {
                    if (chosenValue == number)
                    {
                        Console.WriteLine("OOOOOOOYYYEEEE.REALLY.I GUESSED IT.Congrats!", experienceList.Count);
                        Console.WriteLine("Number of tryes: {0}", experienceList.Count);
                        foreach (string s in experienceList)
                            Console.WriteLine(s);
                        TimeSpan spentTime = DateTime.Now - start;
                        Console.WriteLine("Spent time in minuts: {0}", (int)spentTime.TotalMinutes);
                        Console.ReadLine();
                        return;
                    }
                    else if (chosenValue < number)
                    {
                        Console.WriteLine("{0} < guess number", chosenValue);
                        experienceList.Add(String.Format(" {0} < {1}", chosenValue, number));
                    }
                    else if (chosenValue > number)
                    {
                        Console.WriteLine("{0} > guess number", chosenValue);
                        experienceList.Add(String.Format(" {0} > {1}", chosenValue, number));
                    }
                }
                else
                    Console.WriteLine("No,you should write a number or a symbol q to exit.");



                if (experienceList.Count % 4 == 0)
                {
                    Console.WriteLine(looserstring[rand.Next(looserstring.Length)], name);
                    Console.ReadLine();
                }

                }
        }
    }
}
