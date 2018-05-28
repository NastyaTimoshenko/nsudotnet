using System.Security.Cryptography;
using System.IO;
using System;

namespace Timoshenko.Nsudotnet.Enigma
{
    class Decrypt
    {
        public static void DecryptFile(string inputFile, string alg, string keyFile, string outputFile)
        {
            var cryptAlgorithm = Algorithm.GetAlgorithm(alg);

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
    }
}
