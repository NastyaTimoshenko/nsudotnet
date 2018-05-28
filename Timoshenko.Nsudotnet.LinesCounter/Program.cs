using System;
using System.IO;


namespace Timoshenko.Nsudotnet.LinesCounter
{
    class MainLinesCounter
    {
        static void Main(string[] args)
        {

            if (args.Length == 1)
            {
                var linesCounter = new LinesCounter(args[0]);
                linesCounter.CountLines();
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Type, please,write the expansion of files in which you want to count the number of lines.");
                Console.ReadKey();
                return;
            }
        }
    }


    class LinesCounter
    {
        private string expansion;

        public LinesCounter(string expans)
        {
            expansion = expans;
        }

        public void CountLines()
        {
            var files = Directory.EnumerateFiles(Directory.GetCurrentDirectory(), "*." + expansion, SearchOption.AllDirectories);
            int amount = 0;

            foreach (var file in files)
            {
                using (var streamReader = new StreamReader(file))
                {
                    int count = 0;
                    bool multiComment = false;
                    string line = "";
                    int yes = 0;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                        if (line == ("") || line.StartsWith("//")) continue;
                        if (line.StartsWith("/*")) multiComment = true;

                        if (!multiComment)
                        {
                            for (int i = 0; i < line.Length; i++)
                            {
                                if (char.IsWhiteSpace(line[i]) || line[i] == '\t')
                                {
                                    continue;
                                }
                                else
                                    if (line[i] == '/' && i < line.Length - 1)
                                {
                                    if (line[i + 1] == '*')
                                    {
                                        multiComment = true;
                                        break;
                                    }
                                    if (line[i + 1] == '/')
                                        break;
                                }
                                else if (char.IsDigit(line[i]))
                                    count++;
                                Console.WriteLine(count);
                            }
                        }
                        else if (line.Contains("*/"))
                        {
                            for (int i = 0; i < line.Length; i++)
                            {
                                if (line[i] == '*' && i < line.Length - 1)
                                {
                                    if (line[i + 1] == '/')
                                    {
                                        multiComment = false;
                                        continue;
                                    }

                                }
                                if (!multiComment)
                                {
                                    if (char.IsWhiteSpace(line[i]) || line[i] == '\t')
                                    {
                                        continue;
                                    }
                                    else
                                         if (line[i] == '/' && i < line.Length - 1)
                                    {
                                        if (line[i + 1] == '*')
                                        {
                                            multiComment = true;
                                            break;
                                        }
                                        if (line[i + 1] == '/')
                                            break;
                                    }
                                    else if (char.IsDigit(line[i]))
                                    {
                                        count++;
                                        Console.WriteLine(count);
                                        break;
                                    }
                                    
                                }
                            }
                        }
                    }
                    amount += count;
                    count = 0;
                }
            }
            Console.WriteLine("Expansion {0} : amount {1}.", expansion, amount);
        }

    }
}









