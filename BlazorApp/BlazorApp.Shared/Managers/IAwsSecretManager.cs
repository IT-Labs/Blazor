using BlazorApp.Shared.Response;

namespace BlazorApp.Shared.Managers
{
    public interface IAwsSecretManager
    {
        Response<string> GetSecret(string secretId);
    }
}
