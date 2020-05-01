namespace BlazorApp.Shared.ConfigurationValues
{
    public class AwsSQSSettings
    {
        public string RegionSystemName { get; set; }
        public string QueueTemplate { get; set; } = "{0}";
        public int MessageRetentionSeconds { get; set; } = 600;
        public int ErrorQueueRetentionPeriodSeconds { get; set; } = 1209600;
        public int VisibilityTimeoutSeconds { get; set; } = 60;
        public int DeliveryDelaySeconds { get; set; } = 1;
        public int RetryCountBeforeSendingToErrorQueue { get; set; } = 3;
        public int PublishFailureReAttempts { get; set; } = 3;
        public int PublishFailureBackoffMilliseconds { get; set; } = 200;
    }
}