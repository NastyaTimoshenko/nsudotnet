using System;
using System.IO;
using System.Security.Cryptography;

namespace Timoshenko.Nsudotnet.Enigma
{
    class Enigma
    {
        static void Main(string[] args)
        {
            try
                {
                    if (args.Length == 4 && args[0] == "encrypt")
                           encrypt(args[1], args[2], args[3]);
                   
                    else if (args.Length == 5 && args[0] == "decrypt")
                               decrypt(args[1], args[2], args[3], args[4]);
                    
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

            
             void encrypt(string inputFile, string alg, string outputFile)
            {
                var algorithm = GetAlgorithmName(alg);

                algorithm.GenerateKey();
                algorithm.GenerateIV();

                byte[] Key = algorithm.Key;
                byte[] IV = algorithm.IV;
                string base64Key = Convert.ToBase64String(Key);
                string base64IV = Convert.ToBase64String(IV);

                var encryptor = algorithm.CreateEncryptor(Key, IV);

                using (var outputFileStream = new FileStream(outputFile, FileMode.Create))
                {
                    using (var cryptoStream = new CryptoStream(outputFileStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (var inputFileStream = new FileStream(inputFile, FileMode.Open))
                                   inputFileStream.CopyTo(cryptoStream);
                        
                    }
                }

                string path = Path.GetDirectoryName(inputFile);
                string keyFilePath = Path.Combine(path, "file.key.txt");
                string[] info = { base64Key, base64IV };
                File.WriteAllLines(keyFilePath, info);
            }



              void decrypt(string inputFile, string alg, string keyFile, string outputFile)
              {
                var cryptAlgorithm = GetAlgorithmName(alg);

                string[] info = File.ReadAllLines(keyFile);
                byte[] rgbKkey = Convert.FromBase64String(info[0]);
                byte[] rgbIV = Convert.FromBase64String(info[1]);

                var decryptor = cryptAlgorithm.CreateDecryptor(rgbKkey, rgbIV);

                using (var inputFileStream = new FileStream(inputFile, FileMode.Open))
                {
                    using (var outputFileStream = new FileStream(outputFile, FileMode.Create))
                    {
                        using (var cryptoStream = new CryptoStream(inputFileStream, decryptor, CryptoStreamMode.Read))
                               cryptoStream.CopyTo(outputFileStream);
                     }
                }
              }

           SymmetricAlgorithm GetAlgorithmName(string alg)
            {
                SymmetricAlgorithm symmetricalgorithm;

                switch (alg)
                {
                    case "aes":
                        symmetricalgorithm = new AesManaged();
                        break;
                    case "des":
                        symmetricalgorithm = new DESCryptoServiceProvider();
                        break;
                    case "rc2":
                        symmetricalgorithm = new RC2CryptoServiceProvider();
                        break;
                    case "rijndael":
                        symmetricalgorithm = new RijndaelManaged();
                        break;
                    default:
                        Console.WriteLine("Wrong algorithm name. Type, please, one from this: aes, des, rc2, rijndael");
                        return null;
                }
                return symmetricalgorithm;
            }
        }
    }
}
