using System.Security.Cryptography;
using System.IO;
using System;

namespace Timoshenko.Nsudotnet.Enigma
{
    class Encrypt
    {
        private const string KeyFileName = "file.key.txt";

        public static void EncryptFile(string inputFile, string alg, string outputFile)
        {
            var algorithm = Algorithm.GetAlgorithm(alg);

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
    }
}
