using System;

namespace Timoshenko.Nsudotnet.Enigma
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length == 4 && args[0] == "encrypt")
                Encrypt.EncryptFile(args[1], args[2], args[3]);

            else if (args.Length == 5 && args[0] == "decrypt")
                Decrypt.DecryptFile(args[1], args[2], args[3], args[4]);

            else
            {
                Console.WriteLine("Wrong arguments.Should be next:");
                Console.WriteLine("First way: encrypt,inputFile,algorithm,outputFile");
                Console.WriteLine("Second way: decrypt,inputFile,algorithm,keyFile,outputFile");
                Console.ReadLine();
            }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }
    }
}