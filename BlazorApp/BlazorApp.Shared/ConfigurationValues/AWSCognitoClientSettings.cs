namespace BlazorApp.Shared.ConfigurationValues
{
    public class AWSCognitoSettings 
    {
        public string UserPoolClientId { get; set; }
        public string UserPoolClientSecret { get; set; }
        public string UserPoolId { get; set; }
        public string TokenEndpoint { get; set; }
    }
}
