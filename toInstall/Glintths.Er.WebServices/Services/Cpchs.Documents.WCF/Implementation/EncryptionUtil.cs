using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Collections.Generic;

namespace Cpchs.Documents.WCF.Utils
{
    public class EncryptionUtil
    {
        private const string EncryptionString = "_RESULT#";

        public static byte[] GetHashKey(string hashKey)
        {
            // Initialise
            UTF8Encoding encoder = new UTF8Encoding();

            // Get the salt
            const string salt = "I am a nice little salt";
            byte[] saltBytes = encoder.GetBytes(salt);

            // Setup the hasher
            Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(hashKey, saltBytes);

            // Return the key
            return rfc.GetBytes(16);
        }

        public static string Encrypt(string dataToEncrypt)
        {
            byte[] key = (new UnicodeEncoding()).GetBytes(EncryptionString);
            // Initialise
            AesManaged encryptor = new AesManaged {Key = key, IV = key};

            // create a memory stream
            using (MemoryStream encryptionStream = new MemoryStream())
            {
                // Create the crypto stream
                using (CryptoStream encrypt = new CryptoStream(encryptionStream, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    // Encrypt
                    byte[] utfD1 = Encoding.UTF8.GetBytes(dataToEncrypt);
                    encrypt.Write(utfD1, 0, utfD1.Length);
                    encrypt.FlushFinalBlock();
                    encrypt.Close();

                    // Return the encrypted data
                    return Convert.ToBase64String(encryptionStream.ToArray());
                }
            }
        }

        public static string Decrypt(string encryptedString)
        {
            byte[] key = (new UnicodeEncoding()).GetBytes(EncryptionString);
            // Initialise
            AesManaged decryptor = new AesManaged();
            byte[] encryptedData = Convert.FromBase64String(encryptedString);

            // Set the key
            decryptor.Key = key;
            decryptor.IV = key;

            // create a memory stream
            using (MemoryStream decryptionStream = new MemoryStream())
            {
                // Create the crypto stream
                using (CryptoStream decrypt = new CryptoStream(decryptionStream, decryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    // Encrypt
                    decrypt.Write(encryptedData, 0, encryptedData.Length);
                    decrypt.Flush();
                    decrypt.Close();

                    // Return the unencrypted data
                    byte[] decryptedData = decryptionStream.ToArray();
                    return Encoding.UTF8.GetString(decryptedData, 0, decryptedData.Length);
                }
            }
        }
        
        public static void DecryptQueryString(string queryStr, ref Dictionary<string, string> parsedQueryStr)
        {
            char[] paramSplitter = { '&' };
            char[] keyValueSplitter = { '=' };

            string decryptedStr = Decrypt(queryStr.Replace(' ','+'));
            string[] parameters = decryptedStr.Split(paramSplitter);

            foreach (string[] item in parameters.Select(s => s.Split(keyValueSplitter)).Where(item => item.Length >= 2))
            {
                parsedQueryStr.Add(item[0], item[1]);
            }
        }
    }
}