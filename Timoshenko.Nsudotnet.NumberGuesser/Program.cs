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
            int number = rand.Next(100);

            string[] looserstring = { "Oh, dear {0},you are looser.",
                                 "{0}, maybe not today. {0}, maybe not this year.",
                                 "Go study,schoolchild."};


            List<string> experienceList = new List<string>();

            DateTime start = DateTime.Now;
            while (true)
            {
                Console.WriteLine("Let's play one game.You chould guess the number in my mind.Write a number from 0 to 100:");
                string line = Console.ReadLine();
                if (line.Contains("q"))
                {
                    Console.WriteLine("Bad player,i am sorry.Goodbye,dear {0}", name);
                    Console.ReadLine();
                    return;
                }

                            
                 int chosenValue = int.Parse(line);
               

                if (chosenValue == number)
                {
                    TimeSpan spentTime = DateTime.Now - start;
                    Console.WriteLine("OOOOOOOYYYEEEE.REALLY.I GUESSED IT.Congrats!", experienceList.Count);
                    Console.WriteLine("Number of tryes: {0}", experienceList.Count);
                    foreach (string s in experienceList)
                          Console.WriteLine(s);
                    
                    Console.WriteLine("Spent time: {0}", spentTime);
                    Console.ReadLine();
                    return;
                }

                if (chosenValue < number)
                {
                    Console.WriteLine("{0} < guess number", chosenValue);
                    experienceList.Add(String.Format(" {0} < {1}", chosenValue, number));
                }
                else
                {
                    Console.WriteLine("{0} > guess number", chosenValue);
                    experienceList.Add(String.Format(" {0} > {1}", chosenValue, number));
                }

                if (experienceList.Count % 4 == 0)
                    Console.WriteLine(looserstring[rand.Next(looserstring.Length)], name);
                
            }

        }
    }
}
