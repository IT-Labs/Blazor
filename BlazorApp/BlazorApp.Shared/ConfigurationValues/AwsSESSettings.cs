namespace BlazorApp.Shared.ConfigurationValues
{
    public class AwsSESSettings
    {
        public string MailSender { get; set; }
        public string RegionSystemName { get; set; }
        public bool IsTestMode { get; set; }
        public string TestEmail { get; set; }
        public string EmailBody { get; set; }
    }
}