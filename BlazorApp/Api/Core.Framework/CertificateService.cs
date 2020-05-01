using System.IO;
using System.Security.Cryptography.X509Certificates;
using BlazorApp.Shared.ConfigurationValues;
using BlazorApp.Shared.Managers;
using Core.Framework;
using Microsoft.IdentityModel.Tokens;

namespace Core.Framework
{
    public static class CertificateService
    {
        private const string JwsAlgorithm = "RS256";

        private static X509Certificate2 CreateX509Certificate2(this CertificateSettings certificateSettings)
        {
            var awsSettings = ContainerFactory.GetInstance<AwsSettings>();
            var fileManager = ContainerFactory.GetInstance<IFileManager>();
            fileManager.Root = awsSettings.ConfigBucketName;

            var certificateStream = fileManager.Read($"{certificateSettings.FolderPath}/{certificateSettings.CertificateName}");
            byte[] certificateBytes;
            using (MemoryStream ms = new MemoryStream())
            {
                certificateStream.CopyTo(ms);
                certificateBytes = ms.ToArray();
            }
            var x509Certificate2 = new X509Certificate2(certificateBytes, certificateSettings.CertificatePassword);
            return x509Certificate2;
        }

        public static SigningCredentials SigningCredentials(this CertificateSettings certificateSettings)
        {
            var x509Certificate2 = CreateX509Certificate2(certificateSettings);
            var x509SecurityKey = new X509SecurityKey(x509Certificate2);
            var credentials = new SigningCredentials(x509SecurityKey, JwsAlgorithm);
            return credentials;
        }
    }
}