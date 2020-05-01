namespace BlazorApp.Shared.ConfigurationValues
{
    public class CertificateSettings
    {
        public string CertificateName { get; set; }
        public string CertificatePassword { get; set; }
        public int ExpirationSeconds { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string FolderPath { get; set; }
    }
}
