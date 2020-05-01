using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Core.Framework.Domain
{
    public class DataEncryptor
    {
        #region Factory
        public DataEncryptor()
        {
            Algorithm = new AesCryptoServiceProvider();
            Algorithm.Padding = PaddingMode.ISO10126;
            Algorithm.Mode = CipherMode.CBC;
        }
        public DataEncryptor(AesCryptoServiceProvider keys)
        {
            Algorithm = keys;
        }

        public DataEncryptor(byte[] key, byte[] iv)
        {
            Algorithm = new AesCryptoServiceProvider();
            Algorithm.Padding = PaddingMode.ISO10126;
            Algorithm.Mode = CipherMode.CBC;
            Algorithm.Key = key;
            Algorithm.IV = iv;
        }

        #endregion

        #region Properties
        public AesCryptoServiceProvider Algorithm { get; set; }
        public byte[] Key
        {
            get { return Algorithm.Key; }
            set { Algorithm.Key = value; }
        }
        public byte[] IV
        {
            get { return Algorithm.IV; }
            set { Algorithm.IV = value; }
        }

        #endregion

        #region Crypto

        public byte[] Encrypt(byte[] data) { return Encrypt(data, data.Length); }
        public byte[] Encrypt(byte[] data, int length)
        {
            try
            {
                // Create a MemoryStream.
                var ms = new MemoryStream();

                // Create a CryptoStream using the MemoryStream 
                // and the passed key and initialization vector (IV).
                var cs = new CryptoStream(ms,
                    Algorithm.CreateEncryptor(Algorithm.Key, Algorithm.IV),
                    CryptoStreamMode.Write);

                // Write the byte array to the crypto stream and flush it.
                cs.Write(data, 0, length);
                cs.FlushFinalBlock();

                // Get an array of bytes from the 
                // MemoryStream that holds the 
                // encrypted data.
                byte[] ret = ms.ToArray();

                // Close the streams.
                cs.Close();
                ms.Close();

                // Return the encrypted buffer.
                return ret;
            }
            catch (CryptographicException ex)
            {
                Console.WriteLine("A cryptographic error occured: {0}", ex.Message);
            }
            return null;
        }

        public byte[] Decrypt(byte[] data)
        {
            var dec = Decrypt(data, data.Length);
            return dec;
        }

        public byte[] Decrypt(byte[] data, int length)
        {
            try
            {
                // Create a new MemoryStream using the passed 
                // array of encrypted data.
                MemoryStream ms = new MemoryStream(data);

                // Create a CryptoStream using the MemoryStream 
                // and the passed key and initialization vector (IV).
                CryptoStream cs = new CryptoStream(ms,
                    Algorithm.CreateDecryptor(Algorithm.Key, Algorithm.IV),
                    CryptoStreamMode.Read);

                // Create buffer to hold the decrypted data.
                byte[] result = new byte[length];

                // Read the decrypted data out of the crypto stream
                // and place it into the temporary buffer.
                cs.Read(result, 0, result.Length);
                return result;
            }
            catch (CryptographicException ex)
            {
                Console.WriteLine("A cryptographic error occured: {0}", ex.Message);
            }
            return null;
        }

        public string EncryptString(string text)
        {
            if (string.IsNullOrEmpty(text))
                return null;

            var encryptedBytes = Encrypt(Encoding.UTF8.GetBytes(text.Trim()));

            if (encryptedBytes != null)
                return Convert.ToBase64String(encryptedBytes);

            return null;
        }

        public string DecryptString(string data)
        {
            if (string.IsNullOrEmpty(data))
                return null;

            var decryptedBytes = Decrypt(Convert.FromBase64String(data.Trim()));

            if (decryptedBytes != null)
                return Encoding.UTF8.GetString(decryptedBytes).TrimEnd('\0');

            return null;
        }

        #endregion

    }
}