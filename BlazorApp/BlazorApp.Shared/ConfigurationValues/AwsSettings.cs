namespace BlazorApp.Shared.ConfigurationValues
{
    public class AwsSettings
    {
        public string AwsAccessKey { get; set; }
        public string AwsSecretKey { get; set; }

        public string S3ServiceUrl { get; set; } = "https://s3.amazonaws.com";
        public string CloudFrontUrl { get; set; }
        public string ConfigBucketName { get; set; }
        public string ImagesBucketName { get; set; } = "{0}{1}";
        public string DocumentsBucketName { get; set; } = "{0}{1}";
        public string Region { get; set; }
    }
}