using System.Text;
using BlazorApp.Shared.ConfigurationValues;
using Core.Framework.Domain;

namespace Core.Framework.Extensions
{
    public static class EncryptionExtensions
    {
        private static readonly EncryptionSettings _encryptionSettings = ContainerFactory.GetInstance<EncryptionSettings>();
        private static readonly DataEncryptor _encryptor = new DataEncryptor(Encoding.UTF8.GetBytes(_encryptionSettings.Key), Encoding.UTF8.GetBytes(_encryptionSettings.InitializationVector));

        public static string Encrypt(this string text)
        {
            return _encryptor.EncryptString(text);
        }

        public static string Decrypt(this string text)
        {
            return _encryptor.DecryptString(text);
        }
    }
}
