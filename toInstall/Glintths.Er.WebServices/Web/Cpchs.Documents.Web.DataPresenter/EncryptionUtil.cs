using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Collections.Generic;

namespace Cpchs.Documents.Web.DataPresenter
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


        private static int[] s;
        private static int[] kep;
        private static int i;
        private static int j;

        public static bool DecodeFile(string key, string inputFile, string outputFile)
        {
            //MessageBox.Show(inputFile + " -> " + outputFile);

            if (string.IsNullOrEmpty(key))
            {
                return false;
                //throw new ArgumentException("Decode key cannot by empty");
            }

            try
            {
                byte[] allBytes = File.ReadAllBytes(inputFile);

                FileStream writer = File.Open(outputFile, FileMode.Create);

                SetDecodeKey(key);

                foreach (byte b in allBytes)
                {
                    writer.WriteByte(DecodeByte(b));
                }

                writer.Close();
            }
            catch
            {
                return false;
            }

            return true;
        }


        private static void SetDecodeKey(string key)
        {
            int b = 0;
            int a, temp;
            i = j = 0;
            s = new int[256];
            kep = new int[256];

            for (a = 0; a < 256; a++)
            {
                

                if (b >= key.Length) b = 0;
                
                //kep[a] = Microsoft.VisualBasic.Strings.Asc(Microsoft.VisualBasic.Strings.Mid(key, b, 1));
                kep[a] = ((int)key.Substring(b, 1)[0]);
                b = b + 1;
            }

            for (a = 0; a < 256; a++)
                s[a] = a;

            b = 0;

            for (a = 0; a < 256; a++)
            {
                /*if (a > 38)
                    this.ToString();*/
                try
                {
                    b = (b + s[a] + kep[a]) % 256;

                }
                catch
                {
                }
                temp = s[a];
                s[a] = s[b];
                s[b] = temp;
            }
        }

        private static byte DecodeByte(byte inByte)
        {
            i = (i + 1) % 256;
            j = (j + s[i]) % 256;

            int temp = s[i];
            s[i] = s[j];
            s[j] = temp;

            int k = s[(s[i] + s[j]) % 256];

            byte decodedByte = Convert.ToByte(inByte ^ k);

            return decodedByte;
        }
    }
}