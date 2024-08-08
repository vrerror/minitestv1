using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Course.Core.Helpers;
using Course.Core.Common;

namespace Course.Core.Helpers
{
    public class SecureHelper
    {
        private static readonly byte[] defaultKey = SecretKey.Default;

        public static string Encrypt(string data) => Encrypt(defaultKey, data);

        public static string Encrypt(byte[] key, string data)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new())
                {
                    using (CryptoStream cryptoStream = new(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new(cryptoStream))
                        {
                            streamWriter.Write(data);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public static string Decrypt(string encrypted) => Decrypt(defaultKey, encrypted);

        public static string Decrypt(byte[] key, string encrypted)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(encrypted);

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

    }
}
